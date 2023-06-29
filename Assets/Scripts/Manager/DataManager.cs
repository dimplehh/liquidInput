using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public float playerXPos; //�÷��̾� x�� ��ġ
    public float playerWaterReserves; //�÷��̾� �� ������
    public float mapXPos; //�� x�� ��ġ
    public int currentStage;
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

    public void SaveData(int index, GameObject pos, int stage, int curWater)
    {
        playerData.playerXPos = pos.transform.position.x + 2f;
        playerData.currentStage = stage+1;
        playerData.playerWaterReserves = curWater;
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
