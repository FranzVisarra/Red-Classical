using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlBodComponents : MonoBehaviour
{
    // Start is called before the first frame update
    private int length = 5;//<->
    private int width = 3;
    public GameObject cannonTemp;
    public GameObject cannon;
    public GameObject armorTemp;
    public GameObject armor;
    void Start()
    {
        //length = 3;
        //setup
        //cannon
        
        //cannonTemp = GameObject.Find("Cannon");
        cannon = Instantiate(cannonTemp, this.transform);
        cannon.transform.Translate(0, 0, 1);
        cannonTemp.SetActive(false);
        //armor Tile
        //front
        directionalTiles(length,width,0);
        //back
        directionalTiles(length,width,180);
        //left
        directionalTiles(width,length,90);
        //right
        directionalTiles(width,length,270);
        armorTemp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void directionalTiles(int forward, int side, int degrees)
    {
        
        for (int i = 0; i < side; i++)
        {
            //Debug.Log(i);
            //armorTemp = GameObject.Find("Armor");
            armor = Instantiate(armorTemp, this.transform);
            armor.transform.eulerAngles = new Vector3(0,0,degrees);
            armor.transform.Translate(0-(float)forward/2,0 - ((float)side / 2) + 0.5f + i, -1);
        }
    }
}
