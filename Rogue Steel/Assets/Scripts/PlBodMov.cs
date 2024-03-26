using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlBodMov : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public Camera cam;
    Vector2 mousePos;
    public Vector2 targPos;
    public Vector2 curPos;
    public Vector2 forPos, forPosPlus, forPosMinus;
    public float Dis;
    public Quaternion targRot;
    public float angle, anglePlus, angleMinus;
    public float dirDeg;
    public GameObject TRight;
    public GameObject TLeft;
    //public GameObject FWS;
    //public GameObject TPS;
    public string movDir="", movRot = "";
    public string moveMode;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.5f;
        rotateSpeed = 50f;
        /*
        FWS = GameObject.Find("SquareForTest");
        FWS = Instantiate(FWS, this.transform);
        TPS = GameObject.Find("SquareForTest");
        TPS = Instantiate(TPS, this.transform);
        */
        moveMode = "";
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.GetType().ToString() + " Update Start");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        curPos.Set(transform.position.x, transform.position.y);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            targPos = mousePos;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveMode = "Rotate Only";
            }
            else
            {
                moveMode = "Quick Move";
            }
        }
        //Debug.Log(this.GetType().ToString() + " Update End");
    }

    void FixedUpdate()
    {
        //Debug.Log(this.GetType().ToString() + " FixedUpdate Start");

        dirDeg = this.transform.eulerAngles.z;
        //make point forward
        Dis = Vector2.Distance(curPos, targPos);
        //set angle ahead of point with reference to direction
        forPos.Set(returnx(0), returny(0));
        angle = Vector2.SignedAngle(forPos-curPos, targPos-curPos);

        switch (moveMode)
        {
            case "Rotate Only":
                if (angle < 0)
                {
                    shortRef("Rotate Right");
                }
                else if (angle > 0)
                {
                    //Debug.Log("Left");
                    shortRef("Rotate Left");
                }
                break;
            case "Quick Move":
                if (angle >= -170 && angle < -90)
                {
                    shortRef("Back Right");
                }
                else if (angle >= -90 && angle < -1)
                {
                    shortRef("Forward Right");
                }
                else if (angle >= -1 && angle <= 1)
                {
                    shortRef("Forward");
                }
                else if (angle > 1 && angle <= 90)
                {
                    shortRef("Forward Left");
                }
                else if (angle > 90 && angle <= 170)
                {
                    shortRef("Back Left");
                }
                else
                {
                    shortRef("Back");
                }
                break;
        }
        //TLeft.GetComponent<TrdL>().Movement(movRot, movDir);
        //TRight.GetComponent<TrdR>().Movement(movRot, movDir);
        //angle = Mathf.Atan2(transform.position.y - targPos.y, transform.position.x - targPos.x) * Mathf.Rad2Deg;
        //Debug.Log(this.GetType().ToString() + " FixedUpdate End");
    }
    public void shortRef(string TrackMov)
    {
        TLeft.GetComponent<TrdL>().Movement(TrackMov);
        TRight.GetComponent<TrdR>().Movement(TrackMov);
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
