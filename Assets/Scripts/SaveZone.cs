using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZone : MonoBehaviour
{
    private int index;
    private int count; 
    private void Start()
    {
        index = Managers.Data.playerData.index;
        count = 0;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("SaveZone")&& Input.GetKey(KeyCode.X) && count == 0) //player �ȿ� �ڽĿ�����Ʈ�� saveZone�̶�� �±׸� ���� �浹ü ����
        {
            Managers.Data.SaveData(index, other.gameObject, GameManager.instance.currentStage+1, GameManager.instance.curWaterReserves);
            Debug.Log(index + " �� �����Ͽ����ϴ�.");
            index++;
            count++;
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
            count--;
            Debug.Log("player�� SaveZone���� �������ϴ�.");
        }
    }
}
