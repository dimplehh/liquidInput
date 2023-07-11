using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Vector3 offset;
    [SerializeField]
    Transform start, end;
    GameObject go;
    void Start()
    {
        go = GameObject.FindWithTag("Player");
        target = go.transform;
        offset = new Vector3(0, this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (start.position.x < target.position.x && target.position.x < end.position.x)
        {
            if (go.GetComponent<Player>().anim.GetBool("canStepup"))
            {
                if (transform.position.x < go.GetComponent<Player>().tempVec.x + offset.x && transform.position.y < go.GetComponent<Player>().tempVec.y + offset.y)
                    transform.position += new Vector3(1f * Time.deltaTime, 2f * Time.deltaTime, 0);
            }
            else
            {
                transform.position = target.position + offset;
            }
        }
        else if (start.position.x >= target.position.x)
            transform.position = new Vector3(start.position.x, target.position.y, target.position.z) + offset;
        else if (target.position.x >= end.position.x)
            transform.position = new Vector3(end.position.x, target.position.y, target.position.z) + offset;
    }
}