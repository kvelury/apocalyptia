using UnityEngine;
using System.Collections;

public class OverlayParticleScript : MonoBehaviour {
	
	public ParticleSystem sandstorm;
	public ParticleSystem ash;
	private ParticleSystem currSystem;
	private int difficulty;
	private ApocalypseType currApocalypse;
	
	private Vector3 followDistance;
	
	private bool startNeeded;
	private bool editNeeded;
	
	// Use this for initialization
	void Start () {
		startNeeded = true;
		editNeeded = false;
		followDistance = new Vector3(-7, 3, -6);
	}
	
	// Update is called once per frame
	void Update () {
		if (startNeeded){
			if (currSystem != null){
				DestroyImmediate(currSystem);
			}

			currApocalypse = GameObject.Find("GameController").GetComponent<GameFlowController>().currentApocalypse.GetComponent<PolygonGenerator>().apocalypseType;
			Debug.Log("Current Apocalypse: " + currApocalypse.ToString());
			difficulty = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>().difficulty;
			Quaternion startRotation = Quaternion.LookRotation (new Vector3 (followDistance.x * -1 + 3, followDistance.y * -1 - 6, 0), new Vector3(0, 0, -1));
			
			switch (currApocalypse){
			case ApocalypseType.Desert:
				currSystem = Instantiate(sandstorm, transform.position + followDistance, startRotation) as ParticleSystem;
				editNeeded = true;
				break;
			case ApocalypseType.Volcano:
				currSystem = Instantiate(ash, transform.position + followDistance, startRotation) as ParticleSystem;
				break;
			default:
				currSystem = null;
				break;
			}
			
			if (currSystem != null){
				currSystem.enableEmission = true;
			}
			startNeeded = false;
		} else if (editNeeded){
			currSystem.startSpeed = 2 * difficulty+1;
			Debug.Log("Speed = " + currSystem.startSpeed.ToString());
			editNeeded = false;
		}
	}
	
	void FixedUpdate (){
		if (currSystem != null){
			currSystem.transform.position = transform.position + followDistance;
		}
	}

	public void SetDifficulty(){
		startNeeded = true;
	}
}
