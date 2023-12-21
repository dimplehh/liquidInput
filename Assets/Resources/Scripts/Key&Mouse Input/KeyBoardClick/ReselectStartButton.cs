using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReselectStartButton : MonoBehaviour
{
    public LoadSlotSelect loadSlotSelect;
    int first = 0;
    private void OnEnable()
    {
        Managers.Input.keyaction += OnKeyboard;
    }
    private void OnDisable()
    {
        Managers.Input.keyaction -= OnKeyboard;
    }

    void OnKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(first > 0)
                loadSlotSelect.NewGame();
            first++;
        }
    }

    public void OnMouseButton()
    {
        loadSlotSelect.NewGame();
    }
}
