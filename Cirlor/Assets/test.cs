using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class test : MonoBehaviour
{
    public Camera cam;
    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        T();

    }
    public void T()
    {
        Social.localUser.Authenticate((bool success) => {
            if (success)
                cam.backgroundColor = Color.green;
            else
                cam.backgroundColor = Color.red;

        });
    }
}
