using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandling : MonoBehaviour
{
    public int credits;
    public float fuel;
    public float maxFuel;
    public GameObject ResourcesDisplay;
    public Text RDT;
    public Image FuelBar;
    void Awake()
    {
        RDT = ResourcesDisplay.GetComponent<Text>();
        credits = 0;
        fuel = 0;
        //setResourcesDisplay();
    }
    //setting the ui elements
    public void setResourcesDisplay()
    {
        RDT.text = "$"+credits+"\n"+fuel+"/"+maxFuel;
        if (maxFuel != 0)
        {
            FuelBar.rectTransform.anchorMax = new Vector2(fuel / maxFuel, 0.05f);
        }
        else
        {
            FuelBar.rectTransform.anchorMax = new Vector2(0, 0.05f);
        }
    }
    //updating values
    public void setCredits(int credits)
    {
        this.credits = credits;
        setResourcesDisplay();
    }
    public void setFuel(float fuel)
    {
        this.fuel = fuel;
        setResourcesDisplay();
    }
    public void setMaxFuel(float maxFuel)
    {
        this.maxFuel = maxFuel;
        setResourcesDisplay();
    }
}
