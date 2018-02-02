using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private Paw sloth;
    private int levelCount;
    private bool onBranch;

	// Use this for initialization
	void Start () {
        sloth = FindObjectOfType<Paw>();
        levelCount = sloth.level;
        onBranch = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!onBranch)
        {
            
        }
	}
}
