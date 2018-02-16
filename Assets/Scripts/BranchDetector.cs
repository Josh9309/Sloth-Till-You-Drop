using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchDetector : MonoBehaviour {

    private Paw pawScript;
    [SerializeField] private GameObject target;

	// Use this for initialization
	void Start () {
        pawScript = GetComponentInParent<Paw>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Branch")
        {
            //Debug.Log("Entering collision with " + collision.gameObject.name);
            target.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z); //Move the target to the branch
            pawScript.OnBranch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Branch")
        {
            //Debug.Log("Leaving collsion with " + collision.gameObject.name);
            pawScript.OnBranch = false;
        }
    }
}
