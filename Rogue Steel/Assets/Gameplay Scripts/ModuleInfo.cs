using UnityEngine;
using UnityEngine.UI;

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
    public GameObject Par;
    public GameObject UI;

    public ModSpecInfo infoInterface;
    //public MonoBehaviour info;

    public bool alive;

    public void Awake()
    {
        Par = this.transform.parent.parent.gameObject;
        if (Par.layer == LayerMask.NameToLayer("Player"))
        {
            this.transform.gameObject.layer = LayerMask.NameToLayer("PlayerCollision");
        }
        else if (Par.layer == LayerMask.NameToLayer("Enemy"))
        {
            this.transform.gameObject.layer = LayerMask.NameToLayer("EnemyCollision");
        }
        alive = true;
        //Debug.Log(this.GetType().ToString()+" calling ");
        infoInterface = this.transform.GetComponent<ModSpecInfo>();
        infoInterface.Test();
    }
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
        //Debug.Log("ModuleInfo DamDur Start");
        CurDur -= dam;
        if (MaxDur <=0)
        {
            Debug.LogError("Max Dur is 0. That is bad. "+ gameObject.name+" is supposed to have a max duration");
        }
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
            alive = false;
            //info.Destroyed();
            //this.gameObject.SetActive(false);
            infoInterface.Destroyed();
        }
        CalcAng();
        //Debug.Log("ModuleInfo DamDur End");
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
            alive = false;
            infoInterface.Destroyed();
            //info.Destroyed();
            //this.gameObject.SetActive(false);
        }
        if (UI != null)
        {
            UI.GetComponent<Image>().color = new Vector4(1,1/(1-(MaxHp-CurHp)), 1 / (1 - (MaxHp - CurHp)), 1);
        }
    }
    void CalcAng()
    {
        Ang = 90 / (CurArmor + 1);
    }
    //TODO healing
}
