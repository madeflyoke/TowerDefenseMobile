using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace TD.Ad
{
    public class AdManager
    {
        //private const string androidBannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";
        //private const string iOSBannerAdUnitId = "ca-app-pub-3940256099942544/2934735716";
        private const string androidInterstitialAdUnitId = "ca-app-pub-2034412524665269/4047958978";
        private const string iOSInterstitialAdUnitId = "ca-app-pub-3940256099942544/4411468910";

        //private string bannerAdUnitId;
        //private BannerView bannerView;

        private string interstitialAdUnitId;
        private InterstitialAd interstitialAd;

        public void Initialize()
        {
            MobileAds.Initialize((initStatus) =>
            {                
                Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
                foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
                {
                    string className = keyValuePair.Key;
                    AdapterStatus status = keyValuePair.Value;
                    switch (status.InitializationState)
                    {
                        case AdapterState.NotReady:
                            Debug.Log("Adapter: " + className + " not ready.");
                            break;
                        case AdapterState.Ready:
                            Debug.Log("Adapter: " + className + " is initialized.");
                            break;
                    }
                }
            });

#if UNITY_ANDROID
            //bannerAdUnitId = androidBannerAdUnitId;
            interstitialAdUnitId = androidInterstitialAdUnitId;
#elif UNITY_IOS
            //bannerAdUnitId = iOSBannerAdUnitId;
            interstitialAdUnitId = iOSInterstitialAdUnitId;
#endif
            InterstitialInitialize();
        }

        #region Interstitial

        private void InterstitialInitialize()
        {
            interstitialAd = new InterstitialAd(interstitialAdUnitId);
            interstitialAd.OnAdLoaded += HandleOnAdLoaded;
            interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            interstitialAd.OnAdOpening += HandleOnAdOpened;
            interstitialAd.OnAdClosed += HandleOnAdClosed;
            interstitialAd.OnAdFailedToShow += HandeOnAdFailedShow;
            LoadInterstitial();
        }

        private void LoadInterstitial()
        {
            AdRequest request = new AdRequest.Builder().Build();
            if (interstitialAd!=null)
            {
                interstitialAd.LoadAd(request);
            }
        }

        public void ShowInterstitial()
        {
            if (interstitialAd!=null&&interstitialAd.IsLoaded())
            {
                interstitialAd.Show();
            }
        }

        private void ResetInterstitial()
        {
            if (interstitialAd != null)
            {
                interstitialAd.OnAdLoaded -= HandleOnAdLoaded;
                interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
                interstitialAd.OnAdOpening -= HandleOnAdOpened;
                interstitialAd.OnAdClosed -= HandleOnAdClosed;
                interstitialAd.OnAdFailedToShow -= HandeOnAdFailedShow;
                interstitialAd.Destroy();
            }
        }

        #endregion

        #region Banner

        //public void LoadBanner()
        //{
        //    ResetBanner();
        //    bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.BottomRight);
        //    bannerView.OnAdLoaded += HandleOnAdLoaded;
        //    bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        //    bannerView.OnAdOpening += HandleOnAdOpened;
        //    bannerView.OnAdClosed += HandleOnAdClosed;
        //    AdRequest request = new AdRequest.Builder().Build();
        //    bannerView.LoadAd(request);          
        //}

        //public void ResetBanner()
        //{
        //    if (bannerView != null)
        //    {
        //        bannerView.OnAdLoaded -= HandleOnAdLoaded;
        //        bannerView.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        //        bannerView.OnAdOpening -= HandleOnAdOpened;
        //        bannerView.OnAdClosed -= HandleOnAdClosed;
        //        bannerView.Destroy();
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Banner is null reference");
        //        return;
        //    }
        //}

        #endregion

        #region Callbacks
        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            Debug.Log($"{sender} Ad Loaded");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.LogWarning($"{sender} Ad FAILURE to load, message: " + args.LoadAdError.GetMessage());
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            Debug.Log($"{sender} Ad Opened");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            Debug.Log($"{sender} Ad Closed (return to app)");
            if (sender.GetType() == typeof(InterstitialAd))
            {
                Debug.Log($"{sender} Ad, loading next...");
                LoadInterstitial();
            }
        }

        public void HandeOnAdFailedShow(object sender, AdErrorEventArgs args)
        {
            Debug.Log($"{sender} Ad Failed to Show, msg: {args.AdError.GetMessage()}");
        }

#endregion

        ~AdManager()
        {
            //ResetBanner();
            ResetInterstitial();
        }
    }

}
