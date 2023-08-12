using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterLoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] private ChapterLoad chapterLoad; //é�ͺ� �ҷ�����
    [SerializeField] private int chapLoadindex = 1; //é�ͺ� �ε���
    [SerializeField] private RectTransform selectCheckImage; //���� Ȯ�� �̹���
    [SerializeField] private RectTransform[] selectPos; //�����̹��� ��ġ
    [Header("Ű���� �̵� ��ġ")]
    [SerializeField] private RectTransform buttonGroup;
    [SerializeField] private RectTransform initButtonGroup; //�ʱ�ȭ ��ġ

    private void Start()
    {
        initButtonGroup.position = buttonGroup.position;
    }
    private void OnEnable()
    {
        chapLoadindex = 1;
    }
    private void OnDisable()
    {
        buttonGroup.position = initButtonGroup.position;
        selectCheckImage.position = selectPos[0].position;
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
                buttonGroup.position = buttonGroup.position - new Vector3(1100, 0, 0);
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
                buttonGroup.position = buttonGroup.position + new Vector3(1100, 0, 0);
                selectCheckImage.position = selectPos[chapLoadindex - 1].position;
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            chapterLoad.ChapterLoadButton(chapLoadindex);
        }
    }
}
