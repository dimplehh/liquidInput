using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public float playerXPos; //플레이어 x축 위치
    public float playerWaterReserves; //플레이어 물 보유량
    public float mapXPos; //맵 x축 위치
    public int currentStage; //현재 스테이지 정보
    public int index; //저장 위치
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

    public void SaveData(int index, GameObject pos, int stage, int curWater)
    {
        playerData.playerXPos = pos.transform.position.x;
        playerData.currentStage = stage;
        playerData.playerWaterReserves = curWater;
        playerData.index = index;
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + index.ToString(), data);
        nowSlot = index;
    }
    public void LoadData(int index)
    {
       string data = File.ReadAllText(path + index.ToString());
       playerData = JsonUtility.FromJson<PlayerData>(data);
    }
}
