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
            GameManager.instance.player.GetComponent<Player>().anim.SetBool("isDie", true);
            StartCoroutine("GameOver");
        }
        //if (other.gameObject.CompareTag("SwampDown"))
        //{
        //    downLoad.GetComponent<SandSwamp>().isDown = true;
        //}
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.instance.isPlay = false;
        GameManager.instance.OpenGameOver();
    }
}
