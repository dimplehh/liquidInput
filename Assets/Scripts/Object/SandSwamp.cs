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

    private void Start()
    {
        //ù ��ġ ����
        initdownLoadPos = downLoad.transform.position; 
        initDownSpeed = downSpeed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))        //if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (!GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                isTriggerPlayer = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))        //if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (!GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                Debug.Log("�����");
                isTriggerPlayer = true;
            }
            else
            {
                Debug.Log("��������");
                isTriggerPlayer = false;
            }
        }
    }
    private void Update()
    {
        if (isTriggerPlayer && GameManager.instance.player.GetComponent<Player>().isSlow) //�÷��̾ ������ ��
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
