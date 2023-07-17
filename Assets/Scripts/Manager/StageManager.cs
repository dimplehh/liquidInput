using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void Start()
    {
        //���� �������� �ʱ�ȭ (1���������� �ʱ�ȭ �ҰŸ� �ּ� Ǯ�� �����ϰ� �ٽ� �ּ� ó���ϸ� ��)
        PlayerPrefs.SetInt(LAST_STAGE_KEY, 1);
        
        lastStageIndex = PlayerPrefs.GetInt(LAST_STAGE_KEY);
        
        //���� �������� ���� 
        //Managers.Data.DefaultSaveData();
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
}
