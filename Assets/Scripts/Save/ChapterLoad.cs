using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        LoadingSceneController.Instance.LoadScene("GameScene");       
    }

    
}
