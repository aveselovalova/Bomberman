using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoading : ResourcesLoadingBase
{

    public override GameObject LoadItem(string path)
    {
        return Resources.Load(path, typeof(GameObject)) as GameObject;
    }
}
