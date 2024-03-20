using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class EnBodMov : MonoBehaviour
{
    public GameObject TLeft;
    public GameObject TRight;
    public GameObject Enemy;
    public EnTnkStats EScript;
    //mov
    public Vector2 targPos, forPos, curPos;
    public float Dis, angle;
    public string moveMode;

    public float dirDeg;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = this.transform.parent.gameObject;
        EScript = Enemy.GetComponent<EnTnkStats>();
        moveMode = "Quick Move";
    }
    private void Update()
    {
        //Debug.Log(this.GetType().ToString() + " Update Start");
        string AIState = EScript.AiState;
        switch (AIState)
        {
            case "Patrol":
                //targPos = patrol position;
                break;
            case "Investigate":
            case "Attack":
                targPos = EScript.dlist[0].pos;
                break;
        }
        //Debug.Log(this.GetType().ToString() + " Update End");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(this.GetType().ToString() + " FixedUpdate Start");
        curPos.Set(transform.position.x, transform.position.y);
        dirDeg = this.transform.eulerAngles.z;
        //make point forward
        Dis = Vector2.Distance(curPos, targPos);
        //set angle ahead of point with reference to direction
        forPos.Set(returnx(0), returny(0));
        angle = Vector2.SignedAngle(forPos - curPos, targPos - curPos);

        if (moveMode == "Rotate Only")
        {
            if (angle < 0)
            {
                shortRef("Rotate Right");
            }
            else if (angle > 0)
            {
                //Debug.Log("Left");
                shortRef("Rotate Left");
            }
        }
        else if (moveMode == "Quick Move")
        {
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
        }
        //Debug.Log(this.GetType().ToString() + " FixedUpdate End");
    }
    public void shortRef(string TrackMov)
    {
        TLeft.GetComponent<TrdL>().Movement(TrackMov);
        TRight.GetComponent<TrdR>().Movement(TrackMov);
    }
    public float returny(float i)
    {
        return Dis * (Mathf.Sin((dirDeg + 90 + i) * Mathf.Deg2Rad)) + curPos.y;
    }
    public float returnx(float i)
    {
        return Dis * (Mathf.Cos((dirDeg + 90 + i) * Mathf.Deg2Rad)) + curPos.x;
    }
}
