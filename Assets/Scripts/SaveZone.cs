using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZone : MonoBehaviour
{
    private int index;
    private bool isSave = false; 
    private void Start()
    {
        index = Managers.Data.playerData.index;
        isSave = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("SaveZone")&& Input.GetKey(KeyCode.X) && isSave) //player 안에 자식오브젝트로 saveZone이라는 태그를 가진 충돌체 생성
        {
            Managers.Data.SaveData(index, other.gameObject, GameManager.instance.currentStage+1, GameManager.instance.curWaterReserves);
            Debug.Log(index + " 에 저장하였습니다.");
            index++;
            isSave = false;
            if(index == 4)
            {
                index = 1;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            isSave = true;
            Debug.Log("player가 SaveZone에서 나갔습니다.");
        }
    }
}
