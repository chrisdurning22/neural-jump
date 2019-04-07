using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraFollowNEAT : MonoBehaviour {

    public GameObject controller;
    private Transform t;
    NEATPlayerOne [] scripts;

    void Start () {
        scripts = controller.GetComponentsInChildren<NEATPlayerOne>();

        // scripts = the scripts connected to the robot gameobjects
        // .Aggregate - get the transform of the gameobject furthest right (largest x value) store it in t
        // t is used to control the camera
        t = (scripts.Aggregate((i1,i2) => i1.gameObject.transform.position.x > i2.gameObject.transform.position.x ? i1 : i2)).transform;
        transform.position = new Vector3(t.position.x, transform.position.y, transform.position.z);
    }
	void Update () {
        t = (scripts.Aggregate((i1,i2) => i1.gameObject.transform.position.x > i2.gameObject.transform.position.x ? i1 : i2)).transform;
        transform.position = new Vector3(t.position.x, transform.position.y, transform.position.z);
        
	}
}

