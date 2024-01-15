using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private const string LAST_STAGE_KEY = "LAST_STAGE"; //현재 스테이지 저장 키
    public int currentStageIndex; //현재 스테이지 번호
    public int lastStageIndex; //최종 스테이지 번호
    
    //스테이지 오브젝트들
    [SerializeField]
    private Water[] waterList;
    [SerializeField]
    private Npc[] npcList;
    [SerializeField]
    private SaveZone[] saveZoneList;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

        //최종 스테이지 초기화(1스테이지로 초기화 할거면 주석 풀고 실행하고 다시 주석 처리하면 됨)
        //PlayerPrefs.SetInt(LAST_STAGE_KEY, 1);
        //Debug.Log("초기화");
        lastStageIndex = PlayerPrefs.GetInt(LAST_STAGE_KEY);

        //완전 기초정보 저장 
        Managers.Data.DefaultSaveData();
    }

    public void FindStageObjects()
    {
        waterList = FindObjectsOfType<Water>();
        npcList = FindObjectsOfType<Npc>();
        saveZoneList = FindObjectsOfType<SaveZone>();
    }
    public StageData GetStageData(int stageChapter)
    {
        StageData stageData = new StageData();

        stageData.stageChapter = stageChapter;
        foreach (var data in waterList)
        {
            WaterData waterData = new WaterData {id = data.id, waterReserves = data.currentWaterReserves};
            stageData.waterData.Add(waterData);
        }
        
        foreach (var data in npcList)
        {
            NpcData npcData = new NpcData {id = data.id, interaction = data.isInteraction};
            stageData.npcData.Add(npcData);
        }
        
        foreach (var data in saveZoneList)
        {
            SaveZoneData saveZoneData = new SaveZoneData {id = data.id, isSave = data.isSave};
            stageData.saveZoneData.Add(saveZoneData);
        }
        
        return stageData;
    }

    public void UpdateStageData()
    {
        var waterData= Managers.Data.stageData.waterData;
        var npcData = Managers.Data.stageData.npcData;
        var saveZoneData = Managers.Data.stageData.saveZoneData;
        if(waterList.Length > 0)
        {
            for (int i = 0; i < waterData.Count; i++)
            {
                for(int j = 0; j < waterList.Length; j++)
                {
                    if (waterList[j].id == waterData[i].id)
                    {
                        waterList[j].currentWaterReserves = waterData[i].waterReserves;
                        waterList[j].UpdateWater();
                    }
                }
            }
        }
        
        if(npcList.Length > 0)
        {
            for (int i = 0; i < npcData.Count; i++)
            {
                if (npcList[i].id == npcData[i].id)
                {
                    npcList[i].isInteraction = npcData[i].interaction;
                    npcList[i].UpdateNpcData();
                }
            }
        }
        
        if(saveZoneList.Length > 0)
        {
            for (int i = 0; i < saveZoneData.Count; i++)
            {
                if (saveZoneList[i].id == saveZoneData[i].id)
                {
                    var isSave = saveZoneData[i].isSave;
                    saveZoneList[i].isSave = isSave;
                    if (isSave)
                    {
                        saveZoneList[i].anim.enabled = false;
                        saveZoneList[i].anim.SetBool("Active", isSave);
                        saveZoneList[i].Save();
                    }
                }
            }
        }
    }

    public void UpdateSave(SaveZone saveZone)
    {
        foreach (var data in saveZoneList)
        {
            if (data != saveZone)
            {
                data.DeSave();
            }
        }
    }

    public bool LastStageCheck(int index)
    {
        if (lastStageIndex >= index)
        {
            return true;
        }
        return false;
    }

    public void LastStageUp() //챕터 클리어 시 라스트 스테이지 번호를 저장 
    {
        if (currentStageIndex >= lastStageIndex)
        {
            lastStageIndex = currentStageIndex;
            PlayerPrefs.SetInt(LAST_STAGE_KEY, lastStageIndex);
        }
    }

    public void DataDelete() //중간발표 때 시연 - 데이터 삭제 편하게 하기 위해
    {
        for(int i=0; i<4; i++)
        {
            File.Delete(Managers.Data.path + "PlayerData" + i.ToString());
            File.Delete(Managers.Data.path + "StageData" +i.ToString());
        }
    }
}
