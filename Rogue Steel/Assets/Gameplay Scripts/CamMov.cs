using UnityEngine;

public class CamMov : MonoBehaviour
{
    //public bool camFol;
    public Vector3 camPos;
    public GameObject pl;
    // Start is called before the first frame update
    void Start()
    {
        //camFol = false;
        camPos = new Vector3(0,0,0);
        pl = GameObject.Find("Player(Clone)").gameObject.transform.Find("Chassis").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        camPos = new Vector3(pl.transform.position.x, pl.transform.position.y,transform.position.z);
        transform.position = camPos;
        /*
        switch(camFol)
        {
            case true:
                camPos = new Vector2(pl.transform.position.x, pl.transform.position.y);
                transform.position = camPos;
                break;
            case false:
                transform.position = camPos;
                break;
        }
        */
    }
    
}
