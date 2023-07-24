using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSwamp : MonoBehaviour
{
    [SerializeField] private GameObject downLoad;
    [SerializeField] private GameObject deadLoad;
    public Vector3 initdownLoadPos;
    public bool isTriggerPlayer = false;
    private float downSpeed = 0.3f;
    private float initDownSpeed = 0.3f;
    public bool isDown = false;
    float time = 0;

    private void Start()
    {
        //첫 위치 저장
        initdownLoadPos = downLoad.transform.position; 
        initDownSpeed = downSpeed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (!GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                isTriggerPlayer = true;
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                time += Time.deltaTime;
                if(time >= 1.0f)
                {
                    GameManager.instance.curWaterReserves -= 1;
                    time = 0f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                isTriggerPlayer = true;
            }
            else
            {
                isTriggerPlayer = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isTriggerPlayer && GameManager.instance.player.GetComponent<Player>().isSlow) //플레이어가 들어왔을 때
        {
            DownTime(); 
            Down();
        }
        else if(!GameManager.instance.player.GetComponent<Player>().isSlow)
        {
            downLoad.transform.position = initdownLoadPos;
        }
    }

    public void DownTime()
    {
        if (!isDown)
        {
            downSpeed -= Time.deltaTime;
            if(downSpeed < 0)
            {
                downSpeed = initDownSpeed;
                isDown = true;
            }
        }
    }
    public void Down()
    {
        if (isDown && GameManager.instance.isPlay && downLoad.transform.position.y >= deadLoad.transform.position.y - 1)
        {
            downLoad.transform.position = downLoad.transform.position + new Vector3(0, -0.005f, 0);
        }
    }

}
