using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullBoard : MonoBehaviour
{
    public Transform targetTransform;
    public float startRotation = 135f;
    public float endRotation = 45f;
    public float rotationDuration = 3.0f;
    bool oneTime = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SaveZone"))
        {
            if(collision.transform.parent.gameObject.GetComponent<Player>().attached == true)
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    if(oneTime == false)
                    {
                        oneTime = true;
                        StartCoroutine(RotateOverTime());
                    }
                }
            }
        }
    }
    private IEnumerator RotateOverTime()
    {
        float elapsedTime = 0f;

        Quaternion startRotationQuaternion = Quaternion.Euler(0, 0, startRotation);
        Quaternion endRotationQuaternion = Quaternion.Euler(0, 0, endRotation);

        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            targetTransform.rotation = Quaternion.Slerp(startRotationQuaternion, endRotationQuaternion, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        targetTransform.rotation = endRotationQuaternion;
    }
}
