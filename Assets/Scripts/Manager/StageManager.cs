using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private const string CURRENT_STAGE_KEY = "CURRENT_STAGE"; //���� �������� ���� Ű
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
        PlayerPrefs.SetInt(CURRENT_STAGE_KEY, 1);
        //lastStageIndex = PlayerPrefs.GetInt(CURRENT_STAGE_KEY);
        
    }

    
   
    void Update()
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
    
    //public void LastStageUp() //é�� Ŭ���� �� ��Ʈ �������� ��ȣ�� ���� 
    //{
    //    if (currentStageIndex >= lastStageIndex)
    //    {
    //        lastStageIndex = currentStageIndex;
    //        PlayerPrefs.SetInt(CURRENT_STAGE_KEY, lastStageIndex);
    //    }
    //}
}
