using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSwamp : MonoBehaviour
{
    [SerializeField] private GameObject downLoad;
    [SerializeField] private GameObject deadLoad;
    [SerializeField] private ParticleSystem sandPs;
    [SerializeField] private GameObject sandPsGo;
    [SerializeField] private GameObject sandSwampPos;
    public Vector3 initdownLoadPos;
    public bool isTriggerPlayer = false;
    private float downSpeed = 0.3f;
    private float initDownSpeed = 0.3f;
    public bool isDown = false;
    float time = 0;
    public bool boxExist = false;
    private void Start()
    {
        //ù ��ġ ����
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
                if (time >= 2.0f)
                {
                    GameManager.instance.waterParticle.GetComponent<ParticleSystem>().Play(); // �̰� ���� GameManager�� curWaterReverse �پ�� �� �Լ� ���� ����°� ������
                    if (GameManager.instance.curWaterReserves > 0)
                        GameManager.instance.curWaterReserves -= 1;
                    time = 0f;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
            boxExist = true;
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
        if(isTriggerPlayer && GameManager.instance.player.GetComponent<Player>().isSlow)
        {
            DownTime();
            Down();
        }

        if(GameManager.instance.player.GetComponent<Player>().isSlow)
        {
            sandPs.Play();
            sandPsGo.transform.position = new Vector3(GameManager.instance.player.GetComponent<Player>().transform.position.x, sandSwampPos.transform.position.y, 0);
        }
        else
        {
            sandPs.Stop();
            if (!boxExist)
                downLoad.transform.position = initdownLoadPos;
        }
    }

    public void DownTime()
    {
        if (!isDown)
        {
            downSpeed -= Time.deltaTime;
            if (downSpeed < 0)
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
            downLoad.transform.position = downLoad.transform.position + new Vector3(0, -0.01f, 0);
        }
    }

}