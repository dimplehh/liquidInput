using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc1 : Npc
{
    public override void Start()
    {
        interactionCount = 2;
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone") && Input.GetKeyDown(KeyCode.X))
        {
            if (interactionCount <= 0)
                return;
            interactionCount--;
            GameManager.instance.curWaterReserves--;
            
            SoundManager.instance.SfxPlaySound(0, transform.position);
            anim.SetBool("IsSlime", true); //motion test
            StartCoroutine(SuccessMessege());
            Debug.Log("npc���� ���� �־����ϴ�.");
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
        anim.SetBool("IsSlime", false);
    }
    public override IEnumerator FailMessege() //�ʿ�� ���) ���߿� ����Ʈ �̷��� ������ ���� ���б����ϱ� ���� 
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "�� �̹� ���� �� �Ծ���";
        yield return new WaitForSeconds(1);
        messegeImage.SetActive(false);
    }
}
