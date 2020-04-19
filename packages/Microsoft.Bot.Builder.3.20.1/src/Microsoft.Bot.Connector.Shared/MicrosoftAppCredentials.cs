﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Connector.Shared.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

#if NET45
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Serialization;
#endif

namespace Microsoft.Bot.Connector
{
    public class MicrosoftAppCredentials : ServiceClientCredentials
    {
        /// <summary>
        /// The key for Microsoft app Id.
        /// </summary>
        public const string MicrosoftAppIdKey = "MicrosoftAppId";

        /// <summary>
        /// The key for Microsoft app Password.
        /// </summary>
        public const string MicrosoftAppPasswordKey = "MicrosoftAppPassword";

        protected static readonly ConcurrentDictionary<string, DateTime> TrustedHostNames = new ConcurrentDictionary<string, DateTime>(
                                                                                        new Dictionary<string, DateTime>() {
                                                                                            { "state.botframework.com", DateTime.MaxValue },
                                                                                            { "api.botframework.com", DateTime.MaxValue },
                                                                                            { "token.botframework.com", DateTime.MaxValue }
                                                                                        });

#if !NET45
        protected ILogger logger;
#endif 

        private readonly Lazy<AdalAuthenticator> authenticator;

        public MicrosoftAppCredentials(string appId = null, string password = null)
        {
            MicrosoftAppId = appId;
            MicrosoftAppPassword = password;
#if NET45
            if (appId == null)
            {
                MicrosoftAppId = ConfigurationManager.AppSettings[MicrosoftAppIdKey] ?? Environment.GetEnvironmentVariable(MicrosoftAppIdKey, EnvironmentVariableTarget.Process);
            }

            if (password == null)
            {
                MicrosoftAppPassword = ConfigurationManager.AppSettings[MicrosoftAppPasswordKey] ?? Environment.GetEnvironmentVariable(MicrosoftAppPasswordKey, EnvironmentVariableTarget.Process);
            }
#endif
            authenticator = new Lazy<AdalAuthenticator>(() => new AdalAuthenticator(new ClientCredential(MicrosoftAppId, MicrosoftAppPassword)), LazyThreadSafetyMode.ExecutionAndPublication);
        }

#if !NET45
        public MicrosoftAppCredentials(string appId, string password, ILogger logger)
            : this(appId, password)
        {
            this.logger = logger;
        }
#endif

#if !NET45
        public MicrosoftAppCredentials(IConfiguration configuration, ILogger logger = null)
            : this(configuration.GetSection(MicrosoftAppIdKey)?.Value, configuration.GetSection(MicrosoftAppPasswordKey)?.Value, logger)
        {
        }
#endif

        public string MicrosoftAppId { get; set; }
        public string MicrosoftAppPassword { get; set; }

        public static string OAuthEndpoint
        {
            get
            {
                string tenant = null;
#if NET45
                // Advanced user only, see https://aka.ms/bots/tenant-restriction
                tenant = SettingsUtils.GetAppSettings("ChannelAuthTenant");
#endif
                var endpointUrl = string.Format(JwtConfig.ToChannelFromBotLoginUrlTemplate, string.IsNullOrEmpty(tenant) ? "botframework.com" : tenant);

                if (Uri.TryCreate(endpointUrl, UriKind.Absolute, out Uri result))
                    return endpointUrl;

                throw new Exception($"Invalid token endpoint: {endpointUrl}");
            }
        }

        public static string OAuthAuthority
        {
            get
            {
                string tenant = null;
#if NET45
                // Advanced user only, see https://aka.ms/bots/tenant-restriction
                tenant = SettingsUtils.GetAppSettings("ChannelAuthTenant");
#endif
                var authority = string.Format(JwtConfig.ConvergedAppAuthority, string.IsNullOrEmpty(tenant) ? "botframework.com" : tenant);

                if (Uri.TryCreate(authority, UriKind.Absolute, out Uri result))
                    return authority;

                throw new Exception($"Invalid token endpoint: {authority}");
            }
        }

        public static string OAuthBotScope { get { return JwtConfig.ToChannelFromBotOAuthScope; } }

        /// <summary>
        /// Adds the host of service url to <see cref="MicrosoftAppCredentials"/> trusted hosts.
        /// </summary>
        /// <param name="serviceUrl">The service url</param>
        /// <param name="expirationTime">The expiration time after which this service url is not trusted anymore</param>
        /// <remarks>If expiration time is not provided, the expiration time will DateTime.UtcNow.AddDays(1).</remarks>
        public static void TrustServiceUrl(string serviceUrl, DateTime expirationTime = default(DateTime))
        {
            try
            {
                if (expirationTime == default(DateTime))
                {
                    // by default the service url is valid for one day
                    var extensionPeriod = TimeSpan.FromDays(1);
                    TrustedHostNames.AddOrUpdate(new Uri(serviceUrl).Host, DateTime.UtcNow.Add(extensionPeriod), (key, oldValue) =>
                    {
                        var newExpiration = DateTime.UtcNow.Add(extensionPeriod);
                        // try not to override expirations that are greater than one day from now
                        if (oldValue > newExpiration)
                        {
                            // make sure that extension can be added to oldValue and ArgumentOutOfRangeException
                            // is not thrown
                            if (oldValue >= DateTime.MaxValue.Subtract(extensionPeriod))
                            {
                                newExpiration = oldValue;
                            }
                            else
                            {
                                newExpiration = oldValue.Add(extensionPeriod);
                            }
                        }
                        return newExpiration;
                    });
                }
                else
                {
                    TrustedHostNames.AddOrUpdate(new Uri(serviceUrl).Host, expirationTime, (key, oldValue) => expirationTime);
                }
            }
            catch (Exception)
            {
#if NET45
                Trace.TraceWarning($"Service url {serviceUrl} is not a well formed Uri!");
#endif
            }
        }

        /// <summary>
        /// Checks if the service url is for a trusted host or not.
        /// </summary>
        /// <param name="serviceUrl">The service url</param>
        /// <returns>True if the host of the service url is trusted; False otherwise.</returns>
        public static bool IsTrustedServiceUrl(string serviceUrl)
        {
            Uri uri;
            if (Uri.TryCreate(serviceUrl, UriKind.Absolute, out uri))
            {
                return TrustedUri(uri);
            }
            return false;
        }

        private bool ShouldSetToken(HttpRequestMessage request)
        {
            if (TrustedUri(request.RequestUri))
            {
                return true;
            }

#if NET45
            Trace.TraceWarning($"Service url {request.RequestUri.Authority} is not trusted and JwtToken cannot be sent to it.");
#else
            logger?.LogWarning($"Service url {request.RequestUri.Authority} is not trusted and JwtToken cannot be sent to it.");
#endif
            return false;
        }

        /// <summary>
        /// Apply the credentials to the HTTP request.
        /// </summary>
        /// <param name="request">The HTTP request.</param><param name="cancellationToken">Cancellation token.</param>
        public override async Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (ShouldSetToken(request))
            {
                var authResult = await GetTokenAsync().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult);
            }
            await base.ProcessHttpRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> GetTokenAsync(bool forceRefresh = false)
        {
            var token = await authenticator.Value.GetTokenAsync(forceRefresh).ConfigureAwait(false);
            return token.AccessToken;
        }

        private void LogWarning(string warning)
        {
#if NET45
            Trace.TraceWarning(warning);
#else
            logger?.LogWarning(warning);
#endif
        }

        private void LogError(string error)
        {
#if NET45
            Trace.TraceError(error);
#else
            logger?.LogError(error);
#endif
        }

        private static bool TrustedUri(Uri uri)
        {
            DateTime trustedServiceUrlExpiration;
            if (TrustedHostNames.TryGetValue(uri.Host, out trustedServiceUrlExpiration))
            {
                // check if the trusted service url is still valid
                if (trustedServiceUrlExpiration > DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(5)))
                {
                    return true;
                }
            }
            return false;
        }

#if NET45
        [Serializable]
#endif
        public sealed class OAuthException : Exception
        {
            public OAuthException(string body, Exception inner)
                : base(body, inner)
            {
            }

#if NET45
            private OAuthException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
#endif
        }
    }
}
