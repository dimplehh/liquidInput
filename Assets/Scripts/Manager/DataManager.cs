using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public string dataName;
    public float playerXPos; //플레이어 x축 위치
    public float playerWaterReserves; //플레이어 물 보유량
    public float mapXPos; //맵 x축 위치
}
//public class MapData
//{
    
//    //진행사항 퍼센트
//}
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
       
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }
    public void LoadData()
    {
       string data = File.ReadAllText(path + nowSlot.ToString());
       playerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        playerData = new PlayerData();
    }
}
