using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadKeyBoardClick : MonoBehaviour
{
    public int loadindex; //é�ʹ� 1���� �����̰� �̰Ŵ� 0���� �����ϴ°� ���ϼ��ְ� ������
    [SerializeField] private LoadSlotSelect? loadSlotSelect; //���� �ҷ�����
    [SerializeField] private RectTransform?[] selectPos; //�����̹��� ��ġ
    public Text[] selectText; //���� �ؽ�Ʈ
    private void OnEnable()
    {
        loadindex = 0;
        SelectTextColorWhite();
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
                gameObject.SetActive(false);
                for (int i = 0; i < selectText.Length; i++)
                {
                    selectText[i].color = Color.black;
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
                SelectTextColorBlack();
                loadindex++;
                SelectTextColorWhite();
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
                SelectTextColorBlack();
                loadindex--;
                SelectTextColorWhite();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            loadSlotSelect.Slot(loadindex);
        }
    }
    public void SelectTextColorWhite()
    {
        selectText[loadindex].color = Color.white;
        selectText[loadindex].fontSize = 40;
    }
    public void SelectTextColorBlack()
    {
        selectText[loadindex].color = Color.black;
        selectText[loadindex].fontSize = 30;
    }
}
