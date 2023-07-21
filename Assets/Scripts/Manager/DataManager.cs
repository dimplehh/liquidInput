using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    //�ʱⰪ ����//
    public float playerXPos = 58.0f; //�÷��̾� x�� ��ġ
    public float playerWaterReserves = 5; //�÷��̾� �� ������
    public int currentStage = 1;  //���� �������� ����
    public int index; //���� ��ġ
    public float goodGauge; //���������
}
public class GameData
{
   
    //������� �ۼ�Ʈ
}
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
        //DefaultSaveData(); //�⺻���� ����
    }
    //���� �� ���� / �ҷ�����
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
    //�⺻������ ���� / �ҷ�����
    public void DefaultSaveData()
    {
        playerData.playerWaterReserves = 5; //��������
        playerData.currentStage = 1; //��������
        playerData.playerXPos = -13.0f;
        //�����Ұ͵�....�߰��ϸ� ��
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + "DefaultPlayerData", data);
    }
    public void DefaultLoadData()
    {
        string defaultData = File.ReadAllText(path + "DefaultPlayerData");
        playerData = JsonUtility.FromJson<PlayerData>(defaultData);
    }

    //é�ͺ� ���� / �ҷ�����
    public void StageSaveData(int curWater, int stage) 
    {
        playerData.playerWaterReserves = curWater; //��������
        //�����Ұ͵�....�߰��ϸ� ��
        string stageData = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + "StagePlayerData" + stage.ToString(), stageData);
    }
    public void StageLoadData(int stage)
    {
        string stageData = File.ReadAllText(path + "StagePlayerData" + stage.ToString());
        playerData = JsonUtility.FromJson<PlayerData>(stageData);
    }
}
