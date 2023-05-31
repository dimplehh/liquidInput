using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset; 
    void Start()
    {
        GameObject go = GameObject.Find("Player");
        target = go.transform;
        offset = new Vector3(0, this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
