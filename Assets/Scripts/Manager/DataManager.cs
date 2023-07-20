using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    //초기값 설정//
    public float playerXPos = 58.0f; //플레이어 x축 위치
    public float playerWaterReserves = 5; //플레이어 물 보유량
    public int currentStage = 1;  //현재 스테이지 정보
    public int index; //저장 위치
    public float goodGauge; //선행게이지
}
public class GameData
{
   
    //진행사항 퍼센트
}
public class DataManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();
    //MapData mapData = new MapData();
    public string path;
    public int nowSlot; //현재 슬롯 번호
    private void Awake()
    {
        path = Application.persistentDataPath + "/";
        
    }
    private void Start()
    {
        //DefaultSaveData(); //기본정보 저장
    }
    //슬롯 별 저장 / 불러오기
    public void SlotSaveData(int index, GameObject pos, int stage, int curWater)
    {
        playerData.playerXPos = pos.transform.position.x;
        playerData.currentStage = stage;
        playerData.playerWaterReserves = curWater;
        playerData.index = index;
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + index.ToString(), data);
        nowSlot = index;
    }
    public void SlotLoadData(int index)
    {
        string data = File.ReadAllText(path + index.ToString());
        playerData = JsonUtility.FromJson<PlayerData>(data);
    }
    //기본정보들 저장 / 불러오기
    public void DefaultSaveData()
    {
        playerData.playerWaterReserves = 5; //물보유량
        playerData.currentStage = 1; //스테이지
        playerData.playerXPos = -13.0f;
        //저장할것들....추가하면 됨
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + "DefaultPlayerData", data);
    }
    public void DefaultLoadData()
    {
        string defaultData = File.ReadAllText(path + "DefaultPlayerData");
        playerData = JsonUtility.FromJson<PlayerData>(defaultData);
    }

    //챕터별 저장 / 불러오기
    public void StageSaveData(int curWater, int stage) 
    {
        playerData.playerWaterReserves = curWater; //물보유량
        //저장할것들....추가하면 됨
        string stageData = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + "StagePlayerData" + stage.ToString(), stageData);
    }
    public void StageLoadData(int stage)
    {
        string stageData = File.ReadAllText(path + "StagePlayerData" + stage.ToString());
        playerData = JsonUtility.FromJson<PlayerData>(stageData);
    }
}
