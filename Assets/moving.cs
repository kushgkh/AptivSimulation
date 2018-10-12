using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class moving : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(this.transform.position.z);
        this.transform.position = new Vector3(this.transform.position.x , this.transform.position.y  , this.transform.position.z -1);
		if(this.transform.position.z < -80)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        }
	}
}
