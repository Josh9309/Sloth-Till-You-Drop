using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour {

    private GameManager gameMan;
    private Rigidbody2D rBody;
    private CircleCollider2D collider;

    //steering force
    private Vector2 ultimateForce;
    private Vector2 velocity;
    private Vector2 acceleration;
    public float launchSpeed = 10.0f;
    public float maxSpeed;
    public float arrivalSpeed;
    public float launchWeight = 200.0f;
    public CircleCollider2D mouseCol;
    private bool clickedOn = false; 
	// Use this for initialization
	void Start () {
        gameMan = GameObject.Find("GameManager").GetComponent<GameManager>();
        collider = GetComponent<CircleCollider2D>();
        rBody = GetComponent<Rigidbody2D>();
        mouseCol = GameObject.Find("mouseTracker").GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 intialMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0) && clickedOn)
        {
            ultimateForce = Vector2.zero;

            //get seek force to mouse
            ultimateForce += Arrive(intialMousePos, arrivalSpeed) * launchWeight;


            //Limit steering force
            ultimateForce = Vector2.ClampMagnitude(ultimateForce, maxSpeed);

            //apply acceleration 
            acceleration = acceleration + (ultimateForce / rBody.mass);

            velocity += acceleration * Time.deltaTime;
            velocity = Vector2.ClampMagnitude(velocity, launchSpeed);

            rBody.velocity = velocity;
            acceleration = Vector2.zero;
            //    Vector3 finalMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //    Vector2 launchDirection = finalMousePos - intialMousePos;

            //    Vector2 launchForce = launchDirection * launchDirection.magnitude * launchWeight;

            //    rBody.AddForce(launchForce, ForceMode2D.Impulse);
        }
        else if (clickedOn == true)
        {
            clickedOn = false;
            gameMan.SwitchToPaw();
        }
    }

    private Vector2 Seek(Vector3 target)
    {
        Vector2 seekForce = target - transform.position;
        seekForce = seekForce.normalized * launchSpeed;
        seekForce -= velocity;
        return seekForce;
    }

    private Vector2 Arrive(Vector3 target, float slowRadius)
    {
        Vector2 ArrivalForce = target - transform.position;
        if (ArrivalForce.magnitude > slowRadius)
        {
            //outside arival radius which means no reason to slow down
            ArrivalForce = ArrivalForce.normalized * launchSpeed;
        }
        else
        {
            //inside arrival radius, needs to slow down 
            ArrivalForce = ArrivalForce.normalized * launchSpeed * (ArrivalForce.magnitude / slowRadius);
        }

        ArrivalForce -= velocity;

        return ArrivalForce;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0) && collision.tag == "MouseTracker")
            clickedOn = true;
    }
}
