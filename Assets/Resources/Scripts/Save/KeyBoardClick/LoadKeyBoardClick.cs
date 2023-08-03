using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] private int loadindex; //챕터는 1부터 시작이고 이거는 0부터 시작하는거 통일성있게 가야함
    [SerializeField] private LoadSlotSelect loadSlotSelect; //수동 불러오기
    [SerializeField] private RectTransform selectCheckImage; //선택 확인 이미지
    [SerializeField] private RectTransform[] selectPos; //선택이미지 위치
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
