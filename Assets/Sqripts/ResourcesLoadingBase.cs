using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourcesLoadingBase : MonoBehaviour {

    public abstract GameObject LoadItem(string path);
}
