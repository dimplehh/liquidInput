using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSwampDeadLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            Debug.Log("»ç¸Á");
            GameManager.instance.OpenGameOverPanel();
        }
    }
}
