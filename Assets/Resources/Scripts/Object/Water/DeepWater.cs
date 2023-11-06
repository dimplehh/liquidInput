using System;
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
    }

    private void Awake()
    {
        Init();
    }
    
    public override void UpdateWater()
    {
        if (currentWaterReserves > 0)
        {
            transform.position -= new Vector3(0, 0.5f * (maxWaterReserves - currentWaterReserves) , 0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.instance.player.GetComponent<Rigidbody2D>().mass = 0.6f;//다시 플레이어 질량 원래대로 되돌림
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (currentWaterReserves <= 0)//남아있는 물이 0보다 작을 때
                gameObject.SetActive(false);//깊은물 비활성화

            if (!GameManager.instance.player.GetComponent<Player>().isSlime //슬라임이 아니면서
                && currentWaterReserves >= 3 && collision.transform.position.y <= deadZone.transform.position.y) //남은 물이 3 이상인데 플레이어 위치가 이럴때
            {
                GameManager.instance.player.GetComponent<Player>().anim.Play("Die");//죽음
                GameManager.instance.curWaterReserves = 0;
            }
            if (!GameManager.instance.player.GetComponent<Player>().isSlime) //사람 형태일 때
                GameManager.instance.player.GetComponent<Rigidbody2D>().mass = 1.5f; //가라앉음
            else//슬라임 형태일 때
            {
                if ((Input.GetKey(KeyCode.X)))//물 흡수하면
                {
                    time += Time.deltaTime;
                    if (time >= 0.05f)
                        if (currentWaterReserves > 0)
                        {
                            SoundManager.instance.SfxPlaySound(5, transform.position);
                            GameManager.instance.curWaterReserves += 1;
                            currentWaterReserves--;
                            this.transform.position -= new Vector3(0, 0.5f, 0);
                            time = 0f;
                        }
                }
            }
        }
    }

}
