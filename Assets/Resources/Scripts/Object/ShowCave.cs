using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCave : MonoBehaviour
{
    [SerializeField] GameObject hideCave;
    [SerializeField] GameObject hideWall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            hideCave.SetActive(false);
            hideWall.SetActive(true);
        }
    }
}
