using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour
{

    public GameObject exp;
    public float expForce, radius;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject _exp = Instantiate(exp,transform.position, transform.rotation);
            Destroy(exp,3);
            Knockback();
            Destroy(gameObject);
        }
    }
    void Knockback() //apply knockback
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,radius);

        foreach(Collider nearyby in colliders)
        {
              Rigidbody rigg = nearyby.GetComponent<Rigidbody>();
            if(rigg != null)
            {
                rigg.velocity = Vector3.zero; //reset the rigidbody's velocity
                rigg.AddExplosionForce(expForce, transform.position, radius);
            }
        }
    }
}