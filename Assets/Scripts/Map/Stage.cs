using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    GameObject[] stagePiece;
    // Start is called before the first frame update
    void Start()
    {
        stagePiece = GameObject.FindGameObjectsWithTag("Platform");
    }
}
