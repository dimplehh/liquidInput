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
        if (other.gameObject.GetComponentInChildren<DeepWater>().currentWaterReserves > 0 && Input.GetKeyDown(KeyCode.X))
        {
            if (interactionCount <= 0)
                return;

            interactionCount--;
            GameManager.instance.curWaterReserves++;


            StartCoroutine(SuccessMessege());
            Debug.Log("���� �Ծ����ϴ�.");

            if (interactionCount == 0)
            {
                if (GameManager.instance.player.GetComponent<Player>().transform.position.x > gameObject.transform.position.x)
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                else
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;

                anim.SetBool("IsSuccess", true);
                GameManager.instance.successGauge += successGauge;
            }
        }
    }

    public override IEnumerator SuccessMessege()
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "����  \n" + interactionCount + "�� �� ����ϸ� �� �츱 �� �־�";
        yield return new WaitForSeconds(1);
        //SoundManager.instance.SfxPlaySound(0, transform.position);
        messegeImage.SetActive(false);
    }
    public override IEnumerator FailMessege() //�ʿ�� ���) ���߿� ����Ʈ �̷��� ������ ���� ���б����ϱ� ���� 
    {
        yield return null;
    }
}
