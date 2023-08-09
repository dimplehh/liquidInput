using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterLoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] private ChapterLoad chapterLoad; //é�ͺ� �ҷ�����
    [SerializeField] private int chapLoadindex = 1; //é�ͺ� �ε���
    [SerializeField] private RectTransform selectCheckImage; //���� Ȯ�� �̹���
    [SerializeField] private RectTransform[] selectPos; //�����̹��� ��ġ

    private void Start()
    {
        //selectCheckImage.position = selectPos[0].position;
    }
    private void OnEnable()
    {
        chapLoadindex = 1;
        //selectCheckImage.position = selectPos[0].position;
    }
    private void Update()
    {
        LoadKeyBoardClick();
        ExitButton();
    }
    private void ExitButton()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void LoadKeyBoardClick()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (chapLoadindex >= 4)
            {
                chapLoadindex = 4;
                selectCheckImage.position = selectPos[3].position;
                
            }
            else
            {
                chapLoadindex++;
                selectCheckImage.position = selectPos[chapLoadindex - 1].position;
            }
  
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (chapLoadindex <= 1)
            {
                chapLoadindex = 1;
                selectCheckImage.position = selectPos[0].position;
            }
            else
            {
                chapLoadindex--;
                selectCheckImage.position = selectPos[chapLoadindex - 1].position;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            chapterLoad.ChapterLoadButton(chapLoadindex);
        }
    }
}
