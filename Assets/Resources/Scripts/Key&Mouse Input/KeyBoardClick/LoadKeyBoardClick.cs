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
