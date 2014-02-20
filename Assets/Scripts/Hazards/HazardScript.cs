using UnityEngine;
using System.Collections;

/// <summary>
/// A base script for other hazards to inherit from.
/// </summary>
public class HazardScript : MonoBehaviour {

	public bool isAlive = true;

	// Use this for initialization
	public void Start () {
		isAlive = true;
	}
	
	// Update is called once per frame
	public void Update () {
	
	}
}
