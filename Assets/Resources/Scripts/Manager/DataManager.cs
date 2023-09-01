using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class PlayerData
{
    //�ʱⰪ ����//
    public float playerXPos = 0.0f; //�÷��̾� x�� ��ġ
    public float playerYPos = 0.0f; //�÷��̾� y�� ��ġ
    public float playerWaterReserves = 5; //�÷��̾� �� ������
    public int currentStage = 1;  //���� �������� ����
    public int index; //���� ��ġ
    public float goodGauge; //���������
    public float playTime = 0; //�÷��� �ð�
    public string saveDate = ""; //���峯¥

    public bool isFirst = true; //���ν����ϸ� �ƽ� ������
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
    //���� �� ���� / �ҷ�����
    public void SlotSaveData(int index, GameObject pos, int stage, int curWater, float playTime)
    {
        playerData.playerXPos = pos.transform.position.x;
        playerData.playerYPos = pos.transform.position.y;
        playerData.currentStage = stage;
        if (curWater <= 0) //����� �� ������ 0���� ������ 1���� �ʱ�ȭ
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
    //�⺻������ ���� / �ҷ�����
    public void DefaultSaveData()
    {
        playerData.playerWaterReserves = 5; //��������
        playerData.currentStage = 1; //��������
        playerData.playerXPos = -20f; //ù ���� ��ġ
        playerData.playerYPos = 0f; //ù ���� ��ġ
        playerData.playTime = 0; //�÷��� Ÿ��
        playerData.saveDate = "";
        playerData.isFirst = true;
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
