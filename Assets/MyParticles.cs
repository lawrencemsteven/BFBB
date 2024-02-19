using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticles : MonoBehaviour
{
   public GameObject block;
    //public Rigidbody rb;
    //public PhysicMaterial WhipCreamPhys;
   public int width = 10;
   public int height = 4;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    public float velocity = 0.0f;


   void FixedUpdate()
   {
        
        Quaternion randomRotation = Random.rotation;
        
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

               Instantiate(block, this.transform.position, randomRotation);
               
               
        }    
        
        //rb.AddForce(transform.forward * velocity);
   }

     /* private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collision.gameObject.CompareTag("pancake"))
        {
            collider.material.dynamicFriction = WhipCreamPhys.dynamicFriction;
            collider.material.staticFriction = WhipCreamPhys.staticFriction;
        }

        if (collision.gameObject.CompareTag("cream"))
        {
            collider.material.dynamicFriction = WhipCreamPhys.dynamicFriction;
            collider.material.staticFriction = WhipCreamPhys.staticFriction;
        }
    }*/
}

