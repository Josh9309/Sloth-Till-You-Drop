using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move the camera for testing purposes
public class MoveCamera : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private GameObject sloth;
    //public float manualSpeed = 10;

    //Use this for initialization
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        //cam.transform.position += new Vector3(Input.GetAxis("Horizontal") * manualSpeed, Input.GetAxis("Vertical") * manualSpeed, 0) * Time.deltaTime; //Manual move

        //If the sloth is above the center of the camera
        if (sloth.transform.position.y > cam.transform.position.y)
            transform.position = new Vector3(cam.transform.position.x, sloth.transform.position.y, cam.transform.position.z); //Move the camera up
    }

    //Death
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Head")
        {
            Debug.Log("YOU HAVE FUCKED UP");
        }
    }
}