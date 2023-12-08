using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ProColHan : MonoBehaviour
{
    public ProStats stats;
    public int layer;
    public void Awake()
    {
        stats = this.transform.gameObject.GetComponent<ProStats>();
        layer = this.transform.gameObject.layer;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided");
        if (this.transform.gameObject.layer == LayerMask.NameToLayer("PlayerCollision"))
        {
            Debug.Log("Hit");
            other.gameObject.GetComponent<ModuleInfo>().HitByPro(stats.Dam,stats.Pen,stats.angle);
        }
        else if (this.transform.gameObject.layer == LayerMask.NameToLayer("EnemyCollision"))
        {
            Debug.Log("Hit");
            other.gameObject.GetComponent<ModuleInfo>().HitByPro(stats.Dam,stats.Pen,stats.angle);
        }
    }
}
