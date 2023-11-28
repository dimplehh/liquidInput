using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject ManualGroup;
    [SerializeField] GameObject[] depthFirstPanel;
    [SerializeField] GameObject[] depthSecondPanel;
    [SerializeField] GameObject[] depthThirdPanel;
    void Update()
    {
        ESCEvent();
    }
    void ESCEvent()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (depthThirdPanel.Length > 0)
            {
                for (int i = 0; i < depthThirdPanel.Length; i++)
                {
                    if (depthThirdPanel[i].activeSelf == true)
                    {
                        depthThirdPanel[i].SetActive(false);
                        return;
                    }
                }
            }
            if (depthSecondPanel.Length > 0)
            {
                for (int i = 0; i < depthSecondPanel.Length; i++)
                {
                    if (depthSecondPanel[i].activeSelf == true)
                    {
                        depthSecondPanel[i].SetActive(false);
                        return;
                    }
                }
            }
            if (depthFirstPanel.Length > 0)
            {

                Debug.Log("여길 들어오는가?1");
                for (int i = 0; i < depthFirstPanel.Length; i++)
                {
                    Debug.Log("여길 들어오는가?2");
                    if (depthFirstPanel[i].activeSelf == true)
                    {
                        Debug.Log("여길 들어오는가?3");
                        depthFirstPanel[i].SetActive(false);
                        return;
                    }
                }
            }
            if (!ManualGroup.activeSelf)
                ManualGroup.SetActive(true);
        }
    }
}
