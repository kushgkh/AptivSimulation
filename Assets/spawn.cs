﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

    public static int carCount = 0;
    public static float zcount = 0;
    List<GameObject> cars;
    public GameObject movingObject;
    public GameObject myCar;
    int cooldown = 100;

    // Use this for initialization
    void Start () {
        //GameObject newCar = Instantiate(movingObject, myCar.transform.position + new Vector3(-5, 0.5f, 60), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
        zcount = 0;
        float time = Random.value * 100;
        cooldown++;


        if(time > 98  && cooldown > 100)
        {
            carCount++;
            cooldown = 0;
            float t = Random.value; 
            Vector3 v;
            if(t > 0.5)
            {
                v = new Vector3(-5, 0.5f, 140);
            }
            else
            {
                v = new Vector3(5, 0.5f, 140);
            }


            GameObject newCar = Instantiate(movingObject, myCar.transform.position + v, Quaternion.identity);
            cars.Add(newCar);
        }
       

        



    }
}
