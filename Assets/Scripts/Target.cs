using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private Paw sloth;
    private int levelCount;
    private System.Random rand;
    public GameObject[,] treeGenMatrix;

	// Use this for initialization
	void Start () {
        sloth = FindObjectOfType<Paw>();
        levelCount = sloth.level;
        rand = new System.Random();
        treeGenMatrix = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GenerateTree>().treeMatrix;
        MoveTarget();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called by Paw when click/drag succeeds
    //Adds to level count, moves the target
    void Climb()
    {
        levelCount++;
        MoveTarget();
    }

    void MoveTarget()
    {
        GameObject branch;

        //Makes it so the target doesn't default to one side
        int branchCheck = rand.Next(0, 10);
        if(branchCheck % 2 == 0)
        {
            branch = treeGenMatrix[levelCount + 1, 0];
            if(branch.tag == "Branch")
            {
                this.transform.position = new Vector2(-6.65f, branch.transform.position.y);
            }
        }
        else
        {
            branch = treeGenMatrix[levelCount + 1, 2];
            if (branch.tag == "Branch")
            {
                this.transform.position = new Vector2(6.65f, branch.transform.position.y);
            }
        }
    }
}
