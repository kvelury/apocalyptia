using UnityEngine;
using System.Collections;

public class NPC_Dialogue : MonoBehaviour {
	public string[] answerButtons;
	public string[] Questions;
	bool DisplayDialogue = false;
	bool ActivateQuest = false;
	bool NextQuest = false;

	// Use this for initialization
	void Start () {
		Questions = new string[6];
		answerButtons = new string[6];
		Questions [0] = "Why hello there! I'm an NPC";
		Questions [1] = "Would you fend of 5 of these beasts for me?";
		Questions [2] = "Why thank you!";
		Questions [3] = "Hey, I know this might be a bit much to ask";
		Questions [4] = "But would you be willing to build a home for me?";
		Questions [5] = "Thank you so, so much, I might just make it through this after all.";
		answerButtons [0] = "Sure Thing!";
		answerButtons [1] = "Nope, not interested.";
		answerButtons [2] = "You're welcome =D";
		answerButtons [3] = "Nobody should have to survive out here alone, sure thing";
		answerButtons [4] = "Sorry, every man for himself, I'm sure you'll be fine";
		answerButtons [5] = "Of course, I was happy too";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	// this.gameObject.transform.localPosition
	void OnGUI(){
		if (DisplayDialogue) {
				GUI.Box (new Rect (Screen.width /2 - 250, Screen.width /2 - 250, 425, 300), "");//675, 575, 400, 400), "");
				GUILayout.BeginArea (new Rect (Screen.width /2 - 235, Screen.width /2 - 235, 400, 400));
						if (!ActivateQuest) {

								GUILayout.Label (Questions [0]);
								GUILayout.Label (Questions [1]);
								if (GUILayout.Button (answerButtons [0])) {
										ActivateQuest = true;
										GameController.questAccepted = true;
										DisplayDialogue = false;
								}
								if (GUILayout.Button (answerButtons [1])) {
										DisplayDialogue = false;
								}
						}
						if (ActivateQuest && (GameController.killCount > 4) && !NextQuest) {

								GUILayout.Label (Questions [2]);
								if (GUILayout.Button (answerButtons [2])) {
										DisplayDialogue = false;
										NextQuest = true;
								}
						}
						if (NextQuest && (GameController.killCount > 4) && !GameController.houseBuilt && !GameController.questTwoAccepted){
								GUILayout.Label (Questions [3]);
								GUILayout.Label (Questions [4]);
								if (GUILayout.Button (answerButtons [3])) {
									GameController.questTwoAccepted = true;
									DisplayDialogue = false;
								}
								if (GUILayout.Button (answerButtons [4])) {
									DisplayDialogue = false;
								}
						}
						if (GameController.questTwoAccepted && GameController.houseBuilt) {
							
							GUILayout.Label (Questions [5]);
							if (GUILayout.Button (answerButtons [5])) {
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
