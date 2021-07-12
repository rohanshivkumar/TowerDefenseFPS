using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bullet;
    public ParticleSystem muzzleFlash;

    private Vector3 temp;

    // Update is called once per frame
    void Update()
    {   
    
        if(Input.GetKeyDown(KeyCode.P))
        {
            muzzleFlash.Play();
            //Instantiate(bullet,firePoint.position,firePoint.rotation);

        }
    }
}
