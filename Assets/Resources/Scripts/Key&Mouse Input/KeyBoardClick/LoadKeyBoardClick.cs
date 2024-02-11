using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] GameObject creat; //비어있는 슬롯을 눌렀을 때 뜨는 창
    public int loadindex; //챕터는 1부터 시작이고 이거는 0부터 시작하는거 통일성있게 가야함
    [SerializeField] private CheckSaveSlot? checkSaveSlot; //수동 불러오기
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
    }

    private void SlotLoadKeyBoardClick()
    {
        if (creat.activeSelf)
            return;
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
            checkSaveSlot.Slot(loadindex);
        }
    }
    public void SelectTextColorChanged()
    {
        selectText[loadindex].color = changedColor;
        selectText[loadindex].fontSize = 35;
    }
    public void SelectTextColorBase()
    {
        selectText[loadindex].color = baseColor;
        selectText[loadindex].fontSize = 30;
    }
}
