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

        //���� �������� �ʱ�ȭ (1���������� �ʱ�ȭ �ҰŸ� �ּ� Ǯ�� �����ϰ� �ٽ� �ּ� ó���ϸ� ��)
        //PlayerPrefs.SetInt(LAST_STAGE_KEY, 1);
        //Debug.Log("�ʱ�ȭ");
        lastStageIndex = PlayerPrefs.GetInt(LAST_STAGE_KEY);

        //���� �������� ���� 
        //Managers.Data.DefaultSaveData();
    }

    private void Start()
    {
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
            File.Delete(Managers.Data.path + i.ToString());
        }
    }
}
