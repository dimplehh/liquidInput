using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc2 : Npc
{
    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone") && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Messege());
            Debug.Log("아무런 효과가 없습니다.");
        }
    }

    public override IEnumerator Messege()
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "난 물이 필요없어!";
        yield return new WaitForSeconds(1);
        messegeImage.SetActive(false);
    }
}
