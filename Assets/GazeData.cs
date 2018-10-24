using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;


public class GazeData : MonoBehaviour {

    JArray gazeData;
    int i = 1500;
    // Use this for initialization
    void Start () {
        string json = System.IO.File.ReadAllText(@"/Users/kushg/Documents/Codebase/data.json");
        gazeData = (JArray)JToken.Parse(json);
        JObject g = (JObject)gazeData[4];
        Debug.Log("This is obj one " + g);
        Debug.Log("This is gazeValue one " + g["Headpose.yaw"]);
        Debug.Log("This is score");
        Debug.Log("Size of array" + gazeData.Count);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // up and down   left right  roll
        //

        JObject state = (JObject)gazeData[i];
        float yaw = (float)state["Headpose.yaw"];
        float pitch = (float)state["Headpose.pitch"];
        float roll = (float)state["Headpose.roll"];

        yaw *= 180 / 3.1415f;
        pitch *= 180 / 3.1415f;
        roll *= 180 / 3.1415f;

        Quaternion delta = Quaternion.Euler(pitch, yaw-180, roll);
        i++;





        this.transform.rotation = delta;


    }
}
