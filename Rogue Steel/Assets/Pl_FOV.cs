using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_FOV : MonoBehaviour
{
    public float radius = 5f;
    [Range(1,360)]public float angle = 360f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public GameObject ThingToFollow;
    public bool CanSeeTarg { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        ThingToFollow = GameObject.FindGameObjectWithTag("Enemy");
        StartCoroutine(FOVCheck());
    }
    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FOV();
        }
    }
    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        //in?
        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            CanSeeTarg = true;
            /*
            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);
                if(!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    CanSeeTarg = true;
                }
                else
                {
                    CanSeeTarg = false;
                }
            }
            else
            {
                CanSeeTarg = false;
            }
            */
        }
        /*
        else if (CanSeeTarg)
        {
            CanSeeTarg = false;
        }
        */
    }
}
