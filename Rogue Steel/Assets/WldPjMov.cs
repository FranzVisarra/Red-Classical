using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WldPjMov : MonoBehaviour
{
    public float moveSpeed;
    public Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += -transform.right * moveSpeed * Time.deltaTime;
    }
}
