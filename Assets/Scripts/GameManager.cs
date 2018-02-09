using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PAWMODE, LAUNCHMODE};
public class GameManager : MonoBehaviour {
    public Paw leftPaw;
    public Paw rightPaw;
    public Launch slothLaunch;
    public GameState gameState;

	// Use this for initialization
	void Start () {
        leftPaw = GameObject.Find("leftPaw").GetComponent<Paw>();
        rightPaw = GameObject.Find("rightPaw").GetComponent<Paw>();
        slothLaunch = GameObject.Find("sloth bod").GetComponent<Launch>();
        gameState = GameState.PAWMODE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchToLaunch()
    {
        leftPaw.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        rightPaw.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

        slothLaunch.enabled = true;

        gameState = GameState.LAUNCHMODE;
    }

    public void SwitchToPaw()
    {
        leftPaw.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        rightPaw.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        slothLaunch.enabled = false;

        gameState = GameState.PAWMODE;
    }
}
