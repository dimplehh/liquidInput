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
            Debug.Log("npc에게 물을 주었습니다.");
        }
    }

    public override IEnumerator Messege()
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "물 맛있다.";
        yield return new WaitForSeconds(1);
        messegeImage.SetActive(false);
    }
}
