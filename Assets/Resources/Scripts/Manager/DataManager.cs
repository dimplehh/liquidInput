using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class PlayerData
{
    //초기값 설정//
    public float playerXPos = 0.0f; //플레이어 x축 위치
    public float playerYPos = 0.0f; //플레이어 y축 위치
    public float playerWaterReserves = 5; //플레이어 물 보유량
    public int currentStage = 1;  //현재 스테이지 정보
    public int index; //저장 위치
    public float goodGauge; //선행게이지
    public float playTime = 0; //플레이 시간
    public string saveDate = ""; //저장날짜

    public bool isFirst = true; //새로시작하면 컷신 나오게
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
    //슬롯 별 저장 / 불러오기
    public void SlotSaveData(int index, GameObject pos, int stage, int curWater, float playTime)
    {
        playerData.playerXPos = pos.transform.position.x;
        playerData.playerYPos = pos.transform.position.y;
        playerData.currentStage = stage;
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
        playerData.playerXPos = -20f; //첫 시작 위치
        playerData.playerYPos = 0f; //첫 시작 위치
        playerData.playTime = 0; //플레이 타임
        playerData.saveDate = "";
        playerData.isFirst = true;
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
