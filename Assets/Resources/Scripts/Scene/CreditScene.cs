using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScene : MonoBehaviour
{
    public int index;
    void Start()
    {
        SoundManager.instance.BgmPlaySound(index);
    }

}
