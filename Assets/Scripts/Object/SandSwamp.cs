using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSwamp : MonoBehaviour
{
    [SerializeField] private GameObject downLoad;
    public Vector3 initdownLoadPos;
    public bool isTriggerPlayer = false;
    private float downSpeed = 0.3f;
    private float initDownSpeed = 0.3f;
    private bool isDown = false;

    private void Start()
    {
        //ù ��ġ ����
        initdownLoadPos = downLoad.transform.position; 
        initDownSpeed = downSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            isTriggerPlayer = true;
            Debug.Log("������");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            isTriggerPlayer = false;
            downLoad.transform.position = initdownLoadPos;
            Debug.Log("������� ��ġ");
        }
    }
    private void Update()
    {
        if (isTriggerPlayer) //�÷��̾ ������ ��
        {
            DownTime(); 
            Down();
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
        if (isDown)
        {
            downLoad.transform.position = downLoad.transform.position + new Vector3(0, -0.001f, 0);
        }
    }

}
