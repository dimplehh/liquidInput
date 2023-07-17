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
        if (other.gameObject.CompareTag("SaveZone")) //player �ȿ� �ڽĿ�����Ʈ�� saveZone�̶�� �±׸� ���� �浹ü ����
        {
            if(Input.GetKey(KeyCode.X) && isSave)
            {
                Managers.Data.SaveData(index, other.gameObject, StageManager.instance.currentStageIndex, GameManager.instance.curWaterReserves);
                Debug.Log(index + " �� �����Ͽ����ϴ�.");
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
        Debug.Log(0 + "�ڵ� ���̺�");
        isSave = false;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            isSave = true;
            Debug.Log("player�� SaveZone���� �������ϴ�.");
        }
    }
}
