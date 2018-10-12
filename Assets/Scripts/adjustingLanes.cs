using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustingLanes : MonoBehaviour {

    public GameObject hostObject; 

	// Use this for initialization
	void Start () {
        transform.SetParent(hostObject.transform);
	}
}
