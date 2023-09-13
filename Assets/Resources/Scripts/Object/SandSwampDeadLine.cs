using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSwampDeadLine : MonoBehaviour
{
    [SerializeField] private GameObject downLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 18)
        {
            other.gameObject.transform.parent.gameObject.SetActive(false);
            downLoad.GetComponent<SandSwamp>().boxExist = false;
        }
        if (other.gameObject.CompareTag("SaveZone"))
        {
            Debug.Log("»ç¸Á");
            GameManager.instance.curWaterReserves = 0;
        }
        //if (other.gameObject.CompareTag("SwampDown"))
        //{
        //    downLoad.GetComponent<SandSwamp>().isDown = true;
        //}
    }
}
