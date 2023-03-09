
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{

    public int maxHealth = 4;
    public int currenthealth;
    public int Points = 0;
    public HealthBar healthBar;
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;
    //----------------------------------------------------------------------------------------------
    //Mouse Movement
    //----------------------------------------------------------------------------------------------
    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    private float rollSpeed = 90f, rollAcceleration = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        //Locks Mouse into the Screen
        Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update()
    {
        //Getting Mouse Movement
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;
        
        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);//Allows Shark to Roll

        //Applying mouse movement to ship rotation

        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed *Time.deltaTime, Space.Self);
        
        // Ship movement with WASD
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp( activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp( activeHoverSpeed,Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);

        if (maxHealth == 0)
        {
            Destroy(gameObject);
        }

    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Mine")
        {
            maxHealth--;
            healthBar.SetHealth(maxHealth);
        }

        if (collision.gameObject.tag == "Fish")
        {
            Points++;
            Debug.Log("Score is now" + Points);

        }
    }
}
