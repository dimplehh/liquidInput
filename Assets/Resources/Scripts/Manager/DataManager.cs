using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class PlayerData
{
    //초기값 설정//
    public float playerXPos = -10.0f; //플레이어 x축 위치
    public float playerYPos = 0.0f; //플레이어 y축 위치
    public float playerWaterReserves = 5; //플레이어 물 보유량
    public int currentStage = 1;  //현재 스테이지 정보
    public int index; //저장 위치
    public float goodGauge; //선행게이지
    public float playTime = 0; //플레이 시간
    public string saveDate = ""; //저장날짜
    public int deathCount = 0;
    public bool isFirst = true; //새로시작하면 컷신 나오게
}

[Serializable]
public class StageData
{
    //스테이지 오브젝트들
    public int stageChapter;
    public List<WaterData> waterData = new List<WaterData>();
    public List<NpcData> npcData = new List<NpcData>();
    public List<SaveZoneData> saveZoneData = new List<SaveZoneData>();
}


public class GameData
{
    //진행사항 퍼센트
}
public class DataManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();
    public StageData stageData = new StageData();
    //MapData mapData = new MapData();
    public string path;
    public int nowSlot; //현재 슬롯 번호
    
    public bool isClear = false;
    
    private void Awake()
    {
        path = Application.persistentDataPath + "/";
        
    }
    //슬롯 별 저장 / 불러오기
    public void SlotSaveData(int index, GameObject pos, int stage, int curWater, float playTime, int deathCount)
    {
        #region PLAYERDATA
        playerData.playerXPos = pos.transform.position.x;
        playerData.playerYPos = pos.transform.position.y;
        playerData.currentStage = stage;
        playerData.goodGauge = GameManager.instance.successGauge;
        playerData.deathCount = deathCount;
        if (curWater <= 0) //저장된 물 갯수가 0보다 작으면 1으로 초기화
        {
            playerData.playerWaterReserves = 1;
        }
        else
        {
            playerData.playerWaterReserves = curWater;
        }
        
        playerData.index = index;
        playerData.playTime = playTime;
        playerData.isFirst = false;
        playerData.saveDate = DateTime.Now.ToString("yyyy MM dd");
        #endregion

        #region STAGEDATA
        stageData = StageManager.instance.GetStageData(stage);
        #endregion

        string data = JsonUtility.ToJson(playerData);
        string stageDataForm = JsonConvert.SerializeObject(stageData);

        // File.WriteAllText(path + index.ToString(), data);
        File.WriteAllText(path + "PlayerData" + index.ToString(), data);
        File.WriteAllText(path + "StageData" +index.ToString(), stageDataForm);
        nowSlot = index;
    }

    public void SlotSaveData(int index, StageData stageData)
    {
        #region PLAYERDATA
        playerData.currentStage = stageData.stageChapter;
        playerData.isFirst = false;
        playerData.saveDate = DateTime.Now.ToString("yyyy MM dd");
        #endregion

        string data = JsonUtility.ToJson(playerData);
        string stageDataForm = JsonConvert.SerializeObject(stageData);

        File.WriteAllText(path + "PlayerData" + index.ToString(), data);
        File.WriteAllText(path + "StageData" + index.ToString(), stageDataForm);
        nowSlot = index;
    }

    public void SlotSaveData()
    {
        #region PLAYERDATA
        playerData.deathCount++;
        #endregion

        string data = JsonUtility.ToJson(playerData);

        File.WriteAllText(path + "PlayerData0", data);
    }

    public void SlotLoadData(int index)
    {
        // string data = File.ReadAllText(path + index.ToString());
        string data = File.ReadAllText(path + "PlayerData" + index.ToString());
        string stageDataForm = File.ReadAllText(path + "StageData" + index.ToString());

        stageData = JsonConvert.DeserializeObject<StageData>(stageDataForm);
        playerData = JsonUtility.FromJson<PlayerData>(data);

        if (StageManager.instance)
        {
            StageManager.instance.currentStageIndex = stageData.stageChapter;
        }
    }

    public PlayerData GetSlotPlayerData(int i)
    {
        if (!File.Exists(Managers.Data.path + "PlayerData" + i))
        {
            return null;
        }
        string data = File.ReadAllText(path + "PlayerData" + i);
        var slotPlayerData = JsonUtility.FromJson<PlayerData>(data);
        return slotPlayerData;
    }
    
    public StageData GetSlotStageData(int i)
    {
        if (!File.Exists(Managers.Data.path + "StageData" + i))
        {
            return null;
        }
        string data = File.ReadAllText(path + "StageData" + i);
        var slotStageData = JsonUtility.FromJson<StageData>(data);
        return slotStageData;
    }
    
    //기본정보들 저장 / 불러오기
    public void DefaultSaveData()
    {
        if (File.Exists(Managers.Data.path + "DefaultPlayerData"))
        {
            Debug.Log("DefaultPlayerData");
            return;
        }
        playerData.playerWaterReserves = 5; //물보유량
        playerData.currentStage = 1; //스테이지
        playerData.playerXPos = -20f; //첫 시작 위치
        playerData.playerYPos = 0f; //첫 시작 위치
        playerData.playTime = 0; //플레이 타임
        playerData.saveDate = "";
        playerData.deathCount = 0;
        playerData.isFirst = true;
        //저장할것들....추가하면 됨
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + "DefaultPlayerData", data);
    }
    public void DefaultLoadData()
    {
        string defaultPlayerData = File.ReadAllText(path + "DefaultPlayerData");
        playerData = JsonUtility.FromJson<PlayerData>(defaultPlayerData);
    }

    //챕터별 저장 / 불러오기
    public void StageClearSaveData(int curWater, int stage)
    {
        playerData.playerWaterReserves = curWater; //물보유량
        playerData.goodGauge = GameManager.instance.successGauge;
        //playerData.deathCount = 

        //스테이지 클리어 후 초기화 시켜서 저장해야 할 것들
        playerData.playerXPos = -20f; //첫 시작 위치
        playerData.playerYPos = 0f; //첫 시작 위치

        stageData.stageChapter = stage;
        
        //저장할것들....추가하면 됨
        string playerDataForm = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + "PlayerData0", playerDataForm);
    }
    public void StageLoadData(int stage)
    {
        string playerDataForm = File.ReadAllText(path + "PlayerData0");
        playerData = JsonUtility.FromJson<PlayerData>(playerDataForm);
    }

    //챕터 이동후에만 작동
    public void ClearStageData(int chapter)
    {
        stageData = StageManager.instance.GetStageData(chapter);
        string stageDataForm = JsonConvert.SerializeObject(stageData);
        File.WriteAllText(path + "StageData" +0, stageDataForm);
    }
}
