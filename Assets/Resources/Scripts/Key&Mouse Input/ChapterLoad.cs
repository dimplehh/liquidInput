using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] lockImage; //é�� ��� �̹���

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
            Debug.Log("1 é�ʹϱ� �⺻���� �ҷ��ð�");
        }
        else
            Managers.Data.StageLoadData(index); //���� é�ͷ� �Ѿ������ ������ ������ �ҷ�����

        LoadingSceneController.Instance.LoadScene("GameScene");       
    }
   
   
}
