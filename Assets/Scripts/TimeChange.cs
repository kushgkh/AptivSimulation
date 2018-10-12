using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeChange : MonoBehaviour
{

    public GameObject givenInput;
    //public GameObject spawner;
    //spawner.GetComponent<JsonParser>()

    //JsonParser jsonParser; 

    // Use this for initialization
    void Start()
    {
        InputField inputField = givenInput.GetComponent<InputField>();
        inputField.onEndEdit.AddListener(ChangesTime);
        //jsonParser = spawner.GetComponent<JsonParser>(); 
    }

    void ChangesTime(string userInput)
    {
        try
        {
            float targetTime = float.Parse(userInput);
            if (targetTime < 0)
            {
                Debug.LogError("Enter positive value");
            }
            if (targetTime > Time.time)
            { // want to jump to past time
                JsonParser.deltaTime = Time.time - targetTime; // should be negative
            }
            else if (targetTime < Time.time)
            { // want to jump to future time
                JsonParser.deltaTime = Time.time - targetTime; // should be positive
                int count = 0;
                foreach (var i in JsonParser.timesToCreate) {
                    if (i > targetTime)
                    {
                        if (count != 0)
                        {
                            if (JsonParser.timesToCreate[count - 1] < targetTime)
                            {
                                JsonParser.structCount = count;
                            }
                            else
                            {
                                JsonParser.structCount = count - 1;
                            }
                        }
                        else
                        {
                            JsonParser.structCount = 0;
                        }
                        break;
                    }
                    count++;
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
            Debug.LogError("Not a valid time Please enter float");
        }
    }
}