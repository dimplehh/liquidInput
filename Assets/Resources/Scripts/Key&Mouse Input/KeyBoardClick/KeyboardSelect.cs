using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyboardSelect : MonoBehaviour
{
    //이건 인게임에서는 다르게 적용해야할듯
    [SerializeField] private GameObject[] selectButtonEvent; //선택 버튼별 오브젝트 
    [SerializeField] private GameObject? resumeButtonEvent; //인게임 제개 버튼 
    [SerializeField] private GameObject exitGamePanel;
    //위에 제외 나머지는 공용으로 사용할 수 있음  
    public int selectIndex = 0; //선택 번호
    public Text[] selectText; //선택 텍스트
    [SerializeField] private Color baseColor, changedColor;

    private void OnEnable()
    {
        selectIndex = 0;
        selectText[selectIndex].color = changedColor;
        selectText[selectIndex].fontSize = 60;
    }
    private void Update()
    {
        SlotLoadKeyBoardClick();
        SelectButton();
    }
    private void SlotLoadKeyBoardClick()
    {
        if (exitGamePanel != null)
            if (exitGamePanel.activeSelf)
                return; //게임 종료 확인 패턴 활성화 시 다른 동작 하지 못하도록 막기
        for(int i=1; i<4; i++) //게임시작 제외하고선 오브젝트가 활성화 되어있으면 그대로 리턴
        {
            if (selectButtonEvent[i].activeSelf)
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectIndex >= 3)
            {
                selectIndex = 3;
            }
            else
            {
                SoundManager.instance.SfxPlaySound(2, transform.position);
                selectText[selectIndex].color = baseColor;
                selectText[selectIndex].fontSize = 50;
                selectIndex++;
                selectText[selectIndex].color = changedColor;
                selectText[selectIndex].fontSize = 60;

            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectIndex <= 0)
            {
                selectIndex = 0;
            }
            else
            {
                SoundManager.instance.SfxPlaySound(2, transform.position);
                selectText[selectIndex].color = baseColor;
                selectText[selectIndex].fontSize = 50;
                selectIndex--;
                selectText[selectIndex].color = changedColor;
                selectText[selectIndex].fontSize = 60;
            }
        }
    }

    private void SelectButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (exitGamePanel != null)
                if (exitGamePanel.activeSelf)
                    return; //게임 종료 확인 패턴 활성화 시 다른 동작 하지 못하도록 막기
            if (!GameManager.instance)
            {
                //인게임이 아닐때
                SelectButtonEvent();
                SoundManager.instance.SfxPlaySound(3, transform.position);
            }  
            else
            {
                //인게임 일때
                IngameSelectButtonEvent();
                SoundManager.instance.SfxPlaySound(3, transform.position);
            }
        }
    }
    //홈화면 이벤트
    private void SelectButtonEvent()
    {
        for(int i=1; i<selectButtonEvent.Length; i++) //판넬이 하나라도 열려있으면 리턴
        {
            if (selectButtonEvent[i].activeSelf)
            {
                return;
            }
        }
        if(selectIndex == 0)
        {
            selectButtonEvent[0].GetComponentInParent<LoadSlotSelect>().NewGame();        
        }
        else if (selectIndex == 1)
        {
            selectButtonEvent[0].GetComponentInParent<LoadSlotSelect>().GoGame();
        }
        else
        {
            selectButtonEvent[selectIndex].SetActive(true);
        }
    }
    //인게임 설정 이벤트
    private void IngameSelectButtonEvent()
    {
        if (selectIndex == 0)
        {
            resumeButtonEvent.SetActive(false);
            GameManager.instance.PlayGame();
        }
        else if (selectIndex == 1 || selectIndex == 2)
        {
            selectButtonEvent[selectIndex].SetActive(true);
        }else if (selectIndex == 3)
        {
            GameManager.instance.HomeButton();
        }
    }
}
