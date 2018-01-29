using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Using file input and output

//Tally up the player's score as they play the game and display it to them
public class UIScore : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text scoreValue; //The score being displayed
    private int highscore;
    private Camera gameCamera;

	//Use this for initialization
	void Start()
    {
        gameCamera = FindObjectOfType<Camera>(); //Get the camera, there should only be one in the scene
    }
	
	//Update is called once per frame
	void Update()
    {
        scoreValue.text = ((int)gameCamera.transform.position.y).ToString();  //Calculate the score
	}

    //Read the high score from the file
    void ReadHighScore()
    {
        StreamReader fileReader = new StreamReader("Assets\\Data\\highscore.bin"); //Double slash for file path escape sequence
        string line = null; //Stores the strings being read from the file

        while ((line = fileReader.ReadLine()) != null) //Loop till end of file (which is short in this case)
        {
            highscore = int.Parse(line);
        }

        fileReader.Close(); //Close the StreamReader
    }

    //Write the high score to the file
    void WriteHighScore()
    {
        StreamWriter fileWriter = new StreamWriter("Assets\\Data\\highscore.bin"); //Double slash for file path escape sequence
        fileWriter.Write(scoreValue.text);
        fileWriter.Close(); //Close the StreamWriter
    }
}