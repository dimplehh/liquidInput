using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChapterLoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] private ChapterLoad chapterLoad; //é�ͺ� �ҷ�����
    [SerializeField] private int chapLoadindex = 1; //é�ͺ� �ε���
    [Header("Ű���� �̵� ��ġ")]
    [SerializeField] private RectTransform buttonGroup;
    [SerializeField] private RectTransform initButtonGroup; //�ʱ�ȭ ��ġ
    [Header("é�� ��� ��� �̹���")]
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
                chapterNameText.text = ChapterName(chapLoadindex); //é�� �̸� ����
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
                chapterNameText.text = ChapterName(chapLoadindex); //é�� �̸� ����
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
                name = "1é - �����ǿܰ�";
                break;
            case 2:
                name = "2é - ����������";
                break;
            case 3:
                name = "3é - ���� ��";
                break;
            case 4:
                name = "4é - ������ �߽ɺ�(����)";
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
