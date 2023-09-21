using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] lockImage; //챕터 잠금 이미지

    private void LockImageCheck()
    {
        for (int i = 0; i < StageManager.instance.lastStageIndex - 1; i++)
        {
            lockImage[i].SetActive(false);
        }
    }
    private void Start()
    {
        LockImageCheck();
    }

    public void ChapterLoadButton(int index)
    {
        if (!StageManager.instance.LastStageCheck(index))
            return;

        StageManager.instance.currentStageIndex = index;
        
        if(index <= 1)
        {
            Managers.Data.DefaultLoadData();
            Debug.Log("1 챕터니까 기본정보 불러올게");
        }
        else
            Managers.Data.StageLoadData(index); //다음 챕터로 넘어오기전 마지막 데이터 불러오기

        LoadingSceneController.Instance.LoadScene("GameScene");       
    }
   
   
}
