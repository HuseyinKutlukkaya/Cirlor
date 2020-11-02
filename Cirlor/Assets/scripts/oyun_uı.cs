using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class oyun_uı : MonoBehaviour {

    public Sprite sprite_play;
    public Sprite sprite_pause;
    public Button img_pause;
    public Canvas canvas_pause;
    public Canvas canvas_kaybettin;
    public Text txt_sayac;
    public Canvas canvas_devam;
    public Canvas canvas_parayok;
    public void Menuye_Git()
    {
        SceneManager.LoadScene(1);
    }
    public void Yeniden_Oyna()
    {
        SceneManager.LoadScene(2);

    }
    
    IEnumerator sayac()
    {
       
        txt_sayac.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        txt_sayac.text = "2";
        yield return new WaitForSeconds(1);
        txt_sayac.text = "1";
        yield return new WaitForSeconds(1);
        genel.pause = false;
        txt_sayac.gameObject.SetActive(false);
        txt_sayac.text = "3";

    }
   void Yandiktan_Sonra_Devam()
    {
        if (genel.olumsayaci == -1)
        {//reklam başlar ve biter
            canvas_devam.gameObject.SetActive(false);
            genel.canvasdevamaktifmi = false;
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.tag == "yuvarlak")
                {
                    Destroy(go);
                }
            }
            canvas_kaybettin.gameObject.SetActive(false);
          
            db.instance.para_sil((genel.puan - (genel.puan % 10)) / 10);


            genel.tekrarli_uretim_arasi_bekleme_suresi = 1;
            genel.olunce_devam_ediyormu = true;
            genel.olunce_zorluk_tetikleyici = true;
            StartCoroutine(sayac());
        }
        else
        {
            if (db.para >= genel.olunce_devam_parasi)
            {
                Debug.Log("once"+genel.olunce_devam_parasi.ToString());
                canvas_devam.gameObject.SetActive(false);
                genel.canvasdevamaktifmi = false;

                GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                foreach (GameObject go in allObjects)
                {
                    if (go.tag == "yuvarlak")
                    {
                        Destroy(go);
                    }
                }
              
                Debug.Log("sonra" + genel.olunce_devam_parasi.ToString());
                canvas_kaybettin.gameObject.SetActive(false);
                db.instance.para_sil((genel.puan - (genel.puan % 10)) / 10);
                db.instance.para_sil(genel.olunce_devam_parasi);
                Debug.Log(db.para.ToString());
                genel.tekrarli_uretim_arasi_bekleme_suresi = 1;
                genel.olunce_devam_ediyormu = true;
                genel.olunce_zorluk_tetikleyici = true;
                if(genel.olunce_devam_parasi<1250)
                genel.olunce_devam_parasi *= 5;
                StartCoroutine(sayac());
            }
            else
            {
                canvas_parayok.gameObject.SetActive(true);
            }
        }
       
    }
    public void OK()
    {
        canvas_parayok.gameObject.SetActive(false);
    }
    public void yes()
    {
       
        
        Yandiktan_Sonra_Devam();
           
       
    }
    public void no()
    {
       genel.canvasdevamaktifmi = false;
        canvas_devam.gameObject.SetActive(false);
        canvas_kaybettin.gameObject.SetActive(true);
      
     
    }
    public void Pause()
    {
        if (!genel.canvasdevamaktifmi)
        {

        
        if (genel.pause)
        {
            img_pause.GetComponent<Image>().sprite = sprite_pause;          
            canvas_pause.gameObject.SetActive(false);
            StartCoroutine(sayac());
        }
        else if (!genel.pause)
        {
            img_pause.GetComponent<Image>().sprite = sprite_play;
            canvas_pause.gameObject.SetActive(true);
            genel.pause = true;
        }
        }
    }
    void Start () {
        txt_sayac.gameObject.SetActive(false);
	}
	

	void Update () {
     
	}
}
