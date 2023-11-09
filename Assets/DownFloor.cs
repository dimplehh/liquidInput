using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownFloor : MonoBehaviour
{
    private bool isCollided = false;
    float duration = 3f;
    float speed = 10f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isCollided = true;
            Invoke("MoveObjectDown", 2f);
        }
    }
    private void MoveObjectDown()
    {
        if (isCollided)
        {
            float distance = speed * duration;

            StartCoroutine(MoveCoroutine(distance, duration));
        }
    }

    private IEnumerator MoveCoroutine(float distance, float duration)
    {
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float newY = transform.position.y - (distance / duration) * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
    }
}
