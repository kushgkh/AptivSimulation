using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gaze : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Renderer r = this.GetComponentInChildren<Renderer>();
        r.material.SetColor("_Color", new Color(1, 0, 0, 0.5f));


    }
	
	// Update is called once per frame
	void Update () {
        Quaternion delta = Quaternion.Euler(0, GazeData.gazeYaw, 0);

        this.transform.rotation = delta;

    }
}
