using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlGunMov : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public Transform transform;
    public Camera cam;
    Vector2 mousePos;
    public Vector2 targPos;
    public Vector2 curPos;
    public Quaternion targRot;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.5f;
        rotateSpeed = 50f;
        /*
        projectile = GameObject.Find("Projectile");
        Instantiate(projectile, this.transform);
        cannon.SetActive(false);
        */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            targPos = mousePos;
        }
        curPos.Set(transform.position.x, transform.position.y);
    }
    void Update()
    {
        angle = Mathf.Atan2(transform.position.y - targPos.y, transform.position.x - targPos.x) * Mathf.Rad2Deg;
        targRot = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targRot, rotateSpeed * Time.deltaTime);
    }
}
