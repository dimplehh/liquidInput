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
                if(Input.GetKey(KeyCode.X))
                {
                    time += Time.deltaTime;
                    if (time >= 0.1f)
                    {
                        if (currentWaterReserves > 0)
                        {
                            SoundManager.instance.SfxPlaySound(5, transform.position);
                            GameManager.instance.curWaterReserves += 1;
                            currentWaterReserves--;
                            this.transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 0.75f, 1.3f);
                            insideWater.transform.localScale = Vector3.Lerp(insideWater.transform.localScale, insideWater.transform.localScale * 0.75f, 1.3f);
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
