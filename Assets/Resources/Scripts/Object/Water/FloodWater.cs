using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodWater : Water
{
    [SerializeField]
    GameObject insideWater;
    [SerializeField] int waterCount;
    public bool getWater = false;
    protected override void Init()
    {
        currentWaterReserves = 0;
        maxWaterReserves = waterCount;
        currentWaterReserves = maxWaterReserves;
    }

    protected override void Start()
    {
        Init();
    }

    public override void UpdateWater()
    {
        if (currentWaterReserves > 0)
        {
            var count = maxWaterReserves - currentWaterReserves;
            for (int i = 0; i < count; i++)
            {
                if (insideWater != null) insideWater.transform.localScale = Vector3.Lerp(insideWater.transform.localScale, insideWater.transform.localScale * 0.75f, 1.3f);
            }
        }
        else if(insideWater != null)
        {
            insideWater.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SaveZone"))
        {
            if (GameManager.instance.player.GetComponent<Player>().isSlime)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    if (currentWaterReserves > 0)
                    {
                        SoundManager.instance.SfxPlaySound(5, transform.position);
                        GameManager.instance.curWaterReserves += 1;
                        currentWaterReserves--;
                        getWater = true;
                        if (insideWater != null) insideWater.transform.localScale = Vector3.Lerp(insideWater.transform.localScale, insideWater.transform.localScale * 0.75f, 1.3f);
                    }
                    else if (insideWater != null)
                    {
                        insideWater.SetActive(false);
                    }
                }
            }
        }
    }
}
