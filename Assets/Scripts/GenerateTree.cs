using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generate the tree for good 'ol Brüce
public class GenerateTree : MonoBehaviour
{
    private Camera gameCamera; //The camera
    private const byte NUMTRUNKS = 5;
    private const byte NUMBRANCHES = 4;
    [SerializeField] private GameObject[] treeSegmentsPrefab = new GameObject[NUMTRUNKS]; //She got junk in the trunk
    [SerializeField] private GameObject[] leftTreeBranchesPrefab = new GameObject[NUMBRANCHES]; //Don't be afraid to branch out
    [SerializeField] private GameObject[] rightTreeBranchesPrefab = new GameObject[NUMBRANCHES]; //Hella dumb to duplicate like this, but since duplicates in a single array of branches don't come out as unique objects this is what I'm doing

    private const byte TREEMATRIXSIZE = 6; //The bigness of this tree
    public GameObject[,] treeMatrix; //The tree
    private Bounds triggerBounds; //The bounds of the trigger to generate the next tree segments
    int bottomChunk; //The last piece of the tree, used in deletion

    //private Renderer stumpRenderer; //Size of a stump segment, for spacing
    private float stumpSizeY;
    private int spacingValue; //More spacing for the tree from the initial render point

    System.Random rand = new System.Random(); //God dammit Unity random

    //Use this for initialization
    void Start()
    {
        gameCamera = GetComponent<Camera>();
        treeMatrix = new GameObject[TREEMATRIXSIZE, 4];
        //stumpRenderer = treeSegmentsPrefab[0].GetComponent<Renderer>(); //Get the renderer from the stump
        //stumpSizeY = stumpRenderer.bounds.size.y;
        stumpSizeY = treeSegmentsPrefab[0].GetComponent<Renderer>().bounds.size.y;
        spacingValue = TREEMATRIXSIZE - 1;
        bottomChunk = 0;

        //Random numbers for branches
        int leftTrunkRand = 0;
        int leftBranchRand = 0;
        int rightBranchRand = 0;
        int rightTrunkRand = 0;

        //Set up the trees
        for (byte i = 0; i < TREEMATRIXSIZE; i++)
        {
            //Randomly choose what type of branch to spawn
            leftTrunkRand = rand.Next(0, NUMTRUNKS);
            leftBranchRand = rand.Next(0, NUMBRANCHES);
            rightBranchRand = rand.Next(0, NUMBRANCHES);
            rightTrunkRand = rand.Next(0, NUMTRUNKS);

            //Don't let a branch be blank
            if (leftBranchRand == NUMBRANCHES - 1 && rightBranchRand == NUMBRANCHES - 1)
            {
                if (rand.Next(0, 2) == 0)
                    leftBranchRand = rand.Next(0, NUMBRANCHES - 1);
                else
                    rightBranchRand = rand.Next(0, NUMBRANCHES - 1);
            }

            //Instantiate the tree
            treeMatrix[i, 0] = Instantiate(treeSegmentsPrefab[leftTrunkRand], new Vector3(treeSegmentsPrefab[leftTrunkRand].transform.position.x - 9.205f, (i * stumpSizeY) - stumpSizeY, 0), Quaternion.identity); //Left trunk
            treeMatrix[i, 1] = Instantiate(leftTreeBranchesPrefab[leftBranchRand], new Vector3(leftTreeBranchesPrefab[leftBranchRand].transform.position.x, (i * stumpSizeY) - stumpSizeY + (rand.Next(-2, 3) * (float)rand.NextDouble() * .8f), 0), Quaternion.identity); //Left branch
            treeMatrix[i, 2] = Instantiate(rightTreeBranchesPrefab[rightBranchRand], new Vector3(rightTreeBranchesPrefab[rightBranchRand].transform.position.x, (i * stumpSizeY) - stumpSizeY + (rand.Next(-2, 3) * (float)rand.NextDouble() * .8f), 0), Quaternion.Euler(0, 0, 180)); //Right branch
            treeMatrix[i, 3] = Instantiate(treeSegmentsPrefab[rightTrunkRand], new Vector3(treeSegmentsPrefab[rightTrunkRand].transform.position.x + 9.205f, (i * stumpSizeY) - stumpSizeY, 0), Quaternion.identity); //Right trunk
        }
        
        triggerBounds = treeMatrix[0, 0].GetComponent<Renderer>().bounds; //Get the bounds for the trigger
    }

    //Update is called once per frame
    void Update()
    {
        //Destroy the tree when necessary
        if (gameCamera.WorldToViewportPoint(treeMatrix[bottomChunk, 0].transform.position + triggerBounds.extents).y < 0)
        {
            Destroy(treeMatrix[bottomChunk, 0]);
            Destroy(treeMatrix[bottomChunk, 1]);
            Destroy(treeMatrix[bottomChunk, 2]);
            Destroy(treeMatrix[bottomChunk, 3]);

            //Random numbers for branches
            int leftTrunkRand = 0;
            int leftBranchRand = 0;
            int rightBranchRand = 0;
            int rightTrunkRand = 0;

            //Randomly choose what type of branch to spawn
            leftTrunkRand = rand.Next(0, NUMTRUNKS);
            leftBranchRand = rand.Next(0, NUMBRANCHES);
            rightBranchRand = rand.Next(0, NUMBRANCHES);
            rightTrunkRand = rand.Next(0, NUMTRUNKS);

            //Don't let a branch be blank
            if (leftBranchRand == NUMBRANCHES - 1 && rightBranchRand == NUMBRANCHES - 1)
            {
                if (rand.Next(0, 2) == 0)
                    leftBranchRand = rand.Next(0, NUMBRANCHES - 1);
                else
                    rightBranchRand = rand.Next(0, NUMBRANCHES - 1);
            }

            //Instantiate the tree
            treeMatrix[bottomChunk, 0] = Instantiate(treeSegmentsPrefab[leftTrunkRand], new Vector3(treeSegmentsPrefab[leftTrunkRand].transform.position.x - 9.205f, (bottomChunk * stumpSizeY) + (spacingValue * stumpSizeY), 0), Quaternion.identity); //Left trunk
            treeMatrix[bottomChunk, 1] = Instantiate(leftTreeBranchesPrefab[leftBranchRand], new Vector3(leftTreeBranchesPrefab[leftBranchRand].transform.position.x, (bottomChunk * stumpSizeY) + (spacingValue * stumpSizeY) + (rand.Next(-2, 3) * (float)rand.NextDouble() * .8f), 0), Quaternion.identity); //Left branch
            treeMatrix[bottomChunk, 2] = Instantiate(rightTreeBranchesPrefab[rightBranchRand], new Vector3(rightTreeBranchesPrefab[rightBranchRand].transform.position.x, (bottomChunk * stumpSizeY) + (spacingValue * stumpSizeY) + (rand.Next(-2, 3) * (float)rand.NextDouble() * .8f), 0), Quaternion.Euler(0, 0, 180)); //Right branch
            treeMatrix[bottomChunk, 3] = Instantiate(treeSegmentsPrefab[rightTrunkRand], new Vector3(treeSegmentsPrefab[rightTrunkRand].transform.position.x + 9.205f, (bottomChunk * stumpSizeY) + (spacingValue * stumpSizeY), 0), Quaternion.identity); //Right trunk

            bottomChunk++;

            //Last piece was deleted
            if (bottomChunk == TREEMATRIXSIZE)
            {
                bottomChunk = 0; //Reset the position in the matrix
                spacingValue += TREEMATRIXSIZE; //Increment the spacing value
            }
        }

        ////Determine if the object we want is on the screen or not
        ////I'd like to use isVisible but it doesn't want to cooperate
        ////https://answers.unity.com/questions/8003/how-can-i-know-if-a-gameobject-is-seen-by-a-partic.html
        //Plane[] planes = GeometryUtility.CalculateFrustumPlanes(gameCamera);
        //
        ////If the next segment of tree should spawn
        //if (GeometryUtility.TestPlanesAABB(planes, triggerBounds))
        //{
        //    //Random numbers for branches
        //    int leftRand = 0;
        //    int trunkRand = 0;
        //    int rightRand = 0;
        //
        //    //Set up the trees
        //    for (byte i = 0; i < TREEMATRIXSIZE; i++)
        //    {
        //        //Randomly choose what type of branch to spawn
        //        leftRand = rand.Next(0, NUMBRANCHES);
        //        trunkRand = rand.Next(0, 5);
        //        rightRand = rand.Next(0, NUMBRANCHES);
        //
        //        //Don't let a branch be blank
        //        if (leftRand == NUMBRANCHES - 1 && rightRand == NUMBRANCHES - 1)
        //        {
        //            if (rand.Next(0, 2) == 0)
        //                leftRand = rand.Next(0, NUMBRANCHES - 1);
        //            else
        //                rightRand = rand.Next(0, NUMBRANCHES - 1);
        //        }
        //
        //        //Instantiate the tree
        //        treeMatrix[i, 0] = Instantiate(leftTreeBranchesPrefab[leftRand], new Vector3(leftTreeBranchesPrefab[leftRand].transform.position.x, (i * stumpSizeY) + (spacingValue * stumpSizeY) + (rand.Next(-2, 3) * (float)rand.NextDouble() * .8f), 0), Quaternion.Euler(0, 0, 180)); //Left branch
        //        treeMatrix[i, 1] = Instantiate(treeSegmentsPrefab[trunkRand], new Vector3(treeSegmentsPrefab[trunkRand].transform.position.x, (i * stumpSizeY) + (spacingValue * stumpSizeY), 0), Quaternion.identity); //Trunk
        //        treeMatrix[i, 2] = Instantiate(rightTreeBranchesPrefab[rightRand], new Vector3(rightTreeBranchesPrefab[rightRand].transform.position.x, (i * stumpSizeY) + (spacingValue * stumpSizeY) + (rand.Next(-2, 3) * (float)rand.NextDouble() * .8f), 0), Quaternion.identity); //Right branch
        //    }
        //
        //    triggerBounds = treeMatrix[24, 1].GetComponent<Renderer>().bounds; //Get the bounds for the trigger
        //    spacingValue += 25; //Increment the spacing value
        //}
	}
}