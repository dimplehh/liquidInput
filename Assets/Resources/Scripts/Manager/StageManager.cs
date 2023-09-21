using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private const string LAST_STAGE_KEY = "LAST_STAGE"; //현재 스테이지 저장 키
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

        //최종 스테이지 초기화 (1스테이지로 초기화 할거면 주석 풀고 실행하고 다시 주석 처리하면 됨)
        //PlayerPrefs.SetInt(LAST_STAGE_KEY, 1);
        //Debug.Log("초기화");
        lastStageIndex = PlayerPrefs.GetInt(LAST_STAGE_KEY);

        //완전 기초정보 저장 
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

    public void LastStageUp() //챕터 클리어 시 라스트 스테이지 번호를 저장 
    {
        if (currentStageIndex >= lastStageIndex)
        {
            lastStageIndex = currentStageIndex;
            PlayerPrefs.SetInt(LAST_STAGE_KEY, lastStageIndex);
        }
    }

    public void DataDelete() //중간발표 때 시연 - 데이터 삭제 편하게 하기 위해
    {
        for(int i=0; i<4; i++)
        {
            File.Delete(Managers.Data.path + i.ToString());
        }
    }
}
