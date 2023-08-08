using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShallowWater : Water
{
    [SerializeField]
    GameObject insideWater;
    float time = 0f;
    Color tmpColor;
    protected override void Init()
    {
        currentWaterReserves = 0;
        maxWaterReserves = 5;
        currentWaterReserves = maxWaterReserves;
       tmpColor = this.GetComponent<SpriteRenderer>().color;
    }
    protected override void Start()
    {
        Init();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tmpColor.a = 0;
        this.GetComponent<SpriteRenderer>().color = tmpColor;
        insideWater.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        tmpColor.a = 100;
        this.GetComponent<SpriteRenderer>().color = tmpColor;
        insideWater.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                if(Input.GetKey(KeyCode.C))
                {
                    time += Time.deltaTime;
                    if (time >= 0.05f)
                    {
                        if (currentWaterReserves > 0)
                        {
                            GameManager.instance.curWaterReserves += 1;
                            currentWaterReserves--;
                            this.transform.position -= new Vector3(0, 0.5f, 0);
                            insideWater.transform.position -= new Vector3(0, 0.5f, 0);
                            time = 0f;
                        }
                    }
                }
            }
            }
        }
}
