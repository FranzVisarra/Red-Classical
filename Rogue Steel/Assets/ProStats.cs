using UnityEngine;

public class ProStats : MonoBehaviour
{
    //damage to stuff
    public int Dam;
    public int Pen;
    public float Ang;
    //stats
    public float Speed;
    public float Seconds;
    public float LifeTime;
    //type of round
    public string ProType;
    //target of projectile
    public string ProHit;
    public Vector2 startPos;
    public ProMov pm;
    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log(ProType + " Round");
        //startPos = new Vector2(transform.position.x,transform.position.y);
    }
    void FixedUpdate()
    {
        if (Seconds < LifeTime)
        {
            Seconds +=  Time.deltaTime;
        }
        else if(Seconds>=LifeTime)
        {
            Destroy(this.transform.gameObject);
        }
    }
    public void SetPro(string type, int dam, int pen, float ang, float speed, float life)
    {
        ProType = type;
        switch (ProType)
        {
            case "Shell":
                ProHit = "Module,Armor";
                break;
            case "Spall":
                ProHit = "Module,Armor";
                break;
        }
        Dam = dam;
        Pen = pen;
        Ang = ang;
        Speed = speed;
        pm.Speed = Speed;
        LifeTime = life;
        Seconds = 0;
    }
    /*
    public void setStartPos(Vector2 i)
    {
        startPos = i;
    }
    */
}
