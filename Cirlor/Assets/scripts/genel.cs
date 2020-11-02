using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class genel : MonoBehaviour {




  

    public Collider2D[] collider;
  
    public static int puan = 0;
    public static int olunce_devam_parasi = 10;


    public Text txt_puan;
   
    public Text txt_sayac;
    public Material[] renkler = new Material[8];
    public GameObject[] patlamalar = new GameObject[8];
   
  private  GameObject uretilcek_yuvarlak;
    public GameObject yuvarlaksolo;
    public GameObject yuvarlaktekrar;

    public static bool pause = false;
    public static bool kaybettin_sinyali = false;

   public static float tekrarli_uretim_arasi_bekleme_suresi = 1;

    public Canvas canvas_kaybettin;
    public Canvas canvas_pause;
    public Canvas canvas_parayok;
  public static  int sayac = 0;
    public static int candegeri=2;

    #region ölünce yeniden başlama şeyleri (play again variables)
    public static int olumsayaci = 0;
    public static float oldugundeki_hiz;
    public static bool olunce_devam_ediyormu = false;
    public static bool olunce_zorluk_tetikleyici = false;
    #endregion

    #region kaybettin  uı şeyleri (game over screen variables)

    public Text txt_skor;
    public Text txt_para;
    public Text txt_rekor;
    #endregion

    #region  devam etme şeyleri  (continue variables)
    public Canvas canvas_devamet;
    public static bool canvasdevamaktifmi = false;
    public Text txt_devam_aciklama;
    #endregion
    #region eğitim şeyleri(tutorial variables)
    public Text txt_egitim_bildirimleri;
    public static int egitim_sayaci;
    public Button btn_egitimi_bitir;
    #endregion

    private void Awake()
    {
        txt_egitim_bildirimleri.gameObject.SetActive(false);
        btn_egitimi_bitir.gameObject.SetActive(false);
        egitim_sayaci = 0;
        canvas_parayok.gameObject.SetActive(false); 
        canvasdevamaktifmi = false;
        canvas_kaybettin.gameObject.SetActive(false);
        canvas_pause.gameObject.SetActive(false);
        canvas_devamet.gameObject.SetActive(false);
        pause = false;
        olunce_zorluk_tetikleyici = false;
        olunce_devam_ediyormu = false;
        puan = 0;
        olumsayaci = 0;
        olunce_devam_parasi = 10;
        tekrarli_uretim_arasi_bekleme_suresi = 1;
        kaybettin_sinyali = false;
        candegeri = 2;
        sayac = 0;
    }
    
    void Start()
    {

        if (db.egitim == "yapilmadi") //  eğitim gerekiyomu kontrol
        {
            txt_egitim_bildirimleri.gameObject.SetActive(true);
           
            egitimi_baslat();
        }
        else
        {
            oyunu_baslat();

        }



        
    }
    void oyunu_baslat()// eğitim yapılmıyıcaksa
    {
        StartCoroutine(uret_spawnn());
        StartCoroutine(zorluk_ayari());
    }
   void egitimi_baslat()// egitim yapılıcaksa
    {
        if (db.egitim=="yapilmadi")
        {
        StartCoroutine( egitim_yuvarlaklari_uret());
           
        }
    }
    private IEnumerator egitim_yuvarlaklari_uret()
    {
       GameObject c= Instantiate(yuvarlaksolo, new Vector2(-7, 2), Quaternion.identity);
        c.GetComponent<yuvarlak_kontrol>().yesil_patlama = patlamalar[0];
        GameObject a = Instantiate(yuvarlaktekrar, new Vector2(0, 2), Quaternion.identity);
        a.GetComponent<yuvarlak_tekrarli_kontrol>().can = candegeri;
        a.GetComponent<yuvarlak_tekrarli_kontrol>().yesil_patlama = patlamalar[0];
        yield return new WaitForEndOfFrame();
        candegeri = 3;
        GameObject b = Instantiate(yuvarlaktekrar, new Vector2(7, 2), Quaternion.identity);
        b.GetComponent<yuvarlak_tekrarli_kontrol>().can = candegeri;
        b.GetComponent<yuvarlak_tekrarli_kontrol>().yesil_patlama = patlamalar[0];
        yield return new WaitForEndOfFrame();

        candegeri = 2;

    }
    public void  kaybettin()//yanınca gelen ekran
    {
        if (olumsayaci % 3 == 0&&db.olumsayaci%3==0)
        {
            ad_manager.instance.sayfa_reklam_goster();
        }
        olumsayaci++;
        db.olumsayaci++;
        oldugundeki_hiz = tekrarli_uretim_arasi_bekleme_suresi;
        txt_skor.text = "SKOR=" + puan.ToString();
       
        if (puan>db.rekor)
        {
            db.instance.rekor_güncelle(puan);
        }
        txt_rekor.text = "HIGH SCORE=" + db.rekor.ToString();
        txt_para.text = ((puan - (puan % 10)) / 10).ToString();
        db.instance.para_ekle((puan - (puan % 10)) / 10);
        if (olumsayaci==-1)// ilk ölümde gelen video izleme seçim ekranı
        {
            txt_devam_aciklama.text= "Watch video and continue";
        }
        else// sonraki ölümlerde gelen altın ile devam etme
        {
            txt_devam_aciklama.text = "Pay "+olunce_devam_parasi.ToString()+" gold and continue";
        
        }
        
       
         
           
        
       
        
       
    }
   IEnumerator egitim_bitimi()
    {
       
        txt_sayac.text = "3";
        txt_sayac.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        txt_sayac.text = "2";
        yield return new WaitForSeconds(1);
        txt_sayac.text = "1";
        yield return new WaitForSeconds(1);
        txt_sayac.gameObject.SetActive(false);
        db.instance.egitim_guncelle();
        oyunu_baslat();
    
        txt_sayac.text = "3";
    }
    public void Egitimi_bitir()
    {
        btn_egitimi_bitir.gameObject.SetActive(false);
        txt_egitim_bildirimleri.gameObject.SetActive(false);    
        puan = 0;
        StartCoroutine(egitim_bitimi());
        
    }
    private void FixedUpdate()
    {
       
       // Debug.Log("hiz="+tekrarli_uretim_arasi_bekleme_suresi.ToString()+"  oldugundeki hiz=" + oldugundeki_hiz.ToString());
        ad_manager.instance.banner_hide();
        if (genel.egitim_sayaci==3)
        {
            txt_egitim_bildirimleri.text = "Each circle explodes in 3 seconds.\r\nHave fun!";
            btn_egitimi_bitir.gameObject.SetActive(true);
            egitim_sayaci = 0;
        }
        if (olunce_zorluk_tetikleyici)//ölüp devam edince  hızın ayarlanması
        {
            
                StopAllCoroutines();
            oyunu_baslat();
            olunce_zorluk_tetikleyici = false;
        }
        if (kaybettin_sinyali)//ölünce canvasların görünür olması
        {
            canvas_devamet.gameObject.SetActive(true);
            canvasdevamaktifmi = true;
            kaybettin_sinyali = false;
            kaybettin();
        }

 foreach (Touch touch in Input.touches)
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
            if (hit.collider != null)
            {
                if (touch.phase == TouchPhase.Began)
                {//dokunulan yerin hangi yuvarlak oldugunu algılama yeri

                    if (hit.collider.gameObject.GetComponent<yuvarlak_kontrol>() != null)
                    {
                        hit.collider.gameObject.GetComponent<yuvarlak_kontrol>().down();

                    }
                    else if (hit.collider.gameObject.GetComponent<yuvarlak_kontrol>() != null)
                    {
                        hit.collider.gameObject.GetComponent<yuvarlak_tekrarli_kontrol>().down();

                    }

                }
            }
        }  // dokunma  algılama



    }

   
  
    IEnumerator uret_spawnn()
    {//uretim uretme yeri
        
       
        uretilcek_secim();
        StartCoroutine(Uret(uretilcek_yuvarlak));
      


        yield return new WaitForSeconds(tekrarli_uretim_arasi_bekleme_suresi);





        StartCoroutine(uret_spawnn());
    }
    IEnumerator zorluk_ayari()
    {
        
      
        if (olunce_devam_ediyormu)
        {// ölüp devam edince 5 sn içinde öldünü hıza ulaşır  
            
            float artis_hizi = (1 - oldugundeki_hiz)/5;
           
            for (int i = 0; i < 5; i++)
            {
             //   Debug.Log("artış gerçekleşti");
                tekrarli_uretim_arasi_bekleme_suresi -= artis_hizi;
                yield return new WaitForSeconds(1);
            }
            olunce_devam_ediyormu = false;

            if (tekrarli_uretim_arasi_bekleme_suresi >= 0.19f)
                StartCoroutine(zorluk_ayari());
        }
        else
        {//her 1 snde hız artar 40 sn içinde max hıza ulaşır
            yield return new WaitForSeconds(1);

             
                tekrarli_uretim_arasi_bekleme_suresi -=0.02025f;

           
            if (tekrarli_uretim_arasi_bekleme_suresi >= 0.19f)
                StartCoroutine(zorluk_ayari());
        }

    }

    Vector3 konum;
    bool konum_tekrar = true;
    IEnumerator Uret(GameObject nesne)
    {
        if (!pause)
        {// eger oyun devam ediyorsa

       
        konum_tekrar = true;// aynı yere 2 defa üretmemesi için ekranda  yer varsa
        do
        {
            for (int i = 0; i < 5000; i++)// 5k defa random konup ile boşluk kontrolu yapar
            {
                konum = new Vector3(Random.Range(-4.8f, 4.1f), Random.Range(-3.8f, 3.6f), 0);
                collider = Physics2D.OverlapBoxAll(konum, nesne.GetComponent<BoxCollider2D>().size, 0);
                if (collider.Length == 0)
                {// boş yer bulunca döngüden çıkar
                    konum_tekrar = false;
                    break;
                }
                 
            }
            if (konum_tekrar)// eğer bulamazsa  0.001 sn bekler bida arar
                yield return new WaitForSeconds(0.001f);
        } while (collider.Length != 0);
        GameObject cobje = (GameObject)Instantiate(nesne, konum, Quaternion.identity);
      
        if (db.mod=="tekli")// tekli renkler uygulanır
        {

                cobje.GetComponentInChildren<SpriteRenderer>().color = renkler[db.color_list_index].color;
                if (cobje.GetComponent<yuvarlak_kontrol>() != null)
                {
                    cobje.GetComponent<yuvarlak_kontrol>().yesil_patlama = patlamalar[db.color_list_index];
                }
                else if (cobje.GetComponent<yuvarlak_tekrarli_kontrol>() != null)
                {
                    cobje.GetComponent<yuvarlak_tekrarli_kontrol>().yesil_patlama = patlamalar[db.color_list_index];

                }
             

        }
        else if (db.mod=="random")// random renk ise sırayla bütün  renkler uygulanır
        {

            cobje.GetComponentInChildren<SpriteRenderer>().color = renkler[db.color_list_index].color;
                if (cobje.GetComponent<yuvarlak_kontrol>()!=null)
                {
                    cobje.GetComponent<yuvarlak_kontrol>().yesil_patlama = patlamalar[db.color_list_index];
                }
                else if (cobje.GetComponent<yuvarlak_tekrarli_kontrol>() != null)
                {
                    cobje.GetComponent<yuvarlak_tekrarli_kontrol>().yesil_patlama = patlamalar[db.color_list_index];

                }
                db.color_list_index++;
            db.color_list_index = db.color_list_index % 8;
        }
        }
    }




    void Update()
    {
   
        txt_puan.text = puan.ToString();// puan ekrana yazılır


    }

    void uretilcek_secim()
    {
        if (sayac >= 300&&sayac<700)// 400 ile 1000 arası 10 tanede bir 2 canlı üretir
        {
           
            if (sayac % 10 == 0)
            {
                uretilcek_yuvarlak = yuvarlaktekrar;
            }
            else
            {
                uretilcek_yuvarlak = yuvarlaksolo;
            }
        }
        else if(sayac<300)// 400 e kadar duz uretir
            uretilcek_yuvarlak = yuvarlaksolo;
        else if(sayac>=700)// 1000 den sonra 12 tane de bir 3  canlı üretir
        {
            candegeri = 3;
            if (sayac % 12 == 0)
            {
                uretilcek_yuvarlak = yuvarlaktekrar;
            }
            else
            {
                uretilcek_yuvarlak = yuvarlaksolo;
            }

        }
    }


}
