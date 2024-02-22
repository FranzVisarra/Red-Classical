using UnityEngine;

public class ModuleInfo : MonoBehaviour
{
    //hit points (Modules only)
    public int MaxHp;
    public int CurHp;
    //deflect (armor only)
    public int MaxArmor;//max 5
    public int CurArmor;
    public float Ang;//allowed angle of pen
    //durability (Armor only) decreases armor when dur hits 0. like maybe a piece of armor can take 5 hits before durability is decreased by one
    public int MaxDur;
    public int CurDur;
    public float efficiency;
    public float dirDeg;
    public GameObject testSquare;
    public GameObject testSquareF;
    public void Start()
    {
        testSquare = GameObject.Find("SquareForTest");
    }

    public float getDirDeg()
    {
        return this.transform.eulerAngles.z;
    }

    public Vector2 getVector2()
    {
        return new Vector2(transform.position.x,transform.position.y);
    }
    /*
    public void ArmorHitByPro(float Dam,float Pen, Vector2 sp, Vector2 hp)
    {
        //calculate angle of intercept
        GameObject perprep = Instantiate(testSquare, this.transform);
        perprep.transform.Translate(0, -1, 0);
    }
    */
    /*
    public void tsq(Vector2 i)
    {
        GameObject s = Instantiate(testSquare);
        s.transform.position = new Vector2(i.x,i.y);
    }
    */
    public void setCurrentValues(int CHP, int CA, int CD)
    {
        CurHp = CHP;
        CurArmor = CA;
        CurDur = CD;
        CalcAng();
    }
    public void setMaxValues(int MHP, int MA, int MD)
    {
        MaxHp = MHP;
        MaxArmor = MA;
        MaxDur = MD;
    }

    public void DamDur(int dam)
    {
        CurDur -= dam;
        for(int i = CurDur; i < 0; i += MaxDur)
        {
            CurArmor--;
            CurDur = i;
        }
        if (CurArmor < 0)
        {
            CurArmor = 0;
        }
        else if (CurArmor == 0)
        {

            this.gameObject.SetActive(false);
        }
        CalcAng();
    }
    public void DamHp(int dam)
    {
        CurHp -= dam;
        if (CurHp < 0)
        {
            CurHp = 0;
        }
        else if (CurHp == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    void CalcAng()
    {
        Ang = 90 / (CurArmor + 1);
    }
}
