using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Determine mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Determine if arm is left or right of mouse
        if(gameObject.transform.position.x < mousePos.x)
            GetComponent<Rigidbody2D>().AddTorque(-20);
        else
            GetComponent<Rigidbody2D>().AddTorque(20);
    }
}
