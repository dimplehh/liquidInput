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
    [SerializeField] private GameObject[] rockImage;

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
                SoundManager.instance.SfxPlaySound(2, transform.position);
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
                SoundManager.instance.SfxPlaySound(2, transform.position);
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
                chapterNameText.text = ChapterName(chapLoadindex); //é�� �̸� ����
                SoundManager.instance.SfxPlaySound(2, transform.position);
                RockImageCheck();
               
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (StageManager.instance.lastStageIndex >= chapLoadindex)
            {
                SoundManager.instance.SfxPlaySound(3, transform.position);
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
        for(int i=0; i<rockImage.Length; i++)
        {
            rockImage[i].SetActive(true);
        }

        if (StageManager.instance.lastStageIndex < chapLoadindex)
        {
            rockImage[chapLoadindex-1].SetActive(true);
        }
        else
        {
            rockImage[chapLoadindex - 1].SetActive(false);
        }
    }

}
