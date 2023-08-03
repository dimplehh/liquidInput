using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] private int loadindex; //é�ʹ� 1���� �����̰� �̰Ŵ� 0���� �����ϴ°� ���ϼ��ְ� ������
    [SerializeField] private LoadSlotSelect loadSlotSelect; //���� �ҷ�����
    [SerializeField] private RectTransform selectCheckImage; //���� Ȯ�� �̹���
    [SerializeField] private RectTransform[] selectPos; //�����̹��� ��ġ
    private void OnEnable()
    {
        loadindex = 0;
        selectCheckImage.position = selectPos[0].position;
    }
    private void Update()
    {
        SlotLoadKeyBoardClick();
    }

    private void SlotLoadKeyBoardClick()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (loadindex >= 3)
            {
                loadindex = 3;
                selectCheckImage.position = selectPos[3].position;
            }
            else
            {
                loadindex++;
                selectCheckImage.position = selectPos[loadindex].position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (loadindex <= 0)
            {
                loadindex = 0;
                selectCheckImage.position = selectPos[0].position;
            }
            else
            {
                loadindex--;
                selectCheckImage.position = selectPos[loadindex].position;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            loadSlotSelect.Slot(loadindex);
        }
    }
}
