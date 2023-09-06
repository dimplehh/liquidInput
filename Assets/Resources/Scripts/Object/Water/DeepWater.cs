using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeepWater : Water
{
    //[SerializeField] protected Image currentWaterReservesImage;
    float time = 0f;
    [SerializeField]
    GameObject deadZone;
    protected override void Init()
    {
        currentWaterReserves = 0;
        maxWaterReserves = 5;
        currentWaterReserves = maxWaterReserves;
    }
    protected override void Start()
    {
        Init();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.instance.player.GetComponent<Rigidbody2D>().mass = 0.6f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (!GameManager.instance.player.GetComponent<Player>().isSlime) //슬라임 형태가 아닐때 
            {
                GameManager.instance.player.GetComponent<Rigidbody2D>().mass = 1.5f; //가라앉음
            }
            if (!GameManager.instance.player.GetComponent<Player>().isSlime
                && currentWaterReserves >= 3 && collision.transform.position.y <= deadZone.transform.position.y) //남은 물이 3 이상인데 플레이어 위치가 이럴때
            {
                GameManager.instance.player.GetComponent<Player>().anim.Play("Die");
                GameManager.instance.curWaterReserves = 0;
            }
            else if(GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                if ((Input.GetKey(KeyCode.X)))
                {
                    time += Time.deltaTime;
                    if (time >= 0.05f)
                    {
                        if (currentWaterReserves > 0)
                        {
                            SoundManager.instance.SfxPlaySound(5, transform.position);
                            GameManager.instance.curWaterReserves += 1;
                            currentWaterReserves--;
                            this.transform.position -=  new Vector3(0, 0.5f, 0);
                            time = 0f;
                        }
                        else
                        {
                            this.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

}
