using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRock : MonoBehaviour
{
    [SerializeField] GameObject rock;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            rock.GetComponent<Animator>().Play("RockBlocking");
        }
    }
}
