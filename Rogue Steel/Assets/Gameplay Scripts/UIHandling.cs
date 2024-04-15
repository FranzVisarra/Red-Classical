using System;
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

    public GameObject ObjectivesDisplay;
    public Startup str;
    public Text RDT;
    public Text OBJ;
    public Image FuelBar;
    void Awake()
    {
        RDT = ResourcesDisplay.GetComponent<Text>();
        OBJ = ObjectivesDisplay.GetComponent<Text>();
        credits = 0;
        fuel = 0;
        //setResourcesDisplay();
    }
    private void Start()
    {
        SetObjectivesDisplay();
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
    public void SetObjectivesDisplay()
    {
        OBJ.text = "";
        foreach (var objective in str.obj)
        {
            OBJ.text += objective.shortText + objective.progress + "/" + objective.amount+"\n";
        }
    }
    public void UpdateObjectivesDisplay(string keyword)
    {
        OBJ.text = "";
        bool allcomplete = true;
        foreach (var objective in str.obj)
        {

            if (objective.keyword == keyword)
            {
                objective.progress++;
                if (objective.CheckCompletion())
                {
                    OBJ.text += objective.shortText + "X" + "\n";
                }
                else
                {
                    OBJ.text += objective.shortText + objective.progress + "/" + objective.amount+"\n";
                    if (allcomplete)//if true, turn false
                    {
                        allcomplete = false;
                    }
                }
            }
        }
        if (allcomplete)
        {
            str.ToggleWin();
        }
    }
}
