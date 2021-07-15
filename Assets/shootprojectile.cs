using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootprojectile : MonoBehaviour
{
   public GameObject projectile;
   public float launchVelocity = 700f;
   

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("firing");
            GameObject ball = Instantiate(projectile, transform.position, transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity,0));  
        }
    }
}