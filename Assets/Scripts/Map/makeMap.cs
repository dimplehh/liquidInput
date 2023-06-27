using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeMap : MonoBehaviour
{
    [SerializeField]
    GameObject[] background = new GameObject[2];
    GameObject bg;

    // Start is called before the first frame update
    void Start()
    {
        bg = this.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.x < bg.transform.position.x && background[1])
            {
                background[1].SetActive(false);
            }
            else if (bg.transform.position.x < collision.gameObject.transform.position.x && background[0])
            {
                background[0].SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.x < bg.transform.position.x &&background[0])
            {
                background[0].SetActive(true);
            }
            else if (bg.transform.position.x < collision.gameObject.transform.position.x &&  background[1])
            {
                background[1].SetActive(true);
            }
        }
    }
}
