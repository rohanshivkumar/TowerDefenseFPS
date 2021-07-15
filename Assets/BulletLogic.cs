using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float timeToLive = 5f;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, timeToLive);
        //play exploding animation


    }


    /*
    And in your Enemy Script, create the function:

    public void TakeDamage(int damageAmount)
        {
            health -= damageAmount;
            // other stuff you want to happen when enemy takes damage
           }
    */

    
    void OnCollisionEnter(Collision collision)
    {
        //if enemy do damage then destroy bullet
        if (collision.transform.tag == "Enemy")
        {
            //collision.gameObject.GetComponent<Enemy>().TakeDamage(5);
            //Debug.Log("enemy hit");
            Destroy(gameObject);
        }
        
        //if wall destroy bullet
        if (collision.transform.tag == "Wall")
        {
            Destroy(gameObject);
        }
            
    }
}