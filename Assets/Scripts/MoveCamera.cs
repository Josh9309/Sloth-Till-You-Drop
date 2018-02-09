using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move the camera for testing purposes
public class MoveCamera : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private GameObject sloth;
    private bool slothHasDied;
    //public float manualSpeed = 10;

    public bool SlothHasDied
    {
        get { return slothHasDied; }
    }

    //Use this for initialization
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        slothHasDied = false;
    }

    void FixedUpdate()
    {
        //cam.transform.position += new Vector3(Input.GetAxis("Horizontal") * manualSpeed, Input.GetAxis("Vertical") * manualSpeed, 0) * Time.deltaTime; //Manual move

        //If the sloth is above the center of the camera
        if (sloth.transform.position.y > cam.transform.position.y)
            transform.position = new Vector3(cam.transform.position.x, sloth.transform.position.y, cam.transform.position.z); //Move the camera up
    }

    //Death
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "BranchDetective")
        {
            slothHasDied = true;
        }
    }
}