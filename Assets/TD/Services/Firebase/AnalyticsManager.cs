using UnityEngine;
using Firebase;
using Firebase.Analytics;
using System.Collections.Generic;

namespace TD.Services.Firebase
{
    public class AnalyticsManager
    {
        private const string SettingsRetryButtonEvent = "SettingsRetryButtonPressed";
        private const string SettingsShowedEvent = "SettingsShowed";
        private const string AdShownEvent = "AdShownEvent";
        private const string AdFinishedEvent = "AdFinishedEvent";

        private const string ShowHideParameter = "ShowHide";
        private const string AdUnitParameter = "AdUnit";
        private const string AdMediationAdapterParameter = "AdMediationAdapter";

        private FirebaseApp app;

        public void Initialize()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    app = FirebaseApp.DefaultInstance;
                }
                else
                {
                    Debug.LogError(string.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
        }

        public void SendEvent(LogEventName eventName, params EventParameter[] parameters)
        {
            string correctEventName = string.Empty;
            switch (eventName)
            {
                case LogEventName.SettingsRetryButtonEvent:
                    correctEventName = SettingsRetryButtonEvent;
                    break;
                case LogEventName.SettingsShowHideEvent:
                    correctEventName = SettingsShowedEvent;
                    break;
                case LogEventName.AdShownEvent:
                    correctEventName = AdShownEvent;
                    break;
                case LogEventName.AdFinishedEvent:
                    correctEventName = AdFinishedEvent;
                    break;
                default:
                    Debug.Log($"Incorrect event name while sending event {eventName}");
                    return;
            }
            if (parameters != null || parameters.Length != 0)
            {
                List<Parameter> correctParameters = new List<Parameter>();
                foreach (EventParameter param in parameters)
                {
                    switch (param.name)
                    {
                        case LogEventParameterName.ShowHideBoolean:
                            correctParameters.Add(new Parameter(ShowHideParameter, (bool)param.value == true ? 1 : 0));
                            break;
                        case LogEventParameterName.AdUnitString:
                            correctParameters.Add(new Parameter(AdUnitParameter, param.value.ToString()));
                            break;
                        case LogEventParameterName.AdMediationAdapterString:
                            correctParameters.Add(new Parameter(AdMediationAdapterParameter, param.value.ToString()));
                            break;
                        default:
                            Debug.Log($"Incorrect parameter name: {param.name} while sending event {eventName}");
                            continue;
                    }
                }
                FirebaseAnalytics.LogEvent(correctEventName, correctParameters.ToArray());

            }
            else
                FirebaseAnalytics.LogEvent(correctEventName); //without parameters
        }

        //#region Alternative

        //private void SendEventInternal(LogEventName eventName, Dictionary<LogEventParameterName, object> parameters)
        //{
        //    string correctEventName = string.Empty;
        //    switch (eventName)
        //    {
        //        case LogEventName.SettingsRetryButton:
        //            correctEventName = settingsRetryButtonEvent;
        //            break;
        //        case LogEventName.SettingShowedEvent:
        //            correctEventName = settingsShowedEvent;
        //            break;
        //        default:
        //            Debug.Log($"Incorrect event name while sending event {eventName}");
        //            return;
        //    }
        //    if (parameters != null || parameters.Count != 0)
        //    {
        //        List<Parameter> correctParameters = new List<Parameter>();
        //        foreach (var pair in parameters)
        //        {
        //            switch (pair.Key)
        //            {
        //                case LogEventParameterName.ShowHideBoolean:
        //                    correctParameters.Add(new Parameter(showHideParameter, (bool)pair.Value == true ? 1 : 0));
        //                    break;
        //                default:
        //                    Debug.Log($"Incorrect parameter name: {pair.Key} while sending event {eventName}");
        //                    continue;
        //            }
        //        }
        //        FirebaseAnalytics.LogEvent(correctEventName, correctParameters.ToArray());
        //    }
        //    else
        //        FirebaseAnalytics.LogEvent(correctEventName); //without parameters

        //}

        //public void SendEvent(LogEventName eventName)
        //{
        //    SendEventInternal(eventName, null);
        //}
        //public void SendEvent(LogEventName eventName, LogEventParameter p1, object v1)
        //{
        //    SendEventInternal(eventName, new Dictionary<LogEventParameter, object> { { p1, v1 } });
        //}
        //public void SendEvent(LogEventName eventName, LogEventParameter p1, object v1, LogEventParameter p2, object v2)
        //{
        //    SendEventInternal(eventName, new Dictionary<LogEventParameter, object> { { p1, v1 }, { p2, v2 } });
        //}
        //public void SendEvent(LogEventName eventName, LogEventParameter p1, object v1, LogEventParameter p2, object v2, LogEventParameter p3, object v3)
        //{
        //    SendEventInternal(eventName, new Dictionary<LogEventParameter, object> { { p1, v1 }, { p2, v2 }, { p3, v3 } });
        //}
        //public void SendEvent(LogEventName eventName, LogEventParameter p1, object v1, LogEventParameter p2, object v2, LogEventParameter p3, object v3, LogEventParameter p4, object v4)
        //{
        //    SendEventInternal(eventName, new Dictionary<LogEventParameter, object> { { p1, v1 }, { p2, v2 }, { p3, v3 }, { p4, v4 } });
        //}
        //public void SendEvent(LogEventName eventName, LogEventParameter p1, object v1, LogEventParameter p2, object v2, LogEventParameter p3, object v3, LogEventParameter p4, object v4, LogEventParameter p5, object v5)
        //{
        //    SendEventInternal(eventName, new Dictionary<LogEventParameter, object> { { p1, v1 }, { p2, v2 }, { p3, v3 }, { p4, v4 }, { p5, v5 } });
        //}

        //#endregion

    }
}



