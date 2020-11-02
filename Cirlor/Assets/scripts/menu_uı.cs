using UnityEngine.SceneManagement;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
public class menu_uı : MonoBehaviour {
    public Text txt_rekor;
    public Canvas canvas_bilgi;
    public Text    txt_bilgi,txt_gold;
    public static bool freegold_bildirim = false;
    private void Start()
    {
        freegold_bildirim = false;
        canvas_bilgi.gameObject.SetActive(false);
      txt_rekor.text = "High Score=" + db.rekor.ToString();
        txt_gold.text = db.para.ToString();
        if (!ad_manager.banner_yuklu)
        {
            ad_manager.instance.banner_yukle();
        }
        else
        {
            ad_manager.instance.banner_show();
        }
    }
   
    public void Oyuna_Gec()
    {
        ad_manager.instance.sayfa_reklam_iste();
        if (ad_manager.banner_yuklu)
            ad_manager.instance.banner_hide();
            SceneManager.LoadScene(2);
        
    }
    public void Magazaya_Gec()
    {
        if (ad_manager.banner_yuklu)
            ad_manager.instance.banner_hide();
        SceneManager.LoadScene(3);
    }
     private void Watch_video_ad_earn_free_gold()
    {
        ad_manager.instance.rewarded_freegold_goster();
        if (ad_manager.video_freegold_varmi=="basarisiz")
        {
              canvas_bilgi.gameObject.SetActive(true);
            txt_bilgi.text = "Try again later.";
        }
        else if(ad_manager.video_freegold_varmi=="basarili")
        {

            txt_gold.text = db.para.ToString();
        }
    }
  


    void gold_kazandin()
    {
        canvas_bilgi.gameObject.SetActive(true);
        txt_bilgi.text = "40 gold added.";
    }
    private void Update()
    {
        txt_gold.text = db.para.ToString();
        if (freegold_bildirim)
        {
            freegold_bildirim = false;
            gold_kazandin();
        }
        
    }

    public void ok()
    {
        canvas_bilgi.gameObject.SetActive(false);
    }
}
