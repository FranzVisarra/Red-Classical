using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    public FloatValue SavePoint;
    public TMP_Text Time, HP, State, Bullet;
    public void ButtonClicked()
    {

        PlayerPrefs.SetFloat("Time", SavePoint.RunTimeValue);
        PlayerPrefs.SetFloat("HP", SavePoint.HP);
        PlayerPrefs.SetString("state", SavePoint.state);
        PlayerPrefs.SetInt("bullet", SavePoint.bullet);

        Time.text = "Time: " + PlayerPrefs.GetFloat("Time");
        HP.text = "HP: " + PlayerPrefs.GetFloat("HP");
        State.text = "State: " + PlayerPrefs.GetString("state");
        Bullet.text = "Bullet: " + PlayerPrefs.GetInt("bullet");
    }
}