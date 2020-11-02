﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yuvarlak_tekrarli_kontrol : MonoBehaviour {


    bool kullanici_malmi = true;
    public GameObject yesil_patlama, kirimizi_patlama;
  public int can = 2;
    public GameObject text;
    void Start()
    {
        can = genel.candegeri;
        kullanici_malmi = true;
        if (db.egitim == "yapildi")
        {
            Invoke("kendini_yoket", 3f);
            genel.sayac++;
        }
            
        GetComponentInChildren<TextMesh>().text = can.ToString();
    }

    
    void Update()
    {
        if (genel.pause)
        {
            CancelInvoke();
        }
    }


    void kendini_yoket()
    {
        GameObject particle = Instantiate(kirimizi_patlama, transform.position, Quaternion.identity);


        Destroy(particle, 2f);
       genel.pause = true;
       genel.kaybettin_sinyali = true;
        Destroy(gameObject);
    }


    public void down()
    {
        if (!genel.pause)
        {
          
            can--;
            genel.puan++;
            text.GetComponent<TextMesh>().text = can.ToString();
            if (can==0)
            {
                if (db.egitim == "yapilmadi")
                {
                    genel.egitim_sayaci++;
                }
                kullanici_malmi = false;
                if (genel.sayac < 100)
                    genel.puan++;
                else if (genel.sayac < 200)
                    genel.puan += 2;
                else if (genel.sayac < 300)
                    genel.puan += 3;
                else if (genel.sayac < 400)
                    genel.puan += 5;
                else
                    genel.puan += 5;

                genel.sayac++;
                GameObject particle = Instantiate(yesil_patlama, transform.position, Quaternion.identity);
                Destroy(particle, 2f);
                Destroy(gameObject);
            }
        }

    }

    private void OnMouseDown()
    {
        down();
    }
}
