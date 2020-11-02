using System.Data;
using System;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;

public class db : MonoBehaviour {


    private SqliteConnection conn;
    private SqliteCommand cmd;
    private SqliteDataReader reader;
    private string path;

    public static int rekor;
    public static int color_list_index = 0;
    public static string mod = "tekli";//random || tekli
    public static int para=999999;
    public static string[] shop_text = new string[9];
    public static int aktif_tag = 0;
    public static int olumsayaci = 0;


    public static string egitim;
    public static db instance ;
    private void Awake()
    {
        instance = this;    
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                path = Application.dataPath + "/StreamingAssets/database.sqlite";
            }          
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                path = Application.dataPath + "/Raw" + "/database.sqlite";

            }
            else if (Application.platform == RuntimePlatform.Android)
            {


                // check if file exists in Application.persistentDataPath
                path = Application.persistentDataPath + "/database.sqlite";
                if (!File.Exists(path))
                {
                    Debug.LogWarning("File \"" + path + "\" does not exist. Attempting to create from \"" +
                                     Application.dataPath + "!/assets/dbtestt.sqlite");
                    // if it doesn't ->
                    // open StreamingAssets directory and load the db -> 
                    WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/database.sqlite");
                    while (!loadDB.isDone) { }
                    // then save to Application.persistentDataPath
                    File.WriteAllBytes(path, loadDB.bytes);
                }

            }
            conn = new SqliteConnection("URI=file:" + path);
        para_cek();
        shop_text_cek();
        mod_cek();
        egitim_cek();
        rekor_cek();
        Debug.Log(shop_text[3]);
     
        DontDestroyOnLoad(gameObject);
    }
  public void egitim_cek()
    {
        try
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("select * from egitim",conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                egitim = reader.GetString(0);
            }
            Debug.Log(egitim.ToString());
            conn.Close();
        }
        catch (Exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            throw;


        }
    }
    public void egitim_guncelle()
    {
        try
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("update egitim set durum='yapildi' ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            egitim_cek();
        }
        catch (Exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            throw;


        }

    }
    public void  para_cek()
    {
        try
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("select * from para",conn); 
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                para = reader.GetInt32(0);  
            }
            conn.Close();
         //   Debug.Log(para.ToString()+"ez");
        }
        catch (System.Exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            throw;
        }
    }
    public void para_sil(int miktar)
    {
        try
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("update para set para=" + (para - miktar) + "", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            para_cek();
        }
        catch (System.Exception)
        {
            if (conn.State==ConnectionState.Open)
            {
                conn.Close();
            }
            throw;
        }
    }
    public void para_ekle(int miktar)
    {
        try
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("update para set para="+(para+miktar)+"",conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            para += miktar;
        }
        catch (System.Exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            throw;
        }
    }
    public void rekor_cek()
    {
        try
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("select * from skor", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rekor = reader.GetInt32(0);
            }
            conn.Close();
            Debug.Log(para.ToString() + "ez");
        }
        catch (System.Exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            throw;
        }
    }
    public void rekor_güncelle(int yeni_rekor)
    {
        try
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("update  skor set rekor="+yeni_rekor+" ", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                para = reader.GetInt32(0);
            }
            conn.Close();
            rekor = yeni_rekor;
        }
        catch (System.Exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            throw;
        }
    }
    public void shop_text_cek()
    {
        try
        {
          
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("select * from shop", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < 9; i++)
                {
                    shop_text[i] = reader.GetString(i);
                }
                
                
            }
            conn.Close();
            Debug.Log(para.ToString() + "ez");
        }
        catch (System.Exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            throw;
        }
    }
    public void shop_text_textleri_degis()
    {
        try
        {

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("update  shop  set index1='"+shop_text[0]+ "',index2='" + shop_text[1] + "',index3='" + shop_text[2] + "',index4='" + shop_text[3] + "',index5='" + shop_text[4] + "',index6='" + shop_text[5] + "',index7='" + shop_text[6] + "',index8='" + shop_text[7] + "',index9='" + shop_text[8] + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Debug.Log(para.ToString() + "ez");
        }
        catch (System.Exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            throw;
        }
    }
    public void mod_cek()
    {
        try
        {
            if (conn.State==ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("select * from RenkBilgi ",conn);
           reader=cmd.ExecuteReader();
            while (reader.Read())
            {
                mod = reader.GetString(0);
                color_list_index = reader.GetInt32(1);
                aktif_tag = reader.GetInt32(2);
            }
        }
        catch (Exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            throw;
        }
    }
    public void mod_guncelle()
    {
        try
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new SqliteCommand("update RenkBilgi set mod='"+mod+"',color_index="+color_list_index+",aktif_tag="+aktif_tag+" ", conn);
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            throw;
        }
    }
}
