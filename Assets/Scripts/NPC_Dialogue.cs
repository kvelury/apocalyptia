using UnityEngine;
using System.Collections;

public class NPC_Dialogue : MonoBehaviour {
	public string[] answerButtons;
	public string[] Questions;
	bool DisplayDialogue = false;
	bool ActivateQuest = false;

	// Use this for initialization
	void Start () {
		Questions = new string[3];
		answerButtons = new string[3];
		Questions[0] = Questions[1] = Questions[2] = "debug";
		answerButtons [0] = answerButtons [1] = answerButtons [2] = "debug";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (DisplayDialogue) {
			//We have a serious issue with "Magic Numbers" here. It only displays properly on
			//"Maximize on Play". We really need to make all the size values dependant on
			//the screen width.
						GUI.Box (new Rect (Screen.width /2 - 250, Screen.height - 325, 425, 300), "");//675, 575, 400, 400), "");
						GUILayout.BeginArea (new Rect (700, 600, 400, 400));
						if (!ActivateQuest) {

								GUILayout.Label (Questions [0]);
								GUILayout.Label (Questions [1]);
								if (GUILayout.Button (answerButtons [0])) {
										ActivateQuest = true;
										DisplayDialogue = false;
								}
								if (GUILayout.Button (answerButtons [1])) {
										DisplayDialogue = false;
								}
						}
						if (ActivateQuest) {

								GUILayout.Label (Questions [2]);
								if (GUILayout.Button (answerButtons [2])) {
										DisplayDialogue = false;
								}
						}
						GUILayout.EndArea ();
				}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.name == "Player")
						DisplayDialogue = true;

	}

	//void OnTriggerEnter(){
	//	DisplayDialogue = true;
	//}

	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.name == "Player")
		DisplayDialogue = false;
	}
}
