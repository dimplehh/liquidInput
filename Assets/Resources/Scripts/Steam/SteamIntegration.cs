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
        switch (GameManager.instance.deathCount)
        {
            case 10:
                SteamUserStats.SetAchievement("Repechage");
                break;
            case 50:
                SteamUserStats.SetAchievement("Unbroken_Heart");
                break;
            case 100:
                SteamUserStats.SetAchievement("Liquid_Never_Die");
                break;
            default:
                break;
        }
    }
}
