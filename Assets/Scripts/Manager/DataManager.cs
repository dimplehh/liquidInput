using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public string dataName;
    public float playerXPos; //�÷��̾� x�� ��ġ
    public float playerWaterReserves; //�÷��̾� �� ������
    public float mapXPos; //�� x�� ��ġ
}
//public class MapData
//{
    
//    //������� �ۼ�Ʈ
//}
public class DataManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();
    //MapData mapData = new MapData();
    public string path;
    public int nowSlot; //���� ���� ��ȣ
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
