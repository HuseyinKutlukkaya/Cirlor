using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class shop : MonoBehaviour {

    public Image[] resimler = new Image[9];
    public Material[] renkler = new Material[8];
    public Button btn_chroma;
    public Text txt_para;
    public Canvas canvas_parayok;
	void Start () {
       
        canvas_parayok.gameObject.SetActive(false);
        txt_para.text = db.para.ToString();
        for (int i = 0; i <8; i++)
        {
            resimler[i].GetComponent<Image>().color = renkler[i].color;
        }
        for (int i = 0; i <9; i++)
        {
            resimler[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = db.shop_text[i];
        }
	}
	public void Go_Menu()
    {
        SceneManager.LoadScene(1);
    }
    public void OK()
    {
        canvas_parayok.gameObject.SetActive(false);
    }
	public void  Satin_Al()
    {
        int index = Convert.ToInt32(EventSystem.current.currentSelectedGameObject.tag);
        Debug.Log(index.ToString());
        if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text== "Buy")
        {
            if (db.para >= 1000)
            {
                db.para -= 1000;
                txt_para.text = db.para.ToString();
                db.color_list_index = Convert.ToInt32(EventSystem.current.currentSelectedGameObject.tag);
               
                Debug.Log(db.color_list_index.ToString());
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "Use";
                db.shop_text[index] = "Use";
                db.instance.shop_text_textleri_degis();
           
            }
            else
            {
                canvas_parayok.gameObject.SetActive(true);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text == "Use")
        {
            if (db.mod!="tekli")
            {
            db.mod = "tekli";
                db.instance.mod_guncelle();
            }
           
            GameObject go = GameObject.FindWithTag(db.aktif_tag.ToString());
            db.color_list_index = Convert.ToInt32(EventSystem.current.currentSelectedGameObject.tag);
            go.GetComponentInChildren<Text>().text = "Use";
            db.shop_text[db.aktif_tag] = "Use";
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "Active";
            db.shop_text[index] = "Active";
            db.aktif_tag = index;
            db.instance.shop_text_textleri_degis();
            db.instance.mod_guncelle();
        }
      
    }
    public void chroma()
    {
      
        int index = 8;
        if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text == "Buy")
        {
            if (db.para >= 3000)
            {
                db.para -= 3000;
                txt_para.text = db.para.ToString();

              
           
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "Use";
                db.shop_text[index] = "Use";
                db.instance.shop_text_textleri_degis();
            }
            else
            {
                canvas_parayok.gameObject.SetActive(true);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text == "Use")
        {
            if (db.mod!="random")
            {
                db.mod = "random";
                db.instance.mod_guncelle();
            }
            
            GameObject go = GameObject.FindWithTag(db.aktif_tag.ToString());
            db.color_list_index = 0;
            go.GetComponentInChildren<Text>().text = "Use";
            db.shop_text[db.aktif_tag] = "Use";
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "Active";
            db.shop_text[index] = "Active";
            db.aktif_tag = index;
            db.instance.shop_text_textleri_degis();
            db.instance.mod_guncelle();
        }
    }
	void Update () {
        ad_manager.instance.banner_hide();
	}
}
