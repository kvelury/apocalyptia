using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static int killCount;
	public static bool questAccepted;
	public static bool houseBuilt;
	public static bool questTwoAccepted;

	// Use this for initialization
	void Start () {
		killCount = 0;
		questAccepted = false;
		houseBuilt = false;
		questTwoAccepted = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
