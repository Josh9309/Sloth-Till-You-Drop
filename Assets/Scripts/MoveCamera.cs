using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move the camera for testing purposes
public class MoveCamera : MonoBehaviour
{
    private Camera cam;
    public float speed = 10;

    //Use this for initialization
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }
	
	//Update is called once per frame
	void Update()
    {
        cam.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0) * Time.deltaTime;
	}
}