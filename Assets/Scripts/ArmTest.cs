using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 attractForce = Vector3.MoveTowards(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 10 * Time.deltaTime);
        gameObject.GetComponent<Rigidbody2D>().AddForce(attractForce*1, ForceMode2D.Force);
	}
}
