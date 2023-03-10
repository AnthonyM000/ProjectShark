using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{

    public float speed = 20.0f;
    public float rotationSpeed = 100f;
    public int HP = 3;
    public int Points = 0;


    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }


    // Update is called once per frame
    void Update()
    {
        
        float rotationH = Input.GetAxis("Horizontal") * rotationSpeed;
        //float moveHorizontal = Input.GetAxis("Horizontal");
        float rotationV = Input.GetAxis("Vertical");
        float moveUpDown = 0.0f;
        rotationH *= Time.deltaTime;
        transform.Rotate(0, rotationH, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            moveUpDown = 75.0f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.C))
        {
            moveUpDown = -75f * Time.deltaTime;
        }

        Vector3 movement = new Vector3(0, moveUpDown, rotationV);

        rb.AddRelativeForce(movement * speed);

        if(HP==0)
        {
            Destroy(gameObject);
        }

    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Mine")
        {
            HP--;
        }

        if (collision.gameObject.tag == "Fish")
        {
            Points++;
            Debug.Log("Score is now" + Points);

        }
    }

  

}



