using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNpc : Npc
{
    public override void Start()
    {
        interactionCount = 5; //���� �� Ƚ���� �޾ƾ��ϰ�
        successGauge = 1; 
        soundTime = initSoundTime;
        target = GameObject.FindWithTag("Player").gameObject.transform;
        npcDistance = 15;
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            if (!anim.GetBool("IsSuccess"))
            {
                Debug.Log("���� ���� ����ִ�.");
                DeepWater water = other.gameObject.GetComponent<DeepWater>();
                CurWaterReserves(water); //���� üũ
            }
            
     
        }
    }
    private void CurWaterReserves(DeepWater deepWater)
    {
        interactionCount = deepWater.currentWaterReserves-1;
        if (interactionCount <= 0)
        {
            interactionCount = 0;
            if (GameManager.instance.player.GetComponent<Player>().transform.position.x > gameObject.transform.position.x)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else
                gameObject.GetComponent<SpriteRenderer>().flipX = false;

            anim.SetBool("IsSuccess", true);
            GameManager.instance.successGauge += successGauge;
        }
    }
    public override IEnumerator SuccessMessege()
    {
        yield return null;
    }
    public override IEnumerator FailMessege() //�ʿ�� ���) ���߿� ����Ʈ �̷��� ������ ���� ���б����ϱ� ���� 
    {
        yield return null;
    }
}
