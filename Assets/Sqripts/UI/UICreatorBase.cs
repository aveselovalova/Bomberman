using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UICreatorBase : MonoBehaviour
{
    public abstract GameObject LoadUI(string pathToObject);
}
