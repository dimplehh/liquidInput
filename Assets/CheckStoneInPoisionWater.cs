using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStoneInPoisionWater : MonoBehaviour
{
    [SerializeField] float yPos;
    int count = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stone") && count > 0)
        {
            this.gameObject.transform.position += new Vector3(0, yPos, 0);
            count--;
            collision.gameObject.SetActive(false);
            Debug.Log(count);
        }
    }
}
