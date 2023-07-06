using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSwamp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            Debug.Log("�𷡴˿� �����Ͽ����ϴ�.");
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�𷡴˿� �ֽ��ϴ�.");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�𷡴˿��� �����Ͽ����ϴ�.");
        }
    }
}
