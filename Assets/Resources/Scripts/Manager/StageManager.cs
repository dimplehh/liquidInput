using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private const string LAST_STAGE_KEY = "LAST_STAGE"; //���� �������� ���� Ű
    public int currentStageIndex; //���� �������� ��ȣ
    public int lastStageIndex; //���� �������� ��ȣ
    
    //�������� ������Ʈ��
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

        //���� �������� �ʱ�ȭ(1���������� �ʱ�ȭ �ҰŸ� �ּ� Ǯ�� �����ϰ� �ٽ� �ּ� ó���ϸ� ��)
        //PlayerPrefs.SetInt(LAST_STAGE_KEY, 1);
        //Debug.Log("�ʱ�ȭ");
        lastStageIndex = PlayerPrefs.GetInt(LAST_STAGE_KEY);

        //���� �������� ���� 
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

    public void LastStageUp() //é�� Ŭ���� �� ��Ʈ �������� ��ȣ�� ���� 
    {
        if (currentStageIndex >= lastStageIndex)
        {
            lastStageIndex = currentStageIndex;
            PlayerPrefs.SetInt(LAST_STAGE_KEY, lastStageIndex);
        }
    }

    public void DataDelete() //�߰���ǥ �� �ÿ� - ������ ���� ���ϰ� �ϱ� ����
    {
        for(int i=0; i<4; i++)
        {
            File.Delete(Managers.Data.path + "PlayerData" + i.ToString());
            File.Delete(Managers.Data.path + "StageData" +i.ToString());
        }
    }
}
