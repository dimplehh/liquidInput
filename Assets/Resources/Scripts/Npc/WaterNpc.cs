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
    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            if (GameManager.instance.player.GetComponent<Player>().transform.position.x > gameObject.transform.position.x)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else
                gameObject.GetComponent<SpriteRenderer>().flipX = false;

            anim.SetBool("IsSuccess", true);
            GameManager.instance.successGauge += successGauge;
            GameManager.instance.effectsPool.Get(3, this.transform);
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
