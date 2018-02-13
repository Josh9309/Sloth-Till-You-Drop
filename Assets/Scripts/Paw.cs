using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paw : MonoBehaviour {

    public float pawSpeed = 1.0f;
    private Rigidbody2D rBody;
    private Vector3 mousePos;
    private Vector2 ultimateForce;
    private Vector2 velocity;
    private Vector2 acceleration;
    private List<GameObject> leaves;
    private Camera cam; //Camera for mouse position and climbing
    private int mouseDownFrameCounter = 120;
    private GameObject target;
    private bool movingCamera;
    private bool onBranch = false;
    private bool grabbing = false;

    public float maxSpeed;
    public float arrivalSpeed;

    public List<GameObject> Leaves { get { return leaves; } set { leaves = value; } }

    public bool OnBranch { get { return OnBranch; } set { onBranch = value; } }

    // world tether joint
    [SerializeField] HingeJoint2D tetherJoint;

    //Weights
    [SerializeField] private float mouseWeight = 1.3f;
    [SerializeField] private float leafWeight = .8f;

    private GameManager gameMan;
	// Use this for initialization
	void Start () {
        leaves = new List<GameObject>();
        rBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        movingCamera = false;

        gameMan = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {

        switch(gameMan.gameState)
        {
            case GameState.LAUNCHMODE:
                //LaunchModeUpdate();
                break;

            case GameState.PAWMODE:
                PawModeUpdate();
                break;
        }
        
	}

    private void PawModeUpdate()
    {
        
        mouseDownFrameCounter = 120;


        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0) && onBranch)
        {
            gameMan.SwitchToLaunch();
        }
        else
        {
            ultimateForce = Vector2.zero;

            //get seek force to mouse
            ultimateForce += Arrive(mousePos, arrivalSpeed) * mouseWeight;

            //Get force to any leafs
            if (leaves.Count > 0)
                ultimateForce += LeafForce() * leafWeight;

            //Limit steering force
            ultimateForce = Vector2.ClampMagnitude(ultimateForce, maxSpeed);

            //apply acceleration 
            acceleration = acceleration + (ultimateForce / rBody.mass);

            velocity += acceleration * Time.deltaTime;
            velocity = Vector2.ClampMagnitude(velocity, pawSpeed);

            rBody.velocity = velocity;
            acceleration = Vector2.zero;
        }
    }

    private void LaunchModeUpdate()
    {

    }

    private Vector2 Seek(Vector3 target)
    {
        Vector2 seekForce = target - transform.position;
        seekForce = seekForce.normalized * pawSpeed;
        seekForce -= velocity;
        return seekForce;
    }

    private Vector2 Arrive(Vector3 target, float slowRadius)
    {
        Vector2 ArrivalForce = target - transform.position;

        // normalize the vector and set it to a constant
        ArrivalForce.Normalize();
        ArrivalForce *= 5;

        // for detemining where the arrival force will apply to
        //Gizmos.DrawWireSphere(ArrivalForce, slowRadius);

        if(ArrivalForce.magnitude > slowRadius)
        {
            //outside arival radius which means no reason to slow down
            ArrivalForce = ArrivalForce.normalized * pawSpeed;
        }
        else
        {
            //inside arrival radius, needs to slow down 
            ArrivalForce = ArrivalForce.normalized * pawSpeed * (ArrivalForce.magnitude / slowRadius);
        }

        ArrivalForce -= velocity;

        return ArrivalForce;
    }

    private Vector2 LeafForce()
    {
        //Vector2 leafSeekForce = Vector2.zero;

        /* foreach (GameObject leaf in spawner.leafs)
         {
             if ((leaf.transform.position - transform.position).magnitude > leafNoticeRadius)
             {
                 print((leaf.transform.position - transform.position).magnitude);
                 break;
             }
             leafSeekForce += Seek(leaf.transform.position);
         }*/
        GameObject closest = leaves[0];

        foreach (GameObject leaf in leaves)
        {
            if (Mathf.Abs((leaf.transform.position - transform.position).magnitude)
                < Mathf.Abs((closest.transform.position - transform.position).magnitude))
                closest = leaf;
        }

        return Seek(closest.transform.position);
    }

    private bool SphereCollision(Vector3 targetPos)
    {
        float dx = rBody.position.x - targetPos.x;
        float dy = rBody.position.y - targetPos.y;
        float distance = Mathf.Sqrt((dx * dx) + (dy * dy));

        if (distance < .3)
            return true;
        return false;
    }

}





//