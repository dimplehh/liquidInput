using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShallowWater : Water
{
    [SerializeField]
    GameObject insideWater;
    float time = 0f;
    protected override void Init()
    {
        currentWaterReserves = 0;
        maxWaterReserves = 5;
        currentWaterReserves = maxWaterReserves;
    }
    protected override void Start()
    {
        Init();
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
                            this.transform.localScale = new Vector3(this.transform.localScale.x * 0.75f, this.transform.localScale.y * 0.75f, this.transform.localScale.z);
                            insideWater.transform.localScale = new Vector3(insideWater.transform.localScale.x * 0.75f, insideWater.transform.localScale.y * 0.75f, insideWater.transform.localScale.z);
                            time = 0f;
                        }
                        else
                        {
                            this.gameObject.SetActive(false);
                            insideWater.SetActive(false);
                        }
                    }
                }
            }
            }
        }
}
