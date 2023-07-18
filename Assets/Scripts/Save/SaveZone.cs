using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZone : Zone
{
    
    
    protected override void Start()
    {
        isSave = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            AutoSave(other.gameObject);
            GameManager.instance.isNonAutoSave = true;
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone")) //player �ȿ� �ڽĿ�����Ʈ�� saveZone�̶�� �±׸� ���� �浹ü ����
        {
            if (Input.GetKey(KeyCode.X) && isSave)
            {
                GameManager.instance.SaveAndLoadPanel.SetActive(true);
            }
        }
    }
    public void AutoSave(GameObject other)
    {
        Managers.Data.SlotSaveData(0, other, StageManager.instance.currentStageIndex ,GameManager.instance.curWaterReserves);
        Debug.Log(0 + "�ڵ� ���̺�");
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            isSave = true;
            GameManager.instance.isNonAutoSave = false; 
            Debug.Log("player�� SaveZone���� �������ϴ�.");
        }
    }
}
