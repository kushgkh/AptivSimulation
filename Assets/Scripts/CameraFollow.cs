using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject toFollow;
    public GameObject hostObject;
    //private Vector3 startPos; 

	// Use this for initialization
	void Start () {
        transform.SetParent(hostObject.transform);
	}
}
