using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc1 : Npc
{

    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone") && Input.GetKeyDown(KeyCode.X))
        {
            if (anim.GetBool("IsSlime"))
                return;
            
            GameManager.instance.curWaterReserves--;
            anim.SetBool("IsSlime", true); //motion test
            StartCoroutine(Messege());
            Debug.Log("npc���� ���� �־����ϴ�.");
        }
    }

    private IEnumerator Messege()
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "�� ���ִ�.";
        yield return new WaitForSeconds(1);
        messegeImage.SetActive(false);
    }
}
