using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSwampDeadLine : MonoBehaviour
{
    [SerializeField] private GameObject downLoad;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SaveZone"))
        {
            Debug.Log("»ç¸Á");
            GameManager.instance.isPlay = false;
            GameManager.instance.OpenGameOver();
        }
        //if (other.gameObject.CompareTag("SwampDown"))
        //{
        //    downLoad.GetComponent<SandSwamp>().isDown = true;
        //}
    }
}
