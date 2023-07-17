using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZone : Zone
{
    
    protected override void Start()
    {
        index = Managers.Data.playerData.index;
        isSave = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            AutoSave(other.gameObject);
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone")) //player 안에 자식오브젝트로 saveZone이라는 태그를 가진 충돌체 생성
        {
            if(Input.GetKey(KeyCode.X) && isSave)
            {
                Managers.Data.SaveData(index, other.gameObject, StageManager.instance.currentStageIndex, GameManager.instance.curWaterReserves);
                Debug.Log(index + " 에 저장하였습니다.");
                index++;
                isSave = false;
                if (index == 4)
                {
                    index = 1;
                }
            }
        }
    }
    public void AutoSave(GameObject other)
    {
        Managers.Data.SaveData(0, other, StageManager.instance.currentStageIndex ,GameManager.instance.curWaterReserves);
        Debug.Log(0 + "자동 세이브");
        isSave = false;
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
