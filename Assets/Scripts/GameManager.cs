using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PAWMODE, LAUNCHMODE};
public class GameManager : MonoBehaviour {
    public Paw leftPaw;
    public Paw rightPaw;
    private HingeJoint2D[] tethers;
    public Launch slothLaunch;
    public GameState gameState;

	// Use this for initialization
	void Start () {
        leftPaw = GameObject.Find("leftPaw").GetComponent<Paw>();
        rightPaw = GameObject.Find("rightPaw").GetComponent<Paw>();
        slothLaunch = GameObject.Find("sloth bod").GetComponent<Launch>();

        tethers = new HingeJoint2D[2];
        tethers[0] = GameObject.Find("rightPaw").GetComponents<HingeJoint2D>()[1];
        tethers[1] = GameObject.Find("leftPaw").GetComponents<HingeJoint2D>()[1];

        SwitchToLaunch();

        // lock cursor to screen
        Cursor.lockState = CursorLockMode.Confined;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchToLaunch()
    {
        leftPaw.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        rightPaw.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        tethers[0].enabled = true;
        tethers[1].enabled = true;
        slothLaunch.enabled = true;

        gameState = GameState.LAUNCHMODE;
    }

    public void SwitchToPaw()
    {
        leftPaw.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        rightPaw.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        tethers[0].enabled = false;
        tethers[1].enabled = false;
        slothLaunch.enabled = false;

        gameState = GameState.PAWMODE;
    }
}
