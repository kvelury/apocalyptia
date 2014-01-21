using UnityEngine;
using System.Collections;

public class LookAtScript : MonoBehaviour {

	/// <summary>
	/// The object the current object should be looking at
	/// </summary>
	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (target, new Vector3(0, 0, -1));
	}
}
