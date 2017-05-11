using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersGenerating : PlayersGeneratingBase
{

    public override GameObject GetPlayer()
    {
        return Resources.Load("Characters/Player", typeof(GameObject)) as GameObject;
    }
    public override GameObject GetEnemy()
    {
        return Resources.Load("Characters/Enemy", typeof(GameObject)) as GameObject;
    }

}
