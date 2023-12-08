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
    public float dirDeg;
    public float ModAngDeg;
    // Start is called before the first frame update
    void Update()
    {
        dirDeg = this.transform.eulerAngles.z;
    }

    // Update is called once per frame
    public void HitByPro(float Dam,float Pen, float ProAngDeg)
    {
        //calculate angle of intercept

        Dam = Dam * CurPenRes / 100;
        CurHp -= Dam;
        efficiency = CurHp / MaxHp;
    }
    public void setValues(float MHP, float MPR)
    {
        MaxHp = MHP;
        CurHp = MaxHp;
        MaxPenRes = MPR;
        CurPenRes = MaxPenRes;
    }
    public float returnDam(float Dam)
    {
        return Dam;
    }
}
