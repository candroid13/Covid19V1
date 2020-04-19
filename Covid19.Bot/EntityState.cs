using Covid19.Business.Abstract;
using Covid19.Business.DependencyResolvers.SimpleInjector;
using Covid19.Core.Utilities.CustomData;
using Covid19.Core.Utilities.StateFlow;
using Covid19.Core.Utilities.StringOperations;
using Covid19.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Covid19.Bot
{
    public class EntityState : BaseEntityState
    {
        #region Const
        private readonly BotEntity _botEntity;
        private readonly ICovidService _covidService;
        #endregion
        public EntityState(BotEntity botEntity)
        {
            _botEntity = botEntity;
            _covidService = InstanceFactory.GetInstance<ICovidService>();
        }
        public override IEnumerable<Func<StateParams>> Create()
        {
            yield return Allow(
              () =>
              {
                  _botEntity.BotEntityDetail.StateParameters.MissingParameter = StringExtentions.IsAnyNullOrEmpty(_botEntity.BotEntityDetail.IntentEntities);
                  if(_botEntity.BotEntityDetail.IntentName.Equals("DiseaseQuestion") && !string.IsNullOrEmpty(_botEntity.BotEntityDetail.StateParameters.MissingParameter))
                  {
                      _botEntity.BotEntityDetail.StateParameters.MissingParameterData = DataList.Countries();
                  }
                  return !string.IsNullOrEmpty(_botEntity.BotEntityDetail.StateParameters.MissingParameter);
              },
              () => _botEntity.BotEntityDetail.StateParameters);
            yield return Allow(
                () =>
                {
                    _botEntity.BotEntityDetail.StateParameters.IsMissingParameterCompleted = true;
                    if (_botEntity.BotEntityDetail.IntentName.Equals("DiseaseQuestion"))
                    {
                        var covidDiseaseCountryList = _covidService.GetCovidResult().Result;
                        var countryName = _botEntity.BotEntityDetail.IntentEntities.FirstOrDefault().Value;
                        var covidResult = covidDiseaseCountryList.result.FirstOrDefault(x => x.country.ToLower().Contains(countryName.ToLower()));
                        #region Control Message's Language Eng To Turkish
                        if (countryName.Equals("Turkey"))
                        {
                            countryName = "Türkiye'deki";
                        }
                        else if(countryName.Equals("Spain"))
                        {
                            countryName = "İspanya'daki";
                        }
                        else if (countryName.Equals("America"))
                        {
                            countryName = "Amerika'daki";
                        }
                        else if (countryName.Equals("China"))
                        {
                            countryName = "Çin'deki";
                        }
                        #endregion

                        string newCases = covidResult.newCases == string.Empty ? "Bilgiler Henüz Girilmemiş" : covidResult.newCases;
                        string newDeaths = covidResult.newDeaths == string.Empty ? "Bilgiler Henüz Girilmemiş" : covidResult.newDeaths;
                        string activeCases = covidResult.activeCases == string.Empty ? "Bilgiler Henüz Girilmemiş" : covidResult.activeCases;
                        string totalRecovered = covidResult.totalRecovered == string.Empty ? "Bilgiler Henüz Girilmemiş" : covidResult.totalRecovered;
                        string totalCases = covidResult.totalCases == string.Empty ? "Bilgiler Henüz Girilmemiş" : covidResult.totalCases;
                        string totalDeaths = covidResult.totalDeaths == string.Empty ? "Bilgiler Henüz Girilmemiş" : covidResult.totalDeaths;

                        _botEntity.BotEntityDetail.StateParameters.ResultMessage = countryName + " Yeni vaka sayısı : " + newCases +
                                                                                                 " Bugün vefat eden sayısı : " + newDeaths + 
                                                                                                 " Toplam aktif vaka sayısı : "   + activeCases  + 
                                                                                                 " Toplam tedavi olan kişi sayısı :  " + totalRecovered + 
                                                                                                 " Toplam vaka sayısı : " + totalCases + 
                                                                                                 " Toplam vefat sayısı : " + totalDeaths;
                    }
                    _botEntity.BotEntityDetail.StateParameters.IsSuccess = true;
                    return _botEntity.BotEntityDetail.StateParameters.IsSuccess;
                },
                () => _botEntity.BotEntityDetail.StateParameters);
        }
    }
}