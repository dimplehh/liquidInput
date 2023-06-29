using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZone : MonoBehaviour
{
    private int index;

    private void Start()
    {
        index = 1;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("SaveZone"))
        {
            Managers.Data.SaveData(index, other.gameObject, GameManager.instance.currentStage, GameManager.instance.curWaterReserves);
            index++;
            if(index == 4)
            {
                index = 1;
            }
            Debug.Log(index+" 에 저장하였습니다.");
        }
    }
}
