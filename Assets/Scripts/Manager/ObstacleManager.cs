using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    GameObject[] pieces;
    [SerializeField]
    GameScene gs;
    GameObject[] platforms;

    // Start is called before the first frame update
    void Start()
    {
        pieces = GameObject.FindGameObjectsWithTag("Platform");
        for(int i =0; i < pieces.Length; i++)
        {
            pieces[i].transform.Find("Obstacles").transform.Find("Box2").gameObject.SetActive(true);
            pieces[i].transform.Find("Obstacles").transform.Find("Box2").gameObject.GetComponent<ObjectMove>().player = gs.player;
        }
    }

}
