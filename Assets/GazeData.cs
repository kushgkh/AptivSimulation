using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;


public class GazeData : MonoBehaviour {

    JArray gazeData;
    int i = 2000;


    public static int quad = 0;
    public static float gazeYaw = 0;

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
        float drows = (float)state["Drowsiness.quality"];
        drows *= 7;


        SetScore.drowsyScore = "" + drows ;

        yaw *= 180 / 3.1415f;


        pitch *= 180 / 3.1415f;
        roll *= 180 / 3.1415f;
        gazeYaw = yaw - 2.5f; // slight delta due to error?

        if (Mathf.Abs(gazeYaw) < 5)
        {
            quad = 1;
        }
        else if(gazeYaw < -5)
        {
            quad = 0;
        }
        else
        {
            quad = 2;
        }
        Quaternion delta = Quaternion.Euler(pitch, yaw-180, roll);
        i++;





        this.transform.rotation = delta;


    }
}
