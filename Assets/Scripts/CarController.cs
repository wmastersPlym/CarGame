using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public float forwardSpeed = 10;
    public float turnSpeed = 150;
    public float reverseSpeed = 4;
    [Range(0, 1)]
    public float drift = 0.9f;
    public float maxGripSpeed = 2.5f;

    Rigidbody2D rb;
    private float sandMultiplyer = 1.2f;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float forwardInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        float driftAmount = 1;

        if(RightVelocity().magnitude > maxGripSpeed)
        {
            driftAmount = drift;
        }

        rb.velocity = ForwardVelocity() + RightVelocity() * driftAmount;
        
        if(forwardInput > 0)
        {
            rb.AddForce(transform.up * forwardSpeed * forwardInput);
        } else if (forwardInput < 0)
        {
            rb.AddForce(transform.up * reverseSpeed * forwardInput);
        }
        rb.mass = 1;

        float tf = Mathf.Lerp(0, turnSpeed, rb.velocity.magnitude * 0.5f);
        rb.angularVelocity = -turnInput * tf;


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Sand")
        {
            rb.AddForce(-rb.velocity * sandMultiplyer);
        }
    }

    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(rb.velocity, transform.up);
    }

    Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(rb.velocity,transform.right);
    }
    
}
