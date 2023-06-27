using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField]
    GameObject[] stagePiece;
    public GameObject player;
    void Start()
    {

    }

    protected override void Init()
    {
        base.Init();
        player = Managers.Game.Spawn("Player");
    }

    public override void Clear()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
