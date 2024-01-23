using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnGunMov : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public Vector2 targPos;
    public Vector2 curPos;
    public Quaternion targRot;
    public float angle;
    public GameObject Enemy;
    public EnTnkStats EScript;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.5f;
        rotateSpeed = 50f;
        targPos = new Vector2(0,0);
        Enemy = this.transform.parent.parent.parent.gameObject;
        EScript = Enemy.GetComponent<EnTnkStats>();
    }

    // Update is called once per frame
    void Update()
    {
        angle = Mathf.Atan2(transform.position.y - targPos.y, transform.position.x - targPos.x) * Mathf.Rad2Deg;
        targRot = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targRot, rotateSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if (EScript.DetState=="Sound Heard")
        {
            targPos = EScript.TestNoiseDirection;
        }
    }
}
