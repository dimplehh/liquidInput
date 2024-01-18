using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;


public class SteamIntegration : MonoBehaviour
{
    public CGameID m_GameID;
    public AppId_t appID;

    private void Start()
    {
        appID = SteamUtils.GetAppID();
        m_GameID = new CGameID(SteamUtils.GetAppID());
        //DOST();
    }

    private void Update()
    {
        if(Input.GetKeyDown("a"))
        {
            SteamUserStats.SetAchievement("Test1");
        }
    }
}
