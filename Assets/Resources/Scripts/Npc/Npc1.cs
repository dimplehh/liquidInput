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
            Debug.Log("npc에게 물을 주었습니다.");
        }
    }

    public override IEnumerator SuccessMessege()
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "물 맛있다.";
        yield return new WaitForSeconds(1);
        messegeTxt.text = "난 이제 상호작용을 \n" + interactionCount + "번 할 수 있어";
        yield return new WaitForSeconds(1);
        //SoundManager.instance.SfxPlaySound(0, transform.position);
        messegeImage.SetActive(false);
        anim.SetBool("IsSlime", false);
    }
    public override IEnumerator FailMessege() //필요시 사용) 나중에 퀘스트 이런거 있으면 성공 실패구분하기 위해 
    {
        messegeImage.SetActive(true);
        messegeTxt.text = "난 이미 물을 다 먹었어";
        yield return new WaitForSeconds(1);
        messegeImage.SetActive(false);
    }
}
