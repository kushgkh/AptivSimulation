using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Shims;
using System.Linq;
using System;
using UnityEngine.UI;

// Struct to hold time the object will be created at, as well as which indices, as well as at which array number they were found 
public struct timeProperties
{
    public float sTime;
    public int sIndex;
    public int sArrayNum;
    public List<int> sIndicies;
    public List<int> sArrayNums;
    // constructor to create struct with time, list of indices, list of array numbers 
    public timeProperties(float t, List<int> si, List<int> sa)
    {
        sTime = t;
        sIndicies = si;
        sArrayNums = sa;
        sIndex = 0;
        sArrayNum = 0;
    }
    // constructor to create struct with time, index, and array number of independent object 
    public timeProperties(float t, int i, int a)
    {
        sTime = t;
        sIndex = i;
        sArrayNum = a;
        sIndicies = null;
        sArrayNums = null;
    }
}



public class JsonParser : MonoBehaviour
{
    // boolean to detect when application is completed 
    static protected internal bool applicationHasStopped = false;

    // Stores Host information
    JArray hostHeading = new JArray();
    JArray hostSpeed = new JArray();
    JArray hostXLoc = new JArray();
    JArray hostYLoc = new JArray();


    public static float zeroSpeed = 0;
    private float zeroX = 0;
    private float zeroY = 0;
    public static float maxX = float.MinValue; // assume the smallest
    public static float maxY = float.MinValue; // assume the smallest
    public static float minX = float.MaxValue; // assume the largest
    public static float minY = float.MaxValue; // assume the largest

    // Stores Object Information
    JArray objSpeed = new JArray();
    JArray objXLoc = new JArray();
    JArray objYLoc = new JArray();
    JArray objHeading = new JArray();
    JArray alreadySpawned = new JArray();
    JArray objTimeCreated = new JArray();
    JArray objXWidth = new JArray();
    JArray objYLength = new JArray();
    JArray objConfidenceLevel = new JArray();
    JArray objType = new JArray();
    JArray objID = new JArray();
    JArray objMoveable = new JArray();
    JArray objLat = new JArray();
    JArray objLong = new JArray();
    JArray objvHead = new JArray();
    JArray objQuality = new JArray();
    JArray objUpdateTime = new JArray();
    JArray createSpeed = new JArray();
    JArray createID = new JArray();
    JArray createMoveable = new JArray();
    JArray createQuality = new JArray();
    JArray createX = new JArray();
    JArray createY = new JArray();
    JArray createHeading = new JArray();
    JArray createXWidth = new JArray();
    JArray createYLength = new JArray();
    JArray createConfidenceLevel = new JArray();
    JArray createObjType = new JArray();
    JArray createLat = new JArray();
    JArray createLong = new JArray();
    JArray createvHead = new JArray();

    public static float hostHead = 0;

    // Boolean variable to decide whether or not to spawn 
    private bool timeToSpawn = false;

    // List of all the objects 
    List<GameObject> objects = new List<GameObject>();

    // List of Array Number that specific index is currently on 
    List<int> ListBetterCounts = new List<int>();

    // List of Array Number the object was last updated at 
    // List<int> LastUpdatedValue = new List<int>();

    // GameObjects neeeded to reference 
    public GameObject movingObject;
    public GameObject hostObject;
    public GameObject endText;
    public GameObject path;
    public GameObject dashedLaneLeft;
    public GameObject dashedLaneRight;
    public GameObject meshQuad;
    public GameObject leftWheel;
    public GameObject rightWheel;
    public GameObject speedText;
    public GameObject startEndText;
    public GameObject carCountText;
    public GameObject threatObjectDemo;
    public GameObject currentTimeText;
    bool threat1 = false;
    bool threat2 = false;
    bool threat3 = false;
    bool threat4 = false;
    int frameStall = 0;

    // Current Array Number 
    private int mainCount = 0;

    // Current Object Count
    int ObjCount = 0;

    //// list of booleans to check if tracked object already added to struct 
    List<bool> sortedAlreadyAdded = new List<bool>();
    List<bool> sortedAlreadyAdded2 = new List<bool>();

    // list of structs for created objects 
    List<timeProperties> timePropStructs = new List<timeProperties>();
    List<timeProperties> sortedStructsTwo = new List<timeProperties>();
    List<timeProperties> timeUpdateStruct = new List<timeProperties>();
    List<timeProperties> timeUpdateStructFinal = new List<timeProperties>();

    // time to adjust to due to time being lost in updating objects 
    public static float deltaTime = 0.0f;

    // Holds current indices to create in update frame
    List<int> targetTimeIndices = new List<int>();

    // Holds current array numbers to account for in update frame 
    List<int> currentArrayNumbers = new List<int>();

    // Times to Create 
    public static List<float> timesToCreate = new List<float>();
    public static int structCount = 0;

    public static bool shouldMove = true;
    private int prevMainCount = -1;

    float zeroHeading = 0.0f;

    StreamWriter sw;

    // Use this for initialization
    void Start()
    {

        // Reads in json as string 
        
        string json = System.IO.File.ReadAllText(@"/Users/kushg/Documents/Codebase/hwy7.txt");

        //string json = System.IO.File.ReadAllText(@"/Users/nileshgupta/Documents/datasets/hwy_data/hwy7.txt");

        // Creates Object from json string 
        JObject o1 = (JObject)JToken.Parse(json);



        // Obtain Host Information
        hostSpeed = (Newtonsoft.Json.Linq.JArray)o1["hostInfo"]["speed"];
        hostXLoc = (Newtonsoft.Json.Linq.JArray)o1["hostProps"]["x"];
        hostYLoc = (Newtonsoft.Json.Linq.JArray)o1["hostProps"]["y"];
        hostHeading = (Newtonsoft.Json.Linq.JArray)o1["hostProps"]["heading"];
        zeroHeading = ((float)hostHeading[0] * 180.0f) / Mathf.PI;
        Debug.Log("Zero Heading is : " + zeroHeading);

        // Obtain Object Information
        objSpeed = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["speed"]; // list of lists 
        objXLoc = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["xposn"];
        objYLoc = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["yposn"];
        objHeading = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["heading"];
        objTimeCreated = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["time_created"];
        objXWidth = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["length"];
        objYLength = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["width"];
        objConfidenceLevel = (Newtonsoft.Json.Linq.JArray)o1["rdrTrk"]["confidenceLevel"];
        objType = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["object_class"];
        objID = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["id"];
        objQuality = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["quality"];
        objMoveable = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["f_moveable"];
        objLat = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["vcs_latposn"];
        objLong = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["vcs_longposn"];
        objvHead = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["vcs_heading"];
        objUpdateTime = (Newtonsoft.Json.Linq.JArray)o1["fusTrk"]["time_last_detected"];

   

        foreach (var i in objLat)
        {
            foreach (var j in i)
            {
                if (j.Type != JTokenType.Null)
                {
                    if ((float)j > maxX)
                    {
                        maxX = (float)j;
                    }
                    if ((float)j < minX)
                    {
                        minX = (float)j;
                    }
                }
            }
        }

        foreach (var i in objLong)
        {
            foreach (var j in i)
            {
                if (j.Type != JTokenType.Null)
                {
                    if ((float)j > maxY)
                    {
                        maxY = (float)j;
                    }
                    if ((float)j < minY)
                    {
                        minY = (float)j;
                    }
                }
            }
        }

      //  Debug.Log("Max X is : " + maxX);
      //  Debug.Log("Min X is : " + minX);
       // Debug.Log("Max Y is : " + maxY);
       // Debug.Log("Min Y is : " + minY);

        endText.transform.SetParent(hostObject.transform);


        //MeshRenderer meshRenderer = meshQuad.GetComponent<MeshRenderer>();
        //meshRenderer.ver
        Mesh the_mesh = meshQuad.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = new Vector3[4];

        // add or subtract 5 to give space for min/max object to be completely on mesh
        vertices[0] = new Vector3(minX - 5, -0.06f, minY - 5);
        vertices[1] = new Vector3(maxX + 5, -0.06f, minY - 5);
        vertices[2] = new Vector3(minX - 5, -0.06f, maxY + 5);
        vertices[3] = new Vector3(maxX + 5, -0.06f, maxY + 5);
        the_mesh.vertices = vertices;

        int[] tri = new int[6];
        //  lower left triangle.
        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;
        //  upper right triangle.   
        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 1;
        the_mesh.triangles = tri;


        Vector3[] normals = new Vector3[4];
        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;
        the_mesh.normals = normals;


        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);
        the_mesh.uv = uv;

        the_mesh.RecalculateBounds();

        //Debug.Log("The mesh has been set with max x : " + maxX + " min x : " + minX + " max y : " + maxY + " min Y : " + minY);

        // handle path line (where hostObject follows)
        LineRenderer lineRenderer = path.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3;
        lineRenderer.widthMultiplier = 0.02f;
        var points = new Vector3[hostXLoc.Count];
        //for (int i = 0; i < hostXLoc.Count; i++)
        //{
        //    points[i] = new Vector3((float)hostXLoc.ElementAt<JToken>(i), 0.0f, (float)hostYLoc.ElementAt<JToken>(i));
        //}
        points[0] = new Vector3(0, 0, minY - 5);
        points[1] = new Vector3(0, 0, 0);
        points[2] = new Vector3(0, 0, maxY + 5);
        lineRenderer.SetPositions(points);

        // handle left dashed Line 
        LineRenderer leftLineRenderer = dashedLaneLeft.GetComponent<LineRenderer>();
        leftLineRenderer.widthMultiplier = 0.02f;
        // handle right dashed line
        LineRenderer rightLineRenderer = dashedLaneRight.GetComponent<LineRenderer>();
        rightLineRenderer.widthMultiplier = 0.02f;


        float wheelDistance = rightWheel.transform.position.x - leftWheel.transform.position.x;
        // any offset works here just choosing 0.1 for each side
        wheelDistance += 1.5f;

       
        leftLineRenderer.positionCount = 3;
        rightLineRenderer.positionCount = 3;

        var leftPoints = new Vector3[3];
        var rightPoints = new Vector3[3];

        leftPoints[0] = new Vector3(-(wheelDistance / 2), 0, minY - 5);
        leftPoints[1] = new Vector3(-(wheelDistance / 2), 0, 0);
        leftPoints[2] = new Vector3(-(wheelDistance / 2), 0, maxY + 5);

        rightPoints[0] = new Vector3(wheelDistance / 2, 0, minY - 5);
        rightPoints[1] = new Vector3(wheelDistance / 2, 0, 0);
        rightPoints[2] = new Vector3(wheelDistance / 2, 0, maxY + 5);

        leftLineRenderer.SetPositions(leftPoints);
        rightLineRenderer.SetPositions(rightPoints);


        //Vector3[] leftPointsSet = leftPoints.ToArray();
        //Vector3[] rightPointsSet = rightPoints.ToArray();

        //leftLineRenderer.SetPositions(leftPointsSet);
        //rightLineRenderer.SetPositions(rightPointsSet);

        //var distanceLeft = Vector3.Distance(leftPointsSet.First<Vector3>(), leftPointsSet.Last<Vector3>());
        leftLineRenderer.materials[0].mainTextureScale = new Vector3(500, 1, 1);

        // var distanceRight = Vector3.Distance(rightPointsSet.First<Vector3>(), rightPointsSet.Last<Vector3>());
        rightLineRenderer.materials[0].mainTextureScale = new Vector3(500, 1, 1);

        //bool allNulls = true; // assume all null at first

        int numObjects = 0;

        // Add in values to Arrays/Lists
        foreach (var i in (Newtonsoft.Json.Linq.JArray)objSpeed.ElementAt<JToken>(0))
        {
            alreadySpawned.Add(false);
            objects.Add(null);
            ListBetterCounts.Add(0);
            //LastUpdatedValue.Add(0);
            sortedAlreadyAdded.Add(false);
            sortedAlreadyAdded2.Add(false);
            numObjects++;

        }


        int arrayNum = 0;
  
        int aNum = 0;
        foreach (var i in objTimeCreated)
        {
            int count = 0;
            foreach (var j in i)
            {
                if (sortedAlreadyAdded[count] == false)
                {

                    if (j.Type != JTokenType.Null && (bool)objMoveable[aNum][count] == true)
                    {
                        timeProperties newProp = new timeProperties((float)j, count, arrayNum);
                        timePropStructs.Add(newProp);
                        sortedAlreadyAdded[count] = true;
                    }
                    else
                    {
                        if (sortedAlreadyAdded[count] == true)
                        {
                            sortedAlreadyAdded[count] = false;
                        }
                    }
                }
                else
                {
                    if (j.Type == JTokenType.Null)
                    {
                        sortedAlreadyAdded[count] = false;
                    }
                }
                count++;
            }
            arrayNum++;
        }

        int arrayNumb = 0;
        int bNum = 0;
        foreach (var i in objUpdateTime)
        {
            int count = 0;
            foreach (var j in i)
            {
                if (j.Type != JTokenType.Null && (bool)objMoveable[bNum][count] == true)
                {
                    timeProperties newProp = new timeProperties((float)j, count, arrayNumb);
                    timeUpdateStruct.Add(newProp);
                }
                count++;
            }
            arrayNumb++;
        }



        timeProperties[] sortedStructs = timePropStructs.ToArray();


        Array.Sort<timeProperties>(sortedStructs, (x, y) => x.sTime.CompareTo(y.sTime));


        timeProperties[] timeUpdateStruct2 = timeUpdateStruct.ToArray();


        Array.Sort<timeProperties>(timeUpdateStruct2, (x, y) => x.sTime.CompareTo(y.sTime));





        // create array of arrays for what indices to spawn and what arrayNumber they were created in 
        for (int i = 0; i < sortedStructs.Length; i++)
        {
            int jump = 0;
            List<int> tempIndices = new List<int>();
            List<int> tempArrayNums = new List<int>();
            tempIndices.Add(sortedStructs[i].sIndex);
            tempArrayNums.Add(sortedStructs[i].sArrayNum);
            for (int j = i + 1; j < sortedStructs.Length; j++)
            {
                if (System.Math.Abs(sortedStructs[i].sTime - sortedStructs[j].sTime) < 0.01)
                {
                    tempArrayNums.Add(sortedStructs[j].sArrayNum);
                    tempIndices.Add(sortedStructs[j].sIndex);
                    jump++;
                }
            }
            i += jump;
            //sortedIndicesTwo.Add(tempIndices);
            timeProperties newStruct = new timeProperties(sortedStructs[i].sTime, tempIndices, tempArrayNums);
            sortedStructsTwo.Add(newStruct);
        }

        // create array of arrays for what indices to spawn and what arrayNumber they were created in 
        for (int i = 0; i < timeUpdateStruct2.Length; i++)
        {
            int jump = 0;
            List<int> tempIndices = new List<int>();
            List<int> tempArrayNums = new List<int>();
            tempIndices.Add(timeUpdateStruct2[i].sIndex);
            tempArrayNums.Add(timeUpdateStruct2[i].sArrayNum);
            for (int j = i + 1; j < timeUpdateStruct2.Length; j++)
            {
                if (System.Math.Abs(timeUpdateStruct2[i].sTime - timeUpdateStruct2[j].sTime) < 0.01)
                {
                    tempArrayNums.Add(timeUpdateStruct2[j].sArrayNum);
                    tempIndices.Add(timeUpdateStruct2[j].sIndex);
                    jump++;
                }
            }
            i += jump;
            List<int> addIndicies = new HashSet<int>(tempIndices).ToList();
            List<int> addArrayNums = new HashSet<int>(tempArrayNums).ToList();
            timeProperties newStruct = new timeProperties(timeUpdateStruct2[i].sTime, addIndicies, addArrayNums);
            timeUpdateStructFinal.Add(newStruct);
        }


        foreach (var i in sortedStructsTwo)
        {
            timesToCreate.Add(i.sTime);
        }


        // Get Host to start moving 
        mainCount = sortedStructsTwo[0].sArrayNums[0];
        updateHost(mainCount);

        endText.GetComponent<TextMesh>().text = "";
        startEndText.GetComponent<Text>().text = "";


        float firstCreateTime = sortedStructsTwo[0].sTime;
        if (firstCreateTime > 6.0f)
        {
            deltaTime = -(sortedStructsTwo[0].sTime - 5.0f);
        }


        sw = new StreamWriter("data_logger.txt");
        sw.WriteLine("ID" + "\t" + "Lat" + "\t" + "Long" + "\t" + "Speed" + "\t" + "Heading" + "\t" + "Length" + "\t" + "Width" + "\t" + "Time Created" + "\t" + "Time Updated");
        sw.Flush();

        Time.timeScale = 1f; 
    } // end of Start function 



    // updates Host movement 
    void updateHost(int arrayNumber)
    {
        if (arrayNumber < hostSpeed.Count)
        {
            if (prevMainCount == arrayNumber)
            {
                shouldMove = false;
            }
            else
            {
                shouldMove = true;
                zeroSpeed = (float)hostSpeed.ElementAt<JToken>(arrayNumber); // get host speed
                float convertedSpeed = (float)Math.Ceiling((float)zeroSpeed / 4.0f * 9.0f);
                speedText.GetComponent<Text>().text = convertedSpeed.ToString();
            }

            prevMainCount = arrayNumber;
        }
    } // end of update host function

    void Update()
    {
        if (Time.timeScale > 0)
        {
            carCountText.GetComponent<Text>().text = ObjCount.ToString();
            if (applicationHasStopped)
            {
                //Debug.Log("Simulation has ended");
                return;
            }
            //Debug.Log("Main count is : " + mainCount);
            // End of Simulation handler 
            if (mainCount >= hostSpeed.Count)
            {
                int localIndex = 0;
                foreach (var i in objects)
                {
                    Destroy(i);
                    ObjCount = 0;
                    alreadySpawned[localIndex] = false;
                    objects[localIndex] = null;
                    localIndex++;
                }
                startEndText.GetComponent<Text>().text = "Fin.";
                applicationHasStopped = true;
                return;
            }

            if (timeToSpawn == false)
            {
                timeToSpawn = checkTimeToSpawn();
            }


            if (currentArrayNumbers.Count > 0)
            {
                if (mainCount < currentArrayNumbers[0])
                {
                    mainCount++;
                }
                else
                {
                    currentArrayNumbers.RemoveAt(0);
                }
            }


            //if (sortedStructsTwo.Count == 0) {
            //    mainCount++;
            //    updateHost(mainCount);
            //}
            if (sortedStructsTwo.Count - 1 == structCount)
            {
                mainCount++;
                updateHost(mainCount);
            }


            currentTimeText.GetComponent<Text>().text = "Time: " + (Time.time - deltaTime);
            updateObjectMovement();
            // Create new Object 
            if (timeToSpawn)
            {
                //Debug.Log("It is time to spawn, and I will now spawn everything in targetTimeIndices");
                // Go through all indices that are ready to be spawned 
                foreach (var i in targetTimeIndices)
                {
                    createObject(ListBetterCounts[i], i);
                    ListBetterCounts[i] += 1;
                    // LastUpdatedValue[i] += 1; 
                }
                timeToSpawn = false;
            }
            else
            {
                int localCount = 0;
                // Update Objects that have Already been Spawned
                DateTime time1 = DateTime.Now;
                // if (shouldMove == true)
                // {
                
                foreach (var i in alreadySpawned)
                {
                    if (i.Type != JTokenType.Null)
                    {
                        if ((bool)i == true)
                        {
                            //  if (ListBetterCounts[localCount] > mainCount) {
                            //      updateObjectMovement(ListBetterCounts[localCount], localCount);
                            //      //ListBetterCounts[localCount]++; 
                            //  }
                            //  else {
                            //   if (shouldMove == true) {
                            //       updateObjectMovement(mainCount, localCount);
                            //   }
                            //   }
                            updateObjectMovement();
                            //ListBetterCounts[localCount] += 1;
                        }
                    }
                    localCount++;
                }
                // }
                deltaTime += (float)DateTime.Now.Subtract(time1).TotalSeconds;
            }
            updateHost(mainCount);
  
        }
    } 
    int time = 0;
    bool checkTimeToSpawn()
    {
       
        // make sure here that sortedStructs2 is within bounds also for tomorrow figure out the host situation (where its at array number wise) 
        if (structCount < sortedStructsTwo.Count)
        {
            //Debug.Log("Checking if it is time to spawn");
            float targetTime = sortedStructsTwo[structCount].sTime;
            // Debug.Log(deltaTime);
            //Debug.Log(Time.fixedTime);
            // Debug.Log("Time in Range is : " + targetTime + " And time is " + Time.time + " but adjusted time is : " + (Time.time - deltaTime));
            if (Time.fixedTime > 3 * time)
            {
                time++;
                //Debug.Log("Something should be spawning fam");
                timeToSpawn = true;
                targetTimeIndices = sortedStructsTwo[structCount].sIndicies;
                currentArrayNumbers = sortedStructsTwo[structCount].sArrayNums;
                for (int i = 0; i < targetTimeIndices.Count; i++)
                {
                    ListBetterCounts[targetTimeIndices[i]] = sortedStructsTwo[structCount].sArrayNums[i];
                }
                structCount++;
                //Debug.Log("Object going to be spawned so it has been removed from sorted Times, length is now : " + sortedStructs.Count);
            }
        }
        else
        {
            applicationHasStopped = true;
        }
        return timeToSpawn;
    } // end of checkTimeToSpawn function



    void createObject(int arrayNumber, int indexInArray)
    {
        createSpeed = (Newtonsoft.Json.Linq.JArray)objSpeed.ElementAt<JToken>(arrayNumber);
        // createX = (Newtonsoft.Json.Linq.JArray)objXLoc.ElementAt<JToken>(arrayNumber);
        // createY = (Newtonsoft.Json.Linq.JArray)objYLoc.ElementAt<JToken>(arrayNumber);
        // createHeading = (Newtonsoft.Json.Linq.JArray)objHeading.ElementAt<JToken>(arrayNumber);
        createLat = (Newtonsoft.Json.Linq.JArray)objLat.ElementAt<JToken>(arrayNumber);
        createLong = (Newtonsoft.Json.Linq.JArray)objLong.ElementAt<JToken>(arrayNumber);
        createvHead = (Newtonsoft.Json.Linq.JArray)objvHead.ElementAt<JToken>(arrayNumber);
        createID = (Newtonsoft.Json.Linq.JArray)objID.ElementAt<JToken>(arrayNumber);
        createQuality = (Newtonsoft.Json.Linq.JArray)objQuality.ElementAt<JToken>(arrayNumber);
        createMoveable = (Newtonsoft.Json.Linq.JArray)objMoveable.ElementAt<JToken>(arrayNumber);
        createXWidth = (Newtonsoft.Json.Linq.JArray)objXWidth.ElementAt<JToken>(arrayNumber);
        createYLength = (Newtonsoft.Json.Linq.JArray)objYLength.ElementAt<JToken>(arrayNumber);
        JToken currSpeed = createSpeed[indexInArray];
        //JToken currX = createX[indexInArray];
        //JToken currY = createY[indexInArray];
        //JToken currHead = createHeading[indexInArray];
        JToken currID = createID[indexInArray];
        JToken currMoveable = createMoveable[indexInArray];
        JToken currQuality = createQuality[indexInArray];
        JToken currWidth = createXWidth[indexInArray];
        JToken currLength = createYLength[indexInArray];
        JToken currLat = createLat[indexInArray];
        JToken currLong = createLong[indexInArray];
        JToken currVHead = createvHead[indexInArray];
        if (currLat.Type != JTokenType.Null && currLong.Type != JTokenType.Null && (bool)currMoveable == true && currVHead.Type != JTokenType.Null)
        {
            float newHeading = ((float)currVHead * 180.0f) / Mathf.PI;
            if ((((float)currSpeed >= 0) && (newHeading <= 90 && newHeading >= -90)) || (((float)currSpeed <= 0) && (newHeading <= -90 && newHeading >= 90)))
            {
                /*
                Vector3 pos = new Vector3((float)currLat, 0, (float)currLong);
                Debug.Log("Object Created at arrayNumber " + arrayNumber + " And index Number " + indexInArray + " at Lat/Long " + currLat + "/" + currLong);
                GameObject newCar = Instantiate(movingObject, pos, Quaternion.identity);
                ObjCount++;
                newCar.transform.eulerAngles = new Vector3(newCar.transform.eulerAngles.x, newHeading, newCar.transform.eulerAngles.z);
                newCar.transform.GetChild(0).GetComponent<TextMesh>().text = currID.ToString();
                //newCar.transform.GetChild(0).GetComponent<TextMesh>().text = "";
                if ((float)currQuality > 0.8)
                {
                    newCar.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                }
                else if ((float)currQuality >= 0.4f && (float)currQuality < 0.79999f)
                {
                    newCar.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                }
                else if ((float)currQuality < 0.4f)
                {
                    newCar.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
                objects[indexInArray] = newCar;
                alreadySpawned[indexInArray] = true;

                sw.WriteLine(currID + "\t" + currLat + "\t" + currLong + "\t" + currSpeed + "\t" + newHeading + "\t" + currLength + "\t" + currWidth + "\t" + objTimeCreated[arrayNumber][indexInArray] + "\t" + "NA");
                sw.Flush();
                */
            }
        }
        else
        {
            //Debug.Log("Object at index : " + indexInArray + " at arrayNumber  : " + arrayNumber + " was not created because XLoc is : " + (float)currLat + " and YLoc is : " + (float)currLong);
        }
    } // end of createObject function


    void updateObjectMovement()
    {
        //Debug.Log("I am in the beginning of the function updateObjectMovement with the object at array Number ");
            //JToken timeToUpdateAt = objUpdateTime[arrayNumber][indexInArray];
        JToken timeToUpdateAt = timeUpdateStructFinal[0].sTime;
        //Debug.Log("Trying to update objects at time " + timeToUpdateAt + " , but time is " + (Time.time - deltaTime));
        if (timeToUpdateAt.Type != JTokenType.Null)
        {
            float newHeading = float.NaN;
            if ((Mathf.Abs((Time.time - deltaTime) - (float)timeToUpdateAt) < 0.01) || (Mathf.Abs(((Time.time - deltaTime) - (Time.deltaTime / 2) - (float)timeToUpdateAt)) < 0.01) || (Mathf.Abs(((Time.time - deltaTime) + (Time.deltaTime / 2) - (float)timeToUpdateAt)) < 0.01))
            {
                // means it is time to update
                List<int> objectsToCreate = timeUpdateStructFinal[0].sIndicies;
                foreach (var index in objectsToCreate)
                {

                    Debug.Log("I am updating index " + index);

                    if (objLat[ListBetterCounts[index]][index].Type != JTokenType.Null && objLong[ListBetterCounts[index]][index].Type != JTokenType.Null && objects[index] != null)
                    {
                        objects[index].transform.position = new Vector3((float)objLat[ListBetterCounts[index]][index], 0, (float)objLong[ListBetterCounts[index]][index]);
                        if (objvHead[ListBetterCounts[index]][index].Type != JTokenType.Null)
                        {
                            newHeading = ((float)objvHead[ListBetterCounts[index]][index] * 180.0f) / Mathf.PI;
                        }

                        sw.WriteLine(objID[ListBetterCounts[index]][index] + "\t" + objLat[ListBetterCounts[index]][index] + "\t" + objLong[ListBetterCounts[index]][index] + "\t" + objSpeed[ListBetterCounts[index]][index] + "\t" + newHeading + "\t" + objYLength[ListBetterCounts[index]][index] + "\t" + objXWidth[ListBetterCounts[index]][index] + "\t" + objTimeCreated[ListBetterCounts[index]][index] + "\t" + timeToUpdateAt);
                        sw.Flush();
                        int temp = ListBetterCounts[index];
                        ListBetterCounts[index] = temp + 1;
                    }
                    else
                    {

                        if (objects[index] == null)
                        {

                            if (objLat[ListBetterCounts[index]][index].Type != JTokenType.Null && objLong[ListBetterCounts[index]][index].Type != JTokenType.Null)
                            {
                                createObject(ListBetterCounts[index], index);
                            }
                            else
                            {
                                if ((bool)alreadySpawned[index] == true)
                                {
                                    alreadySpawned[index] = false;
                                }
                            }
                        }
                        else
                        {
                            Destroy(objects[index]);
                            ObjCount--;
                            alreadySpawned[index] = false;
                        }
                    }
                }
                timeUpdateStructFinal.RemoveAt(0); // have finished with that time and have created all indicies at that time, so can remove now


            }
        }
        
    } // end of function updateObjectMovement


    // Sets length and width of object 
    void newScale(GameObject theGameObject, float newZ, float newX) // Z is length, X is width
    {
        //float sizeX = theGameObject.GetComponent<Renderer>().bounds.size.x; // width 
        //float sizeZ = theGameObject.GetComponent<Renderer>().bounds.size.z; // length
        Vector3 rescale = theGameObject.transform.localScale;
        //rescale.x = newX * 19.7839591659f;
        //rescale.z = newZ * 7.72719896765f;
        rescale.x = newX * 0.1f;
        rescale.z = newZ * 0.25f;
        theGameObject.transform.localScale = rescale;
    } // end of newScale function 

    // Validates JSON 
    bool ValidateJSON(string s)
    {
        try
        {
            JToken.Parse(s);
            return true;
        }
        catch (Newtonsoft.Json.JsonReaderException ex)
        {
            System.Diagnostics.Trace.WriteLine(ex);
            return false;
        }
    } // end of ValidateJSON function 
} // end of MonoBehavior 




