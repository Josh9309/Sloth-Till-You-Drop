using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move the camera for testing purposes
public class MoveCamera : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private GameObject sloth;
    private bool slothHasDied;
    private bool slothHasWon;
    //public float manualSpeed = 10;

    public bool SlothHasDied
    {
        get { return slothHasDied; }
    }

    public bool SlothHasWon
    {
        get { return slothHasWon; }
    }

    //Use this for initialization
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        slothHasDied = false;
        slothHasWon = false;
    }

    void FixedUpdate()
    {
        //cam.transform.position += new Vector3(Input.GetAxis("Horizontal") * manualSpeed, Input.GetAxis("Vertical") * manualSpeed, 0) * Time.deltaTime; //Manual move

        //If the sloth is above the center of the camera
        if (sloth.transform.position.y > cam.transform.position.y)
            transform.position = new Vector3(cam.transform.position.x, sloth.transform.position.y, cam.transform.position.z); //Move the camera up

        //Win
        if (cam.transform.position.y >= 210)
            slothHasWon = true;
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