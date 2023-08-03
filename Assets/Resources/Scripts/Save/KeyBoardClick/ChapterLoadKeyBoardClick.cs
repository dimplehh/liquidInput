using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterLoadKeyBoardClick : MonoBehaviour
{
    [SerializeField] private ChapterLoad chapterLoad; //Ã©ÅÍº° ºÒ·¯¿À±â
    [SerializeField] private int chapLoadindex = 1; //Ã©ÅÍº° ÀÎµ¦½º
    private void OnEnable()
    {
        chapLoadindex = 1;
    }
    private void Update()
    {
        LoadKeyBoardClick();
    }

    private void LoadKeyBoardClick()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            chapLoadindex++;
            if (chapLoadindex >= 4) chapLoadindex = 4;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            chapLoadindex--;
            if (chapLoadindex <= 1) chapLoadindex = 1;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            chapterLoad.ChapterLoadButton(chapLoadindex);
        }
    }
}
