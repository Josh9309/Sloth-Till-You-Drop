using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int levelCount;
    private System.Random rand;
    public GameObject[,] treeGenMatrix;

	// Use this for initialization
	void Start () {
        levelCount = 0;
        rand = new System.Random();
        treeGenMatrix = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GenerateTree>().treeMatrix;
        MoveTarget();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveTarget()
    {
        GameObject branch;
        int i = 1;

        while(i > 0)
        {
            //Stupid end of matrix bullshit
            if (levelCount + i >= 24)
            {
                //Reset variables for paw placement
                levelCount = 0;
                i = 0;
            }

            //Makes it so the target doesn't default to one side
            int branchCheck = rand.Next(0, 10);
            if (branchCheck % 2 == 0)
            {
                branch = treeGenMatrix[levelCount + i, 0];
                if (branch.tag == "Branch")
                {
                    transform.position = new Vector2(-6.65f, branch.transform.position.y);
                    break;
                }
                branch = treeGenMatrix[levelCount + i, 2];
                if (branch.tag == "Branch")
                {
                    transform.position = new Vector2(6.65f, branch.transform.position.y);
                    break;
                }
            }
            else
            {
                branch = treeGenMatrix[levelCount + i, 2];
                if (branch.tag == "Branch")
                {
                    transform.position = new Vector2(6.65f, branch.transform.position.y);
                    break;
                }
                branch = treeGenMatrix[levelCount + i, 0];
                if (branch.tag == "Branch")
                {
                    transform.position = new Vector2(-6.65f, branch.transform.position.y);
                    break;
                }
            }
            i++;
        }

        levelCount += i;
    }
}
