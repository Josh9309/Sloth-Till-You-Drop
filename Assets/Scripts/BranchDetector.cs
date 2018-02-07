using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchDetector : MonoBehaviour {

    private Paw pawScript;

	// Use this for initialization
	void Start () {
        pawScript = GetComponentInParent<Paw>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pawScript.OnBranch = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pawScript.OnBranch = false;
    }
}
