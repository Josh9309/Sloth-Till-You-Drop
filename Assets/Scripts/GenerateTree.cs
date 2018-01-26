using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generate the tree for good 'ol Brüce
public class GenerateTree : MonoBehaviour
{
    private Camera gameCamera; //The camera
    [SerializeField] private GameObject[] treeSegmentsPrefab = new GameObject[5]; //She got junk in the trunk
    [SerializeField] private GameObject[] leftTreeBranchesPrefab = new GameObject[4]; //Don't be afraid to branch out
    [SerializeField] private GameObject[] rightTreeBranchesPrefab = new GameObject[4]; //Hella dumb to duplicate like this, but since duplicates in a single array of branches don't come out as unique objects this is what I'm doing

    private const byte TREEMATRIXSIZE = 25;
    private GameObject[,] treeMatrix; //The tree
    private Bounds triggerBounds; //The bounds of the trigger to generate the next tree segments

    private Renderer stumpRenderer; //Size of a stump segment, for spacing
    private float stumpSizeY;
    private int spacingValue; //More spacing for the tree from the initial render point

    System.Random rand = new System.Random(); //God dammit Unity random

    //Use this for initialization
    void Start()
    {
        gameCamera = GetComponent<Camera>();
        treeMatrix = new GameObject[TREEMATRIXSIZE, 3];
        stumpRenderer = treeSegmentsPrefab[0].GetComponent<Renderer>(); //Get the renderer from the stump
        stumpSizeY = stumpRenderer.bounds.size.y;
        spacingValue = 24;

        //Set up the trees
        for (byte i = 0; i < TREEMATRIXSIZE; i++)
        {
            //Create objects
            treeMatrix[i, 0] = leftTreeBranchesPrefab[rand.Next(0, 4)]; //Left branch
            treeMatrix[i, 1] = treeSegmentsPrefab[rand.Next(0, 5)]; //Trunk
            treeMatrix[i, 2] = rightTreeBranchesPrefab[rand.Next(0, 4)]; //Right branch

            //Adjust object transforms
            treeMatrix[i, 0].transform.position = new Vector3(treeMatrix[i, 0].transform.position.x, (i * stumpSizeY) - stumpSizeY + rand.Next(-1, 1), 0); //Left branch
            treeMatrix[i, 1].transform.position = new Vector3(treeMatrix[i, 1].transform.position.x, (i * stumpSizeY) - stumpSizeY, 0); //Trunk
            treeMatrix[i, 2].transform.position = new Vector3(treeMatrix[i, 2].transform.position.x, (i * stumpSizeY) - stumpSizeY + rand.Next(-1, 1), 0); //Right branch

            //Instantiate the tree
            Instantiate(treeMatrix[i, 0]);
            Instantiate(treeMatrix[i, 1]);
            Instantiate(treeMatrix[i, 2]);
        }

        triggerBounds = treeMatrix[23, 1].GetComponent<Renderer>().bounds; //Get the bounds for the trigger
    }

    //Update is called once per frame
    void Update()
    {
        //Determine if the object we want is on the screen or not
        //I'd like to use isVisible but it doesn't want to cooperate
        https://answers.unity.com/questions/8003/how-can-i-know-if-a-gameobject-is-seen-by-a-partic.html
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(gameCamera);

        //If the next segment of tree should spawn
        if (GeometryUtility.TestPlanesAABB(planes, triggerBounds))
        {
            //Rebuild the tree
            for (byte i = 0; i < TREEMATRIXSIZE; i++)
            {
                //Create objects
                treeMatrix[i, 0] = leftTreeBranchesPrefab[rand.Next(0, 4)]; //Left branch
                treeMatrix[i, 1] = treeSegmentsPrefab[rand.Next(0, 5)]; //Trunk
                treeMatrix[i, 2] = rightTreeBranchesPrefab[rand.Next(0, 4)]; //Right branch

                //Adjust object transforms
                treeMatrix[i, 0].transform.position = new Vector3(treeMatrix[i, 0].transform.position.x, (i * stumpSizeY) + (spacingValue * stumpSizeY) + rand.Next(-1, 1), 0); //Left branch
                treeMatrix[i, 1].transform.position = new Vector3(treeMatrix[i, 1].transform.position.x, (i * stumpSizeY) + (spacingValue * stumpSizeY), 0); //Trunk
                treeMatrix[i, 2].transform.position = new Vector3(treeMatrix[i, 2].transform.position.x, (i * stumpSizeY) + (spacingValue * stumpSizeY) + rand.Next(-1, 1), 0); //Right branch

                //Instantiate the tree
                Instantiate(treeMatrix[i, 0]);
                Instantiate(treeMatrix[i, 1]);
                Instantiate(treeMatrix[i, 2]);
            }

            triggerBounds = treeMatrix[23, 1].GetComponent<Renderer>().bounds; //Get the bounds for the trigger
            spacingValue += 25; //Increment the spacing value
        }
	}
}