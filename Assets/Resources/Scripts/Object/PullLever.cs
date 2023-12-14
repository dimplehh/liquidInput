using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullLever : MonoBehaviour
{
    bool pushX = false;
    [SerializeField]float endTime = 1.0f;
    [SerializeField]float startRotation = 0f;
    [SerializeField] GameObject ladder;
    [SerializeField] float ladderEndTime = 3.0f;
    [SerializeField] float ladderStartRotation = -15f;
    [SerializeField] float ladderEndRotation = -45f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.X))
            {
                GameManager.instance.player.GetComponent<Player>().anim.SetBool("canGrab", true);
                pushX = true;
            }
            else
            {
                GameManager.instance.player.GetComponent<Player>().anim.SetBool("canGrab", false);
                pushX = false;
            }
            if((pushX == true && Input.GetKey(KeyCode.LeftArrow)  && collision.transform.position.x < this.gameObject.transform.position.x) ||
                (pushX == true && Input.GetKey(KeyCode.RightArrow) && collision.transform.position.x > this.gameObject.transform.position.x))
            {
                StartCoroutine("PullingLever", collision.transform.position.x - this.gameObject.transform.position.x);
                Debug.Log(collision.transform.position.x + "/" + this.gameObject.transform.position.x);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pushX = false;
            GameManager.instance.player.GetComponent<Player>().anim.SetBool("canGrab", false);
        }
    }

    IEnumerator PullingLever(float fromPlayerToLever)
    {
        float elapsedTime = 0f;
        Quaternion startRotationQuaternion = Quaternion.Euler(0, 0, startRotation);
        Quaternion endRotationQuaternion = (fromPlayerToLever < 0) ? Quaternion.Euler(0, 0, startRotation + 30f) : Quaternion.Euler(0, 0, startRotation - 30f);
        //플레이어가 레버보다 왼쪽에 있으면 왼쪽으로 30도, 오른쪽에 있으면 오른쪽으로 30도 회전

        while(elapsedTime < endTime)
        {
            float t = elapsedTime / endTime;
            this.transform.rotation = Quaternion.Slerp(startRotationQuaternion, endRotationQuaternion, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        this.transform.rotation = endRotationQuaternion;
        StartCoroutine("LadderMoving");
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator LadderMoving()
    {
        float elapsedTime = 0f;
        Quaternion startRotationQuaternion = Quaternion.Euler(0, 0, ladderStartRotation);
        Quaternion endRotationQuaternion = Quaternion.Euler(0, 0, ladderEndRotation);
        while (elapsedTime < ladderEndTime)
        {
            float t = elapsedTime / ladderEndTime;
            ladder.transform.rotation = Quaternion.Slerp(startRotationQuaternion, endRotationQuaternion, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ladder.transform.rotation = endRotationQuaternion;
    }
}
