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
    [SerializeField] GameObject[] hiddenWater;

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

        switch(GameManager.instance.successGauge)
        {
            case 1:
                GetAndSetAchievemt("Good_Deed");
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                GetAndSetAchievemt("Encounter_God");
                break;
        }

        if (GameManager.instance.waterDie == true) 
            GetAndSetAchievemt("Sink_Like_A_Rock");

        if(hiddenWater.Length != 0)
        {
            if (hiddenWater[0].GetComponent<ShallowWater>().getWater == true)
                GetAndSetAchievemt("Water_here_Too");
            if (hiddenWater[1].GetComponent<FloodWater>().getWater == true)
                GetAndSetAchievemt("Here_Too");
            if (hiddenWater[2].GetComponent<FloodWater>().getWater == true)
                GetAndSetAchievemt("No_way_Here_Too");
        }

        if(firstObstacle != null)
        {
            Obstacle obstacle = firstObstacle.GetComponent<Obstacle>();
            if (obstacle.isTriggerEnter != false)
            {
                if(obstacle.obstacleNum == 1)
                    GetAndSetAchievemt("Are_You_Serious");
                if(obstacle.obstacleNum == 2)
                    GetAndSetAchievemt("Plants_Like_Water");
            }
        }

        if (IsAllSave())
            GetAndSetAchievemt("Grow_Fast");

        if (clearZone != null)
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
                                    if (IsNoSave() == true && deathCount == 0) 
                                        GetAndSetAchievemt("You_Only_Live_Once");
                                break;
                            case 1:
                                if (deathCount == 0){ 
                                        GetAndSetAchievemt("Immortal_Liquid");
                                        if (IsNoSave() == true) 
                                            GetAndSetAchievemt("You_Only_Live_Once");
                                }
                                GetAndSetAchievemt("Unbelief");
                                break;
                            case 2:
                                    if (deathCount == 0)
                                    {
                                        GetAndSetAchievemt("Immortal_Liquid");
                                        if (IsNoSave() == true)
                                            GetAndSetAchievemt("Doing_Impossible_Thing");
                                    }
                                GetAndSetAchievemt("Blend_Into_The_World");
                                break;
                            case 3:
                                if (deathCount == 0)
                                {
                                    GetAndSetAchievemt("Immortal_Liquid");
                                    GetAndSetAchievemt("GOAT");
                                    if (IsNoSave() == true)
                                        GetAndSetAchievemt("Real_God");

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

    bool IsAllSave()
    {
        if (Managers.Data.playerData.allSave[2] == true && Managers.Data.playerData.allSave[1] == true && Managers.Data.playerData.allSave[0] == true)
        {
            return true;
        }
        return false;
    }

    bool IsNoSave()
    {
        if (Managers.Data.playerData.NoSave[1] == true && Managers.Data.playerData.NoSave[0] == true)
        {
            for (int i = 0; i < Managers.Data.stageData.saveZoneData.Count; i++)
                if (Managers.Data.stageData.saveZoneData[i].isSave == true)
                    return false;
            return true;
        }
        return false;
    }
}