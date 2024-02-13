using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{

    public int selectedAmmo = 0;

    private void Start()
    {
        SelectedAmmo();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedAmmo = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedAmmo = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedAmmo = 2;
        }

    }

    void SelectedAmmo()
    {
        int i = 0;
        foreach (Transform ammo in transform)
        {
            if (i == selectedAmmo)
                ammo.gameObject.SetActive(true);
            else
                ammo.gameObject.SetActive(false);
            i++;
        }
    }
}
