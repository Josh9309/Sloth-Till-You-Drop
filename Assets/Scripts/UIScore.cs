using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tally up the player's score as they play the game and display it to them
public class UIScore : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text scoreValue; //The score being displayed
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

    //TODO: write highscores to file
}