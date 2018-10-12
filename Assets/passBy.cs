using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passBy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //spawn.zcount += this.transform.position.z;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Random.value/2);
        if (transform.position.z < -7)
        {
            spawn.carCount -= 1;
            GameObject.Destroy(this.gameObject);
        }

    }
}
