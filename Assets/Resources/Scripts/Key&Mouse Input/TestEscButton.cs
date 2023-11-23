using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEscButton : MonoBehaviour
{
    public GameObject thisGo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EscButton();
    }

    private void EscButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            thisGo.SetActive(false);
        }
    }
}
