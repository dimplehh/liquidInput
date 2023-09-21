using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HungryNpc : Npc
{
    public override void Start()
    {
        interactionCount = 2;
        successGauge = 1;
        soundTime = initSoundTime;
        target = GameObject.FindWithTag("Player").gameObject.transform;
        npcDistance = 15;
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("SaveZone") && Input.GetKeyDown(KeyCode.X))
        {
            if (GameManager.instance.player.GetComponent<Player>().transform.position.x > gameObject.transform.position.x)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else
                gameObject.GetComponent<SpriteRenderer>().flipX = false;

            if (interactionCount <= 0)
                return;

            interactionCount--;
            GameManager.instance.curWaterReserves--;
            
            
            StartCoroutine(SuccessMessege());
            Debug.Log("npc���� ���� �־����ϴ�.");

            if (interactionCount == 0)
            {
                anim.SetBool("IsSuccess", true);
                GameManager.instance.successGauge += successGauge;
            }
        }
    }

    public override IEnumerator SuccessMessege()
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "�� ���ִ�.";
        yield return new WaitForSeconds(1);
        messegeTxt.text = "�� ���� ��ȣ�ۿ��� \n" + interactionCount + "�� �� �� �־�";
        yield return new WaitForSeconds(1);
        //SoundManager.instance.SfxPlaySound(0, transform.position);
        messegeImage.SetActive(false);
    }
    public override IEnumerator FailMessege() //�ʿ�� ���) ���߿� ����Ʈ �̷��� ������ ���� ���б����ϱ� ���� 
    {
        yield return null;
    }

    

}
