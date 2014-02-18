using UnityEngine;
using System.Collections;

public class LookAtScript : MonoBehaviour {

	private Vector3 targetDir;

	// Use this for initialization
	void Start () {
		targetDir = GameObject.Find("Main Camera").GetComponent<CameraControl>().followDistance;
		transform.LookAt (transform.position + targetDir, new Vector3(0, 0, -1));
	}
	
	// Update is called once per frame
	void Update () {

	}
}
