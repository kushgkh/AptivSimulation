using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class IOtest : MonoBehaviour {


    JArray objPos;
    public GameObject movingObject;
    public GameObject myCar;

    // Use this for initialization
    void Start () {


        string json = System.IO.File.ReadAllText(@"/Users/kushg/Documents/Codebase/test.txt");

        //string json = System.IO.File.ReadAllText(@"/Users/nileshgupta/Documents/datasets/hwy_data/hwy7.txt");

        // Creates Object from json string 
        JObject o1 = (JObject)JToken.Parse(json);
        objPos = (Newtonsoft.Json.Linq.JArray)o1["Time"]["One"];
        Debug.Log(objPos[0]);

        Vector3 v;
        for(int i = 0; i < objPos.Count; i++)
        {
            v = new Vector3((float)objPos[i][0], 0.5f, (float)objPos[i][1]);
            Instantiate(movingObject, myCar.transform.position + v, Quaternion.identity);
        }
        

    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(objPos[2]);
;		
	}
}
