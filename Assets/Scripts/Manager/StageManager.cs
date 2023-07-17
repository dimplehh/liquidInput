using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private const string CURRENT_STAGE_KEY = "CURRENT_STAGE"; //현재 스테이지 저장 키
    public int currentStageIndex; //현재 스테이지 번호
    public int lastStageIndex; //최종 스테이지 번호
    
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
    
    //public void LastStageUp() //챕터 클리어 시 라스트 스테이지 번호를 저장 
    //{
    //    if (currentStageIndex >= lastStageIndex)
    //    {
    //        lastStageIndex = currentStageIndex;
    //        PlayerPrefs.SetInt(CURRENT_STAGE_KEY, lastStageIndex);
    //    }
    //}
}
