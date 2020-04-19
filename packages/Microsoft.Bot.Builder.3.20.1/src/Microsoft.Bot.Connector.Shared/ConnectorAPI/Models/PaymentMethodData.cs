// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Bot.Connector
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Indicates a set of supported payment methods and any associated payment
    /// method specific data for those methods
    /// </summary>
    public partial class PaymentMethodData
    {
        /// <summary>
        /// Initializes a new instance of the PaymentMethodData class.
        /// </summary>
        public PaymentMethodData()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PaymentMethodData class.
        /// </summary>
        /// <param name="supportedMethods">Required sequence of strings
        /// containing payment method identifiers for payment methods that the
        /// merchant web site accepts</param>
        /// <param name="data">A JSON-serializable object that provides
        /// optional information that might be needed by the supported payment
        /// methods</param>
        public PaymentMethodData(IList<string> supportedMethods = default(IList<string>), object data = default(object))
        {
            SupportedMethods = supportedMethods;
            Data = data;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets required sequence of strings containing payment method
        /// identifiers for payment methods that the merchant web site accepts
        /// </summary>
        [JsonProperty(PropertyName = "supportedMethods")]
        public IList<string> SupportedMethods { get; set; }

        /// <summary>
        /// Gets or sets a JSON-serializable object that provides optional
        /// information that might be needed by the supported payment methods
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

    }
}
