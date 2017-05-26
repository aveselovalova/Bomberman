using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerupsGenerator
{
    private static System.Random _rand = new System.Random();
    private static DynamicObjectsCreator _dynamicObj = new DynamicObjectsCreator();
    
    public static void GenerateNewPowerup(Vector3 powerupPos)
    {
        var powerupsAmount = 4;
        var probability = _rand.Next(100);
        var probabilityOfAppearence = 1;

        if (probability> probabilityOfAppearence)
             _dynamicObj.CreatePowerUp(((Powerup)_rand.Next(powerupsAmount)).ToString(), powerupPos);
    }
}
