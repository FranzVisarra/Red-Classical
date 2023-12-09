using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlBodMov : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public Transform transform;
    public Camera cam;
    Vector2 mousePos;
    public Vector2 targPos;
    public Vector2 curPos;
    public Vector2 forPos, forPosPlus, forPosMinus;
    public float Dis;
    public Quaternion targRot;
    public float angle, anglePlus, angleMinus;
    public Rigidbody2D rb;
    float accelerationPower = 5f;
    float steeringPower = 5f;
    float steeringAmount, speed, direction;
    public float dirDeg;
    public GameObject TRight;
    public GameObject TLeft;
    public GameObject FWS;
    public GameObject TPS;
    public string movDir="", movRot = "";

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.5f;
        rotateSpeed = 50f;
        rb = this.GetComponent<Rigidbody2D>();
        FWS = GameObject.Find("SquareForTest");
        FWS = Instantiate(FWS, this.transform);
        TPS = GameObject.Find("SquareForTest");
        TPS = Instantiate(TPS, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            targPos = mousePos;
        }
        curPos.Set(this.transform.position.x, this.transform.position.y);
    }

    void FixedUpdate()
    {
        dirDeg = this.transform.eulerAngles.z;
        //test square
        TPS.transform.position = targPos;
        //make point forward
        Dis = Vector2.Distance(curPos, targPos);
        //set angle ahead of point with reference to direction
        forPos.Set(returnx(0), returny(0));
        //test direction plus
        forPosPlus.Set(returnx(1), returny(1));
        //testdirection minus
        forPosMinus.Set(returnx(-1), returny(-1));
        //set forward square
        FWS.transform.position = forPos;
        //angle between player and target
        angle = Vector2.Angle(forPos-curPos, targPos-curPos);
        anglePlus = Vector2.Angle(forPosPlus - curPos, targPos - curPos);
        angleMinus = Vector2.Angle(forPosMinus - curPos, targPos - curPos);
        if (Vector2.Distance(targPos, curPos) > 1)
        {
            movDir = "Forward";
        }
        else
        {
            movDir = "None";
        }
        //check if angle to turn is greater than one
        if (angle >= 1)
        {
            //check which direction to turn
            if (anglePlus > angleMinus)
            {//lean towards angleplus
                //Debug.Log("Right");
                movRot = "Right";
                }
            else if (anglePlus < angleMinus)
            {
                //Debug.Log("Left");
                movRot = "Left";
            }
        }
        TLeft.GetComponent<TrdL>().Movement(movRot, movDir);
        TRight.GetComponent<TrdR>().Movement(movRot, movDir);
        movDir = "";
        movRot = "";
        //angle = Mathf.Atan2(transform.position.y - targPos.y, transform.position.x - targPos.x) * Mathf.Rad2Deg;
    }
    public float returny(float i)
    {
        return Dis * (Mathf.Sin((dirDeg + 90+i) * Mathf.Deg2Rad)) + curPos.y;
    }
    public float returnx(float i)
    {
        return Dis * (Mathf.Cos((dirDeg + 90+i) * Mathf.Deg2Rad)) + curPos.x;
    }
}
