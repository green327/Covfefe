using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://docs.unity3d.com/ScriptReference/Transform.Rotate.html 
//https://answers.unity.com/questions/695041/2d-platform-anchored-in-center-rotate-only.html - showed how to use the 2d hinge object
//https://stackoverflow.com/questions/45891966/building-a-rotating-platform-in-unity - showed how to operate speed
//Must add rigid body to empty parent, must add this script to parent
//Set the child to have a 2d hinge and a rigid body
//Turn off auto configure connect 
public class Rotate : MonoBehaviour
{
    private Vector3 currentPos;
    GameObject platform;
    private int speed = 20;

    void Start()
    {
        GameObject platform = GameObject.FindGameObjectWithTag("RotationPlatform");
    }


    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        platform.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
        platform.transform.SetParent(this.transform, false);
    }
}
