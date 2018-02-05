using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move the camera for testing purposes
public class MoveCamera : MonoBehaviour
{
    private Camera cam;
    public float manualSpeed = 10;
    public float autoSpeed = .05f;

    //Use this for initialization
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }
	
	//Update is called once per frame
	void FixedUpdate()
    {
        //cam.transform.position += new Vector3(Input.GetAxis("Horizontal") * manualSpeed, Input.GetAxis("Vertical") * manualSpeed, 0) * Time.deltaTime; //Manual move
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + autoSpeed, cam.transform.position.z); //Move the camera up
    }
}