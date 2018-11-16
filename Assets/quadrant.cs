using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quadrant : MonoBehaviour {

    public Image left;
    public Image mid;
    public Image right;

    float lefttimer = 0;
    float midtimer = 0;
    float righttimer = 0;
	// Use this for initialization
	void Start () {
        left.color = new Color(1, 0, 0 , 0.4f);
        mid.color = new Color(1, 0, 0, 0.4f);
        right.color = new Color(1, 0, 0, 0.4f);


    }
	
	// Update is called once per frame
	void Update () {

        lefttimer-=Time.deltaTime;
        midtimer -= Time.deltaTime;
        righttimer -= Time.deltaTime;

        if (GazeData.quad == 0)
        {
            left.color = new Color(0, 1, 0, 0.4f);
            lefttimer = 5;
        }
        else if (GazeData.quad == 1)
        {
            mid.color = new Color(0, 1, 0, 0.4f);
            midtimer = 5;
        }
        else if (GazeData.quad == 2)
        {
            right.color = new Color(0, 1, 0, 0.4f);
            righttimer = 5;
        }


        if(lefttimer < 0)
        {
            left.color = new Color(1, 0, 0, 0.4f);
        }
        if (midtimer < 0)
        {
            mid.color = new Color(1, 0, 0, 0.4f);
        }
        if (righttimer < 0)
        {
            right.color = new Color(1, 0, 0, 0.4f);
        }









    }
}
