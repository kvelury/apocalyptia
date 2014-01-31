using UnityEngine;
using System.Collections;

public class NPC_Dialogue : MonoBehaviour {
	public string[] answerButtons;
	public string[] Questions;
	bool DisplayDialogue = false;
	bool ActivateQuest = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUILayout.BeginArea (new Rect (700, 600, 400, 400));
		if(DisplayDialogue && !ActivateQuest){
			GUILayout.Label (Questions[0]);
			GUILayout.Label (Questions[1]);
			if (GUILayout.Button(answerButtons[0])){
				ActivateQuest = true;
				DisplayDialogue = false;
			}
			if (GUILayout.Button(answerButtons[1])){
				DisplayDialogue = false;
			}
		}
		if(DisplayDialogue && ActivateQuest){
			GUILayout.Label (Questions[2]);
			if(GUILayout.Button(answerButtons[2])){
				DisplayDialogue = false;
			}
		}
		GUILayout.EndArea();
	}

	void OnTriggerEnter(){
		DisplayDialogue = true;
	}

	void OnTriggerExit(){
		DisplayDialogue = false;
	}
}
