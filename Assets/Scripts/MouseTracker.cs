using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //set test cursor to be at mouse cursor
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
	
	// Update is called once per frame
	void Update () {

        //Make cursor follow mouse cursor
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -5);
	}
}
