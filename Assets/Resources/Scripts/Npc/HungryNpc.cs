using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HungryNpc : Npc
{
    public override void Start()
    {
        interactionCount = 1;
        successGauge = 1;
        soundTime = initSoundTime;
        target = GameObject.FindWithTag("Player").gameObject.transform;
        npcDistance = 7;
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
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
            Debug.Log("npc에게 물을 주었습니다.");

            if (interactionCount == 0)
            {
                anim.SetBool("IsSuccess", true);
                GameManager.instance.successGauge += successGauge;
                GameManager.instance.effectsPool.Get(3, this.transform);
            }
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
    }
    public override IEnumerator FailMessege() //필요시 사용) 나중에 퀘스트 이런거 있으면 성공 실패구분하기 위해 
    {
        yield return null;
    }

    

}
