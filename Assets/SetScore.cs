using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour {

    public Text NumberObjectsText;
    public Text DangerText;
    public static string drowsyScore;
    int last = -2;
    Text myText;
	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        DangerText.text = "Low";
        DangerText.color = new Color(0, 1f, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        int objects = spawn.dangerCars;

        myText.text = "Driver Awareness: " + drowsyScore;

        if (objects != last)
        {
            last = objects;
           
            myText.color = new Color(objects * 0.1f, 0, 0);
            if (objects > 1)
            {
                DangerText.text = "High";
                DangerText.color = new Color(1, 0, 0);
            }
            else if (objects > 0)
            {
                DangerText.text = "Medium";
                DangerText.color = new Color(0.5f, 0.2f, 0);
            }
            else if (objects > -1)
            {
                DangerText.text = "Low";
                DangerText.color = new Color(0, 1f, 0);
            }

        }
       
		
	}
}
