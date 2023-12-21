using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReselectExitButton : MonoBehaviour
{
    private void OnEnable()
    {
        SoundManager.instance.SfxPlaySound(3, transform.position);
        Managers.Input.keyaction += OnKeyboard;
    }
    private void OnDisable()
    {
        Managers.Input.keyaction -= OnKeyboard;
    }

    void OnKeyboard()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("종료");
            Application.Quit();
        }
    }

    public void OnMouseButton()
    {
        Debug.Log("종료");
        Application.Quit();
    }
}
