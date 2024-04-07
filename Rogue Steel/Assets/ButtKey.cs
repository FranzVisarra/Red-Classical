using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtKey : MonoBehaviour
{
    public Camera cam;
    public CamMov camMov;
    public GameObject pl;
    public GameObject clicked;
    public GameObject OverImg;
    public List<RaycastHit2D> rc = new List<RaycastHit2D>();

    public ContactFilter2D cf2d;
    void Awake()
    {
        camMov = cam.GetComponent<CamMov>();
    }
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.Find("Player(Clone)").gameObject.transform.Find("Chassis").gameObject;
        OverImg = pl.transform.Find("Overlay").gameObject;
        
    }
    // Update is called once per frame
    void Update()
    {
        //r is for reloading and its on player object
        if (Input.GetKeyDown(KeyCode.F))
        {
            //TODO hold fire
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OverImg.SetActive(!OverImg.activeSelf);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {

        }

        if (Input.GetMouseButtonDown(0))
        {
            Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition),transform.TransformDirection(0,0,1),cf2d,rc,0.01f);
            foreach (RaycastHit2D hit in rc)
            {
                clicked = hit.collider.gameObject;
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }
}
