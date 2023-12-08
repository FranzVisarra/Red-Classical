using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleInfo : MonoBehaviour
{
    public float MaxHp;
    public float CurHp;
    public float MaxPenRes;
    public float CurPenRes;
    public float efficiency;
    // Start is called before the first frame update
    void Start()
    {
        MaxHp = 10;
        CurHp = MaxHp;
        MaxPenRes = 50;
        CurPenRes = MaxPenRes;
    }

    // Update is called once per frame
    public void HitByPro(float Dam)
    {
        Dam = Dam * CurPenRes / 100;
        CurHp -= Dam;
        efficiency = CurHp / MaxHp;
    }
    public float returnDam(float Dam)
    {
        return Dam;
    }
}
