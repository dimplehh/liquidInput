using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;


public class SteamIntegration : MonoBehaviour
{
    public CGameID m_GameID;
    public AppId_t appID;
    [Header("Utils")]
    [SerializeField] GameObject firstObstacle;
    [SerializeField] GameObject clearZone;

    private void Start()
    {
        appID = SteamUtils.GetAppID();
        m_GameID = new CGameID(SteamUtils.GetAppID());
        //SetEverything();
    }

    private void Update()
    {
        int deathCount = GameManager.instance.deathCount;
        switch (deathCount)
        {
            case 10:
                GetAndSetAchievemt("Repechage");
                break;
            case 50:
                GetAndSetAchievemt("Unbroken_Heart");
                break;
            case 100:
                GetAndSetAchievemt("Liquid_Never_Die");
                break;
            default:
                break;
        }
        if(firstObstacle != null) //Are You Serious?
        {
            Obstacle obstacle = firstObstacle.GetComponent<Obstacle>();
            if (obstacle.isTriggerEnter != false)
                GetAndSetAchievemt("Are_You_Serious");
        }
        if(clearZone != null)
        {
            ClearZone ClearZone = clearZone.GetComponent<ClearZone>();
            if(ClearZone.oneAct == true)
            {
                switch (StageManager.instance.currentStageIndex)
                {
                    case 2:
                        GetAndSetAchievemt("In_A_Ruined_World");
                        break;
                    case 3:
                        GetAndSetAchievemt("Defunct_Civilization");
                        break;
                    case 4:
                    {
                        switch (ClearZone.endingIndex)
                        {
                            case 0:
                                GetAndSetAchievemt("Pollution");
                                break;
                            case 1:
                                if (deathCount == 0) GetAndSetAchievemt("Immortal_Liquid");
                                GetAndSetAchievemt("Unbelief");
                                break;
                            case 2:
                                if (deathCount == 0) GetAndSetAchievemt("Immortal_Liquid");
                                GetAndSetAchievemt("Blend_Into_The_World");
                                break;
                            case 3:
                                if (deathCount == 0)
                                {
                                    GetAndSetAchievemt("Immortal_Liquid");
                                    GetAndSetAchievemt("GOAT");
                                }
                                GetAndSetAchievemt("And_So_It_Begins");
                                break;
                        }
                            break;
                    }
                }
            }
        }
    }

    void GetAndSetAchievemt(string name)
    {
        bool isAchieved = SteamUserStats.GetAchievement(name, out bool achieved);
        if (isAchieved)
        {
            if (!achieved)
            {
                SteamUserStats.SetAchievement(name);
                Debug.Log("Achieve '" + name + "'");
            }
        }
    }
}