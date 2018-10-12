using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour {

    public Text NumberObjectsText;
    public Text DangerText;
    int last = 0;
    Text myText;
	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        int objects = spawn.carCount;

        if(objects != last)
        {
            myText.text = "Score: " + spawn.carCount;
            myText.color = new Color(objects * 0.1f, 0, 0);
            if (objects > 10)
            {
                DangerText.text = "High";
                DangerText.color = new Color(1, 0, 0);
            }
            else if (objects > 5)
            {
                DangerText.text = "Medium";
                DangerText.color = new Color(0.5f, 0.2f, 0);
            }
            else if (objects > 0)
            {
                DangerText.text = "Low";
                DangerText.color = new Color(0, 1f, 0);
            }

        }
       
		
	}
}
