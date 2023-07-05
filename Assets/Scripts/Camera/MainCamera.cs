using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    [SerializeField]
    Transform start, end;
    void Start()
    {
        GameObject go = GameObject.FindWithTag("Player");
        target = go.transform;
        offset = new Vector3(0, this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(start.position.x < target.position.x && target.position.x < end.position.x)
            transform.position = target.position + offset;
    }
}
