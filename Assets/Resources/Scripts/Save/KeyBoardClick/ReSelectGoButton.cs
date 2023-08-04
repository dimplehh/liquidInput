using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSelectGoButton : MonoBehaviour
{
    public LoadSlotSelect loadSlotSelect;
    
    // Update is called once per frame
    private void Update()
    {
        GoButton();
    }

    private void GoButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            loadSlotSelect.GoGame();
        }
    }
}
