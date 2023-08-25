using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChapterLoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] private ChapterLoad chapterLoad; //챕터별 불러오기
    [SerializeField] private int chapLoadindex = 1; //챕터별 인덱스
    [Header("키보드 이동 위치")]
    [SerializeField] private RectTransform buttonGroup;
    [SerializeField] private RectTransform initButtonGroup; //초기화 위치
    [Header("챕터 잠금 흑백 이미지")]
    [SerializeField] private GameObject rockImage;

    [SerializeField] private Text chapterNameText;

    private void Start()
    {
        initButtonGroup.position = buttonGroup.position;
    }
    private void OnEnable()
    {
        chapLoadindex = 1;
        chapterNameText.text = ChapterName(chapLoadindex);
    }
    private void OnDisable()
    {
        buttonGroup.position = initButtonGroup.position;
    }
    private void Update()
    {
        LoadKeyBoardClick();
        ExitButton();
        buttonGroup.position = Vector3.Lerp(buttonGroup.position, initButtonGroup.position - (new Vector3(1100, 0, 0) * (chapLoadindex-1)), Time.deltaTime * 3f);
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
                chapterNameText.text = ChapterName(chapLoadindex); //챕터 이름 결정
                //buttonGroup.position = buttonGroup.position - new Vector3(1100, 0, 0);
                //RockImageCheck();
                StartCoroutine(WaitRockImage());
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
                chapterNameText.text = ChapterName(chapLoadindex); //챕터 이름 결정
                //buttonGroup.position = buttonGroup.position + new Vector3(1100, 0, 0);
                //buttonGroup.position = Vector3.Lerp(buttonGroup.position, buttonGroup.position + new Vector3(1100, 0, 0), Time.deltaTime * 0.5f); 
                //RockImageCheck();
                StartCoroutine(WaitRockImage());
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (StageManager.instance.lastStageIndex >= chapLoadindex)
            {
                chapterLoad.ChapterLoadButton(chapLoadindex);
            }
        }
    }
    private string ChapterName(int chapLoadIndex)
    {
        string name = "";
        switch (chapLoadIndex)
        {
            case 1:
                name = "1챕 - 세상의외곽";
                break;
            case 2:
                name = "2챕 - 버려진마을";
                break;
            case 3:
                name = "3챕 - 지하 속";
                break;
            case 4:
                name = "4챕 - 오염의 중심부(공장)";
                break;
        }
        return name;
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
    private IEnumerator WaitRockImage()
    {
        yield return new WaitForSeconds(2.1f);
        RockImageCheck();
    }
}
