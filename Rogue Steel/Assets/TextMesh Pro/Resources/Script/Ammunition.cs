using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 5f; // higher, it will shooter faster

    public int maxAmmo = 5;
    private int currentAmmo;

    public float reloadTime = 5f;
    private bool isReloading = false;

    private float nextTimeToFire = 2f;


    private void Start()
    {

        currentAmmo = maxAmmo;
    }

    void Update(){

        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire) 
        {
            nextTimeToFire = Time.time + 1f / fireRate; 
            //Shoot();
        }

    }

    void Shoot() {
        currentAmmo--;
    }

    IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
