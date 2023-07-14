using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc2 : Npc
{
    public override void Start() //이친구는 변화가 없는 npc이기 때문에 상호작용횟수를 적용시키지않는다.
    {
        //interactionCount = 1;
    }
    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone") && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(SuccessMessege());
            Debug.Log("아무런 효과가 없습니다.");
        }
    }

    public override IEnumerator SuccessMessege()
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "난 물이 필요없어!";
        yield return new WaitForSeconds(1);
        messegeImage.SetActive(false);
    }

    //public override IEnumerator FailMessege()
    //{
    //    yield return null;
    //}
}
