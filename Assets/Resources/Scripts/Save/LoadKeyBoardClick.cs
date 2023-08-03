using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] private int loadindex; //é�ʹ� 1���� �����̰� �̰Ŵ� 0���� �����ϴ°� ���ϼ��ְ� ������
    [SerializeField] private LoadSlotSelect loadSlotSelect; //���� �ҷ�����
    private void Awake()
    {
        loadindex = 0;
    }
    private void Start()
    {
        loadindex = 1;
    }
    private void Update()
    {
        SlotLoadKeyBoardClick();
    }

    private void SlotLoadKeyBoardClick()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            loadindex++;
            if (loadindex >= 3) loadindex = 3;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            loadindex--;
            if (loadindex <= 0) loadindex = 0;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            loadSlotSelect.Slot(loadindex);
        }
    }
}
