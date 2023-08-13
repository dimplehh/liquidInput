using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterLoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] private ChapterLoad chapterLoad; //é�ͺ� �ҷ�����
    [SerializeField] private int chapLoadindex = 1; //é�ͺ� �ε���
    [Header("Ű���� �̵� ��ġ")]
    [SerializeField] private RectTransform buttonGroup;
    [SerializeField] private RectTransform initButtonGroup; //�ʱ�ȭ ��ġ
    [Header("é�� ��� ��� �̹���")]
    [SerializeField] private GameObject rockImage;

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
                
            }
            else
            {
                chapLoadindex++;
                buttonGroup.position = buttonGroup.position - new Vector3(1100, 0, 0);
                RockImageCheck();
            }
  
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (chapLoadindex <= 1)
            {
                chapLoadindex = 1;
            }
            else
            {
                chapLoadindex--;
                buttonGroup.position = buttonGroup.position + new Vector3(1100, 0, 0);
                //buttonGroup.position = Vector3.Lerp(buttonGroup.position, buttonGroup.position + new Vector3(1100, 0, 0), Time.deltaTime * 0.5f); 
                RockImageCheck();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            chapterLoad.ChapterLoadButton(chapLoadindex);
        }
    }

    private void RockImageCheck()
    {
        if (StageManager.instance.lastStageIndex < chapLoadindex)
        {
            rockImage.SetActive(true);
        }
        else
        {
            rockImage.SetActive(false);
        }
    }
}
