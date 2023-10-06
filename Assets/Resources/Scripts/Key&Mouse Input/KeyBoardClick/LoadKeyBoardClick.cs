using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadKeyBoardClick : MonoBehaviour
{
    public int loadindex; //챕터는 1부터 시작이고 이거는 0부터 시작하는거 통일성있게 가야함
    [SerializeField] private LoadSlotSelect? loadSlotSelect; //수동 불러오기
    [SerializeField] private RectTransform?[] selectPos; //선택이미지 위치
    public Text[] selectText; //선택 텍스트
    [SerializeField] private Color baseColor, changedColor;
    private void OnEnable()
    {
        loadindex = 0;
        SelectTextColorChanged();
    }
    private void Update()
    {
        SlotLoadKeyBoardClick();
        ExitButton();
    }
    private void ExitButton()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SoundManager.instance.SfxPlaySound(2, transform.position);
                gameObject.SetActive(false);
                for (int i = 0; i < selectText.Length; i++)
                {
                    selectText[i].color = baseColor;
                }
            }
        }
    }
    private void SlotLoadKeyBoardClick()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (loadindex >= 3)
            {
                loadindex = 3;
            }
            else
            {
                SelectTextColorBase();
                loadindex++;
                SelectTextColorChanged();
                SoundManager.instance.SfxPlaySound(2, transform.position);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (loadindex <= 0)
            {
                loadindex = 0;
                
            }
            else
            {
                SelectTextColorBase();
                loadindex--;
                SelectTextColorChanged();
                SoundManager.instance.SfxPlaySound(2, transform.position);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SoundManager.instance.SfxPlaySound(3, transform.position);
            loadSlotSelect.Slot(loadindex);
        }
    }
    public void SelectTextColorChanged()
    {
        selectText[loadindex].color = changedColor;
        selectText[loadindex].fontSize = 40;
    }
    public void SelectTextColorBase()
    {
        selectText[loadindex].color = baseColor;
        selectText[loadindex].fontSize = 30;
    }
}
