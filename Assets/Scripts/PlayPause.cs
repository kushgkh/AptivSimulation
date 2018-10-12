using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPause : MonoBehaviour {

    public GameObject button;
    public GameObject buttonText;
    public int toDecideWhichPressed = 0; 

	// Use this for initialization
	void Start () {
        Button btn1 = button.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick);
	}

    void TaskOnClick() {
        toDecideWhichPressed++; 
        if (toDecideWhichPressed % 2 == 0) { // even so play 
            Time.timeScale = 0.7f; 
            buttonText.GetComponent<Text>().text = "Pause";
        }
        else { // odd so pause 
            Time.timeScale = 0;
            Debug.Log("Simulation has been paused");
            buttonText.GetComponent<Text>().text = "Play";
        }
    }

}
