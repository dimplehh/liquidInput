using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] private int loadindex; //챕터는 1부터 시작이고 이거는 0부터 시작하는거 통일성있게 가야함
    [SerializeField] private LoadSlotSelect loadSlotSelect; //수동 불러오기
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
