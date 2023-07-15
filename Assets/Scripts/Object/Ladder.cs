using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public GameObject root;
    public GameObject[] prefabLadderSegs;
    public int numLinks = 6;
    void Start()
    {
        GenerateLadder();
    }

    void GenerateLadder()
    {
        GameObject prevBod = root;
        for (int i = 0; i < numLinks; i++)
        {
            int index;
            index = 0;
            GameObject newSeg = Instantiate(prefabLadderSegs[index]);
            newSeg.transform.parent = prevBod.transform;
            newSeg.transform.position= prevBod.transform.position - new Vector3(0, 1.4f, 0);
            prevBod = newSeg;
        }
    }
}
