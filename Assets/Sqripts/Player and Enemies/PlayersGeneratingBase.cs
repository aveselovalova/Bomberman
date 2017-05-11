using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayersGeneratingBase : MonoBehaviour
{
    public abstract GameObject GetPlayer();
    public abstract GameObject GetEnemy();

}
