using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeMap : MonoBehaviour
{
    float width_between_back = 38.0f;
    [SerializeField]
    GameObject[] background = new GameObject[2];
    GameObject bg;

    // Start is called before the first frame update
    void Start()
    {
        bg = this.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            background[0].transform.position = new Vector3(bg.transform.position.x - width_between_back,
               bg.transform.position.y, bg.transform.position.z);
            background[1].transform.position = new Vector3(bg.transform.position.x + width_between_back,
                bg.transform.position.y, bg.transform.position.z);
        }
    }
}
