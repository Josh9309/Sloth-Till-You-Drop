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
    private UnityEngine.UI.Image slothHead;
    private bool movingCamera;

    public float maxSpeed;
    public float arrivalSpeed;

    public List<GameObject> Leaves { get { return leaves; } set { leaves = value; } }

    //Weights
    [SerializeField] private float mouseWeight = 1.3f;
    [SerializeField] private float leafWeight = .8f;

    // How close a leaf has to be in order for the paw to seek it
    //[SerializeField] private float leafNoticeRadius = 5f;

    //[SerializeField] private LeafSpawner spawner;

	// Use this for initialization
	void Start () {
        leaves = new List<GameObject>();
        rBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        slothHead = FindObjectOfType<UnityEngine.UI.Image>(); //Get the sloth head, there's only ever one.
        movingCamera = false;
    }
	
	// Update is called once per frame
	void Update () {
        //get current mouse position
        if(Input.GetMouseButton(0))
        {
            mouseDownFrameCounter--;
        }
        else
        {
            mouseDownFrameCounter = 120;
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (SphereCollision(target.transform.position) && (mouseDownFrameCounter > 0 && mouseDownFrameCounter < 120))
        {
            ////Stop the paw from moving
            //ultimateForce = Vector2.zero;
            //rBody.velocity = Vector2.zero;
            //acceleration = Vector2.zero;

            target.GetComponent<Target>().MoveTarget(); //Move the paw
            movingCamera = true;
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

        //If the camera should move
        if (movingCamera)
        {
            Climb(); //Move the camera and sloth up
        }
	}

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, leafNoticeRadius);
    }*/

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

    //Move up the tree
    private void Climb()
    {
        if (cam.transform.position.y < target.transform.position.y)
        {
            //Stop the paw from moving
            ultimateForce = Vector2.zero;
            rBody.velocity = Vector2.zero;
            acceleration = Vector2.zero;

            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + .1f, cam.transform.position.z); //Move the camera up
        }
        else
        {
            //Move the paw up to the same level as the sloth head
            transform.position = new Vector3(transform.position.x, cam.ScreenToWorldPoint(new Vector3(transform.position.x, slothHead.transform.position.y, transform.position.z)).y, transform.position.z);
            movingCamera = false;
        }
    }
}
