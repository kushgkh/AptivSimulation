//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Lanes : MonoBehaviour
//{
//    public GameObject hostCar;
//    public Texture solid_lane;
//    public Texture road_edge_lane;
//    public Texture dashed_lane;
//    public Texture double_lane;
//    public Texture botts_dots_lane;
//    public float solid_tiling_x;
//    public float solid_tiling_y;
//    public float road_edge_tiling_x;
//    public float road_edge_tiling_y;
//    public float dashed_tiling_x;
//    public float dashed_tiling_y;
//    public float double_tiling_x;
//    public float double_tiling_y;
//    public float botts_dots_tiling_x;
//    public float botts_dots_tiling_y;
//    private float hostSpeed;
//    private float hostHeading;
//   // private int frameStall = 0;
//    public string laneType = "Dashed";

//    // Use this for initialization
//    void Start()
//    {
//        setLaneType(laneType.ToUpper());
//    }

//    void setLaneType(string type)
//    {
//        Renderer ren = GetComponent<Renderer>();
//        ren.material.EnableKeyword("DASHED");
//        ren.material.EnableKeyword("ROADEDGE");
//        ren.material.EnableKeyword("SOLID");
//        ren.material.EnableKeyword("DOUBLELANE");
//        ren.material.EnableKeyword("BOTTSDOTS");

//        if (type == "SOLID")
//        {
//            ren.material.mainTexture = solid_lane;
//            ren.material.mainTextureScale = new Vector2(solid_tiling_x, solid_tiling_y);
//        }
//        else if (type == "ROADEDGE")
//        {
//            //ren.material.SetTexture("RoadEdge", road_edge_lane);
//            ren.material.mainTexture = road_edge_lane;
//            ren.material.mainTextureScale = new Vector2(road_edge_tiling_x, road_edge_tiling_y);
//        }
//        else if (type == "DASHED")
//        {
//            ren.material.mainTexture = dashed_lane;
//            ren.material.mainTextureScale = new Vector2(dashed_tiling_x, dashed_tiling_y);
//        }
//        else if (type == "DOUBLELANE")
//        {
//            ren.material.mainTexture = double_lane;
//            ren.material.mainTextureScale = new Vector2(double_tiling_x, double_tiling_y);
//        }
//        else if (type == "BOTTSDOTS")
//        {
//            ren.material.mainTexture = botts_dots_lane;
//            ren.material.mainTextureScale = new Vector2(botts_dots_tiling_x, botts_dots_tiling_y);
//        }
//        else
//        {
//            Debug.LogError("LaneType invalid: please use Solid, RoadEdge, Dashed, DoubleLane, or BottsDots");
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (JsonParser.applicationHasStopped == true) {
//            enabled = false;
//        }
//        //if (frameStall % 5 == 0)
//        //{
//        hostSpeed = JsonParser.zeroSpeed; // obtain host car current speed 
//        hostHeading = JsonParser.hostHead; // optain host car current heading (rotation in y direction) 
//        //hostCar.transform.eulerAngles = new Vector3(hostCar.transform.eulerAngles.x, hostHeading, hostCar.transform.eulerAngles.z); // set heading 
//        //}
//        float offsetX = Time.time * 0;
//        float offsetY = Time.time * hostSpeed; // set host speed to be lane animation speed 
//        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY); // creates animation
//        //frameStall++;
//    }
//}
