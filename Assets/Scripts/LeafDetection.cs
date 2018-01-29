using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class attacted to the leaf sensor responsible for modifying the Paw's
/// leaf list
/// </summary>
public class LeafDetection : MonoBehaviour {

    private Paw pawScript;

	// Use this for initialization
	void Start () {
        pawScript = GetComponentInParent<Paw>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// adds a leaf to the Paw script's leaf list
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        pawScript.Leaves.Add(other.gameObject);
    }

    /// <summary>
    /// removes a leaf from the Paw script's leaf list
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (pawScript.Leaves.Contains(other.gameObject))
            pawScript.Leaves.Remove(other.gameObject);
    }
}
