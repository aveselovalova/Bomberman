using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICreator : UICreatorBase
{
    public override GameObject LoadUI(string pathToObject)
    {
        var ui = ResourceLoader.LoadItem(pathToObject);
        return Instantiate(ui);
    }
}
