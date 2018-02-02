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
	}
	
	// Update is called once per frame
	void Update () {
        //get current mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ultimateForce = Vector2.zero;

        //get seek force to mouse
        ultimateForce += Arrive(mousePos,3.0f) * mouseWeight;

        //Get force to any leafs
        if(leaves.Count > 0)
            ultimateForce += LeafForce() *leafWeight; 

        //Limit steering force
        ultimateForce = Vector2.ClampMagnitude(ultimateForce, 5.0f);

        //apply acceleration 
        acceleration = acceleration + (ultimateForce / rBody.mass);

        velocity += acceleration * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, pawSpeed);

        rBody.velocity = velocity;
        acceleration = Vector2.zero;

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

    //Move up the tree
    private void Climb()
    {
        //Paw
        //Head
        //Camera

    }
}
