using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNpc : Npc
{
    public override void Start()
    {
        interactionCount = 5; //깊은 물 횟수를 받아야하고
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
            Debug.Log("물을 먹었습니다.");

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
        messegeTxt.text = "물을  \n" + interactionCount + "번 만 흡수하면 날 살릴 수 있어";
        yield return new WaitForSeconds(1);
        //SoundManager.instance.SfxPlaySound(0, transform.position);
        messegeImage.SetActive(false);
    }
    public override IEnumerator FailMessege() //필요시 사용) 나중에 퀘스트 이런거 있으면 성공 실패구분하기 위해 
    {
        yield return null;
    }
}
