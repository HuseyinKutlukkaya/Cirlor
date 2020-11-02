
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
    
public class ad_manager : MonoBehaviour {

    public static ad_manager instance { get; private set; }

    BannerView bannerAd;
    InterstitialAd sayfa_reklam;
    RewardBasedVideoAd video_freegold;


    string bannerid;//banner menu
    private string interstitialid;//interstitial shop ve olum
    private string rewardedid_menu;//free gold
    public static bool banner_yuklu = false;
    public static string video_freegold_varmi = "";
    public static bool video_freegold_yuklumu = false;
    public static bool tamsayfa_yuklumu = false;
	string banneridString="";
    void Start () {
     
        instance = this;

        rewarded_freegold_iste();
        
        
    }
#region bannerad şeyleri
    public void banner_yukle()
    {
        if (Application.platform==RuntimePlatform.Android)
        {
            bannerid = banneridString;
        }
        else if (Application.platform==RuntimePlatform.IPhonePlayer)
        {
            bannerid = banneridString;
        }
     

        AdRequest request = new AdRequest.Builder().Build();
       bannerAd = new BannerView(bannerid, AdSize.Banner, AdPosition.Bottom);
        bannerAd.LoadAd(request);
        bannerAd.OnAdLoaded += BannerAd_OnAdLoaded;
        
    }

  

    public void banner_show()
    {
        bannerAd.Show();
    }
    public void banner_hide()
    {
        bannerAd.Hide();
    }
    private void BannerAd_OnAdLoaded(object sender, EventArgs e)
    {
        banner_yuklu = true;
        throw new NotImplementedException();
    }
    #endregion

#region tam sayfa reklamlari
    public void sayfa_reklam_iste()
    {
        if (Application.platform == RuntimePlatform.Android)
        {

        interstitialid = "";

        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            interstitialid = "";
        }

        sayfa_reklam = new InterstitialAd(interstitialid);
        AdRequest request = new AdRequest.Builder().Build();
        sayfa_reklam.LoadAd(request);
        sayfa_reklam.OnAdLoaded += Sayfa_reklam_OnAdLoaded;
    }

    private void Sayfa_reklam_OnAdLoaded(object sender, EventArgs e)
    {
        tamsayfa_yuklumu = true;
        throw new NotImplementedException();
    }

    public void sayfa_reklam_goster()
    {
        if (sayfa_reklam.IsLoaded())
        {
            
            sayfa_reklam.Show();
            tamsayfa_yuklumu = false;
            sayfa_reklam_iste();
        }
        else
        {
            if (!tamsayfa_yuklumu)
            {
                sayfa_reklam_iste();
            }
        }
      
        
    }
#endregion
    #region video free gold
    public void rewarded_freegold_iste()
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            rewardedid_menu = "";

        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            rewardedid_menu = "";
        }
        AdRequest request = new AdRequest.Builder().Build();
        video_freegold = RewardBasedVideoAd.Instance;
        video_freegold.LoadAd(request,rewardedid_menu);
        video_freegold.OnAdRewarded += Video_freegold_OnAdRewarded;
        video_freegold.OnAdLoaded += Video_freegold_OnAdLoaded;
    }

    private void Video_freegold_OnAdLoaded(object sender, EventArgs e)
    {
        video_freegold_yuklumu = true;
        throw new NotImplementedException();
    }

    private void Video_freegold_OnAdRewarded(object sender, Reward e)
    {
        db.instance.para_ekle(40);
        menu_uı.freegold_bildirim = true;
        throw new NotImplementedException();
    }

    public void rewarded_freegold_goster()
    {
        video_freegold_varmi = "basarili";
        if (video_freegold.IsLoaded())
        {
            video_freegold_yuklumu = false;
            video_freegold.Show();
            rewarded_freegold_iste();
        }
        else
        {
            if (!video_freegold_yuklumu)
            {
                rewarded_freegold_iste();
            }
            video_freegold_varmi ="basarisiz";
        }
    }
#endregion 
}
