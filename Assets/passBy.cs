using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passBy : MonoBehaviour {


    float frac = 0;
    bool close = false;
    float movepos;
    float oldpos;
	// Use this for initialization
	void Start () {
        oldpos = transform.position.x;
        movepos = transform.position.x;


    }

    // Update is called once per frame
    void Update () {

        //spawn.zcount += this.transform.position.z;
        //+=0.015f;
        frac += 0.020f;
        transform.position = new Vector3(Mathf.Lerp(oldpos, movepos, frac), transform.position.y, transform.position.z - Random.value);
        float val = Random.value * 100;
        float z = transform.position.x;
        if(z > 4 && GazeData.quad == 2 ) 
        {

            MeshRenderer rend = GetComponentInChildren<MeshRenderer>();
            //Set the main Color of the Material to green

            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.green);
        }
        else if (Mathf.Abs(z) < 4 && GazeData.quad == 1)
        {

            MeshRenderer rend = GetComponentInChildren<MeshRenderer>();
            //Set the main Color of the Material to green

            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.green);
        }
        else if (z < -4 && GazeData.quad == 0)
        {

            MeshRenderer rend = GetComponentInChildren<MeshRenderer>();
            //Set the main Color of the Material to green

            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.green);
        }
        //val 98
        if (val > 98 && frac > 1 && transform.position.z > 20)
        {
            frac = 0;
            oldpos = transform.position.x;
            if (Mathf.Abs(movepos) > 0)
            {
                movepos = 0;
            }
            else
            {
                val = Random.value * 100;
                if (val > 50)
                    movepos = -5;
                else
                    movepos = 5;

            }
        }
        if(transform.position.z < 20 && !close)
        {
  
            spawn.dangerCars += 1;
            close = true;
        }

        if (transform.position.z < 20 && movepos==0)
        {
            
            frac = 0;
            oldpos = transform.position.x;
            val = Random.value * 100;
            if (val > 50)
                movepos = -5;
            else
                movepos = 5;

        }


        if (transform.position.z < -10)
        {
            spawn.carCount -= 1;
            spawn.dangerCars -= 1;
            GameObject.Destroy(this.gameObject);
        }

    }
}
