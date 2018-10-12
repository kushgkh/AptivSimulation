using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{

    public GameObject brwheel;
    public GameObject blwheel;
    public GameObject frwheel;
    public GameObject flwheel;
    //public GameObject objInfo;
    public float velocity;
    string currSpeedText;
    string currConfidenceLevelText;
    string currObjTypeText;
    private static float starting_velocity = 1.0f;
    //HashSet<GameObject> newInfos = new HashSet<GameObject>(); 

    GameObject newInfo;

    // Use this for initialization
    void Start() {
        //newInfo = Instantiate<GameObject>(objInfo, new Vector3(transform.localPosition.x - 2, transform.localPosition.y + 5, transform.localPosition.z), Quaternion.identity);
        //newInfo.transform.SetParent(transform);
        //newInfo.transform.localScale = new Vector3(0.0125f, 0.0125f, 0.0125f);
        //currSpeedText = newInfo.transform.GetChild(0).GetComponent<TextMesh>().text;
        //currObjTypeText = newInfo.transform.GetChild(1).GetComponent<TextMesh>().text;
        //currConfidenceLevelText = newInfo.transform.GetChild(2).GetComponent<TextMesh>().text;
    }

    // Update is called once per frame
    void Update() {
        if (JsonParser.applicationHasStopped == true) {
            return; 
        }
        // setting speed
        //Vector3 local_pos = transform.localPosition;
        //local_pos.z += velocity;
        //transform.localPosition = local_pos;

        // rotations
        float rim_velocity = 23 * (starting_velocity + velocity);
        //hostSpeed = JsonParser.zeroSpeed; // obtain host car current speed
        if (brwheel != null && JsonParser.shouldMove == true) {
            brwheel.transform.RotateAround(brwheel.transform.position, brwheel.transform.right, rim_velocity);
        }
        if (blwheel != null && JsonParser.shouldMove == true) {
            blwheel.transform.RotateAround(blwheel.transform.position, blwheel.transform.right, rim_velocity);
        }
        if (frwheel != null && JsonParser.shouldMove == true) {
            frwheel.transform.RotateAround(frwheel.transform.position, frwheel.transform.right, rim_velocity);
        }
        if (flwheel != null && JsonParser.shouldMove == true) {
            flwheel.transform.RotateAround(flwheel.transform.position, flwheel.transform.right, rim_velocity);
        }
    }
}
