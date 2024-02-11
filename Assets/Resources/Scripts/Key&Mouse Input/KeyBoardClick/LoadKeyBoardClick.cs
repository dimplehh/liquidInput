using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] GameObject creat; //����ִ� ������ ������ �� �ߴ� â
    public int loadindex; //é�ʹ� 1���� �����̰� �̰Ŵ� 0���� �����ϴ°� ���ϼ��ְ� ������
    [SerializeField] private CheckSaveSlot? checkSaveSlot; //���� �ҷ�����
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
