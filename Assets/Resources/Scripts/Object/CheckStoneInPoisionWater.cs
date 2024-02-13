using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStoneInPoisionWater : MonoBehaviour
{
    [SerializeField] public float yPos;
    [SerializeField] BoxCollider2D seesawCollider;
    public int count = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 24)
        {
            if (count > 0)
            {
                this.gameObject.transform.position += new Vector3(0, yPos, 0);
                this.gameObject.GetComponent<Water>().currentWaterReserves--;
                count--;
                seesawCollider.enabled = false;
                seesawCollider.enabled = true;
                Debug.Log(count);
            }
            SoundManager.instance.SfxPlaySound(9, this.gameObject.transform.position);
            collision.gameObject.SetActive(false);
        }
    }
}
