using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRock : MonoBehaviour
{
    [SerializeField] GameObject Rock;
    [SerializeField] float Xpos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Rock.transform.localPosition = new Vector3(Xpos, Rock.transform.localPosition.y, Rock.transform.localPosition.z);
        }
    }
}
