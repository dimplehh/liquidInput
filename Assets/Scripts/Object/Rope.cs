using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    public int numLinks = 6;
    void Start()
    {
        GenerateRope();
    }
    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for(int i = 0; i < numLinks; i++)
        {
            int index ;
            if(i < 4)
            {
                index = i;
            }
            else if(numLinks - 5 <= i && i < numLinks - 1)
            {
                index = prefabRopeSegs.Length - 2;
            }
            else if (i == numLinks - 1) //끝부분 sprite가 작아서 어색한 상태..
            {
                index = prefabRopeSegs.Length - 1;
            }
            else
            {
                index = 4;
                //index = Random.Range(4, prefabRopeSegs.Length - 1);
            }
            GameObject newSeg = Instantiate(prefabRopeSegs[index]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
    }
}
