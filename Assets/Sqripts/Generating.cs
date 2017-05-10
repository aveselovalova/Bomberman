using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generating : MonoBehaviour {

    
    // Use this for initialization
    void Start () {
        Building newItem = new Building();
        newItem.GetPlane();
        newItem.GenerateConcreteWalls();
        newItem.GenerateBreakWalls();
    }
  
   
    // Update is called once per frame
    void Update () {
		
	}
}
