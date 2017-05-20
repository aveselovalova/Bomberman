using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BombEffectsBase : MonoBehaviour {

    protected virtual GameObject DinamicChildObject(string path, float xPosition, float zPosition)
    {
        var obj = new ResourcesLoading().LoadItem(path);
        var objLocation = new Vector3(Mathf.RoundToInt(xPosition), 0.55f, Mathf.RoundToInt(zPosition));
        return Instantiate(obj, objLocation, new Quaternion(0, 0, 0, 0));
    }
}
