using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
public class AdManager : MonoBehaviour
{
    public static AdManager instance;
   public enum EnvironmentType{
        Prod,
        Staging
    }
    public EnvironmentType environment;
    private string bannerAdUnitID;
    private string rewardedAdUnitID;
    private string interstitialAdUnitID;
    private string rewardedInterstitialAdUnitID;

    public RewardedAd rewardedAd;
    BannerView bannerView;
    InterstitialAd interstitial;
    private RewardedInterstitialAd rewardedInterstitialAd;
    void Awake()
    {
       instance = this;
    }
    
    public void Start()
    {
       // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => {
             this.InitSetup();
       
        });
  
    }
    public void InitSetup(){
        if(environment == EnvironmentType.Staging){
             #if UNITY_EDITOR
                bannerAdUnitID="[BANNER ID]";
                rewardedAdUnitID= "[REWARDED ID]";
                interstitialAdUnitID= "ca-app-pub-3940256099942544/1033173712";
                rewardedInterstitialAdUnitID = "[REWARDED INTERSTITIAL ID]";
            #elif UNITY_ANDROID
                bannerAdUnitID="[BANNER ID]";
                rewardedAdUnitID= "[REWARDED ID]";
                interstitialAdUnitID= "ca-app-pub-3940256099942544/1033173712";
                rewardedInterstitialAdUnitID = "[REWARDED INTERSTITIAL ID]";
            #elif UNITY_IPHONE
                bannerAdUnitID="[BANNER ID]";
                rewardedAdUnitID= "[REWARDED ID]";
                interstitialAdUnitID= "ca-app-pub-3940256099942544/4411468910";
                rewardedInterstitialAdUnitID = "[REWARDED INTERSTITIAL ID]";
            #else
                Debug.LogError($"UNEXPECTED PLATFORM");
            #endif
        }
        else{
            #if UNITY_EDITOR
                bannerAdUnitID="[BANNER ID]";
                rewardedAdUnitID= "[REWARDED ID]";
                interstitialAdUnitID= "[INTERSTITIAL ID]";
                rewardedInterstitialAdUnitID = "[REWARDED INTERSTITIAL ID]";
            #elif UNITY_ANDROID
                bannerAdUnitID="[BANNER ID]";
                rewardedAdUnitID= "[REWARDED ID]";
                interstitialAdUnitID= "ca-app-pub-1294444334428285/9062066130";
                rewardedInterstitialAdUnitID = "[REWARDED INTERSTITIAL ID]";
            #elif UNITY_IPHONE
                bannerAdUnitID="[BANNER ID]";
                rewardedAdUnitID= "[REWARDED ID]";
                interstitialAdUnitID= "ca-app-pub-1294444334428285/5832991890";
                rewardedInterstitialAdUnitID = "[REWARDED INTERSTITIAL ID]";
            #else
                Debug.LogError($"UNEXPECTED PLATFORM");
            #endif
            
        }
        LoadInterstitialAd();
    }


    #region  Interstitial Ad
    /// <summary>
     /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
      // Clean up the old ad before loading a new one.
      if (interstitial != null)
      {
            interstitial.Destroy();
            interstitial = null;
      }

      Debug.Log("Loading the interstitial ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest();
      adRequest.Keywords.Add("unity-admob-sample");

      // send the request to load the ad.
      InterstitialAd.Load(interstitialAdUnitID, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
            // if error is not null, the load request failed.
            if (error != null || ad == null)
            {
                Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);
                return;
            }

             Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

            interstitial = ad;
        });
    }

    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public void ShowAd()
    {
        if (interstitial != null && interstitial.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitial.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    
  
    private void RegisterEventHandlers(InterstitialAd ad)
    {
    // Raised when the ad is estimated to have earned money.
    ad.OnAdPaid += (AdValue adValue) =>
    {
        Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
            adValue.Value,
            adValue.CurrencyCode));
    };
    // Raised when an impression is recorded for an ad.
    ad.OnAdImpressionRecorded += () =>
    {
        Debug.Log("Interstitial ad recorded an impression.");
    };
    // Raised when a click is recorded for an ad.
    ad.OnAdClicked += () =>
    {
        Debug.Log("Interstitial ad was clicked.");
    };
    // Raised when an ad opened full screen content.
    ad.OnAdFullScreenContentOpened += () =>
    {
        Debug.Log("Interstitial ad full screen content opened.");
    };
    // Raised when the ad closed full screen content.
    ad.OnAdFullScreenContentClosed += () =>
    {
        Debug.Log("Interstitial ad full screen content closed.");
    };
    // Raised when the ad failed to open full screen content.
    ad.OnAdFullScreenContentFailed += (AdError error) =>
    {
        Debug.LogError("Interstitial ad failed to open full screen content " +
                       "with error : " + error);
    };
    }

    private void RegisterReloadHandler(InterstitialAd ad)
{
    // Raised when the ad closed full screen content.
    ad.OnAdFullScreenContentClosed += () =>
    {
        Debug.Log("Interstitial Ad full screen content closed.");

        // Reload the ad so that we can show another as soon as possible.
        LoadInterstitialAd();
    };
    // Raised when the ad failed to open full screen content.
    ad.OnAdFullScreenContentFailed += (AdError error) =>
    {
        Debug.LogError("Interstitial ad failed to open full screen content " +
                       "with error : " + error);

        // Reload the ad so that we can show another as soon as possible.
        LoadInterstitialAd();
    };
    }

    #endregion
   
}
