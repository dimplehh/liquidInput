using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStoneInPoisionWater : MonoBehaviour
{
    [SerializeField] float yPos;
    [SerializeField] BoxCollider2D seesawCollider;
    int count = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 24 && count > 0)
        {
            SoundManager.instance.SfxPlaySound(9, this.gameObject.transform.position);
            this.gameObject.transform.position += new Vector3(0, yPos, 0);
            count--;
            Debug.Log(count);
            collision.gameObject.SetActive(false);
            if(count > 0)
            {
                seesawCollider.enabled = false;
                seesawCollider.enabled = true;
            }
        }
    }
}
