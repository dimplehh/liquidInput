using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaterAnim : MonoBehaviour
{
    
    public Sprite[] waterImage;
    public int index;

    [SerializeField] private float curAnimTime = 0;
    [SerializeField] private const float maxAnimTime = 0.07f;

    void Start()
    {
        
    }

    
    void Update()
    {
        WaterAnimation();
    }

    public void WaterAnimation()
    {
        if(curAnimTime <= maxAnimTime)
        {
            curAnimTime += Time.deltaTime;
            if(curAnimTime >= maxAnimTime)
            {
                index++;
                if (waterImage.Length == index)
                {
                    index = 0;
                }
                this.GetComponent<Image>().sprite = waterImage[index];
                
                curAnimTime = 0;
            }
        }
    }
}
