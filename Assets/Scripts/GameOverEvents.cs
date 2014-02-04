using UnityEngine;
using System.Collections;

public class GameOverEvents : MonoBehaviour {

	/// <summary>
	/// This array stores the talent tree.  
	/// There are 4 talents per path, plus one central talent.
	/// 0->1->2->3
	/// 4->5->5->7
	/// 8->9->10->11
	/// 3 || 7 || 11 -> 12
	/// </summary>
	public static Talent[] talentTree = new Talent[20];

	public static int questlinesCompleted = 0;

	// Use this for initialization
	void Start () {
		//Load the saved talents to the tree
		talentTree [0] = new Talent ("Denial", 0, PlayerPrefs.GetInt("Talent0") == 0 ? false : true, new int[] {-1});
		talentTree [1] = new Talent ("Anger", 0, PlayerPrefs.GetInt("Talent1") == 0 ? false : true, new int[] {-1});
		talentTree [2] = new Talent ("Bargaining", 0, PlayerPrefs.GetInt("Talent2") == 0 ? false : true, new int[] {-1});
		talentTree [3] = new Talent ("Depression", 0, PlayerPrefs.GetInt("Talent3") == 0 ? false : true, new int[] {-1});
		talentTree [4] = new Talent ("Acceptance", 0, PlayerPrefs.GetInt("Talent4") == 0 ? false : true, new int[] {-1});
		talentTree [5] = new Talent ("Sword 1", 10, PlayerPrefs.GetInt("Talent5") == 0 ? false : true, new int[] {0});
		talentTree [6] = new Talent ("Sword 2", 50, PlayerPrefs.GetInt("Talent6") == 0 ? false : true, new int[] {1, 5});
		talentTree [7] = new Talent ("Sword 3", 100, PlayerPrefs.GetInt("Talent7") == 0 ? false : true, new int[] {2, 6});
		talentTree [8] = new Talent ("Sword 4", 150, PlayerPrefs.GetInt("Talent8") == 0 ? false : true, new int[] {3, 7});
		talentTree [9] = new Talent ("Sword 5", 200, PlayerPrefs.GetInt("Talent9") == 0 ? false : true, new int[] {4, 8});
		talentTree [10] = new Talent ("Shield 1", 10, PlayerPrefs.GetInt("Talent10") == 0 ? false : true, new int[] {0});
		talentTree [11] = new Talent ("Shield 2", 50, PlayerPrefs.GetInt("Talent11") == 0 ? false : true, new int[] {1, 10});
		talentTree [12] = new Talent ("Shield 3", 100, PlayerPrefs.GetInt("Talent12") == 0 ? false : true, new int[] {2, 11});
		talentTree [13] = new Talent ("Shield 4", 150, PlayerPrefs.GetInt("Talent13") == 0 ? false : true, new int[] {3, 12});
		talentTree [14] = new Talent ("Shield 5", 200, PlayerPrefs.GetInt("Talent14") == 0 ? false : true, new int[] {4, 13});
		talentTree [15] = new Talent ("Spear 1", 10, PlayerPrefs.GetInt("Talent15") == 0 ? false : true, new int[] {0});
		talentTree [16] = new Talent ("Spear 2", 50, PlayerPrefs.GetInt("Talent16") == 0 ? false : true, new int[] {1, 15});
		talentTree [17] = new Talent ("Spear 3", 100, PlayerPrefs.GetInt("Talent17") == 0 ? false : true, new int[] {2, 16});
		talentTree [18] = new Talent ("Spear 4", 150, PlayerPrefs.GetInt("Talent18") == 0 ? false : true, new int[] {3, 17});
		talentTree [19] = new Talent ("Spear 5", 200, PlayerPrefs.GetInt("Talent19") == 0 ? false : true, new int[] {4, 18});

		questlinesCompleted = PlayerPrefs.GetInt("Completion");

		for (int i = 0; i<questlinesCompleted; i++) {
			talentTree[i].Active = true;
			Debug.Log ("Activated Talent " + i.ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
		//some debug controls
		if (Input.GetKeyDown(KeyCode.UpArrow)){
			PlayerPrefs.SetFloat("Fame", PlayerPrefs.GetFloat("Fame") + 10);
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)){
			questlinesCompleted++;
			if (questlinesCompleted > 5) questlinesCompleted = 5;
			for (int i = 0; i<questlinesCompleted; i++) {
				talentTree[i].Active = true;
				Debug.Log ("Activated Talent " + i.ToString());
			}
		}
	}

	//GUI
	void OnGUI(){
		int result = -1;
		
		// Make a background box
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height - 50, 100, 50), "You Are Dead\n Try Again?"))
			Application.LoadLevel ("MainMenu");

		GUI.Box (new Rect(Screen.width - 50, 0, 50, 50), PlayerPrefs.GetFloat("Fame").ToString());

		#region MainTalents
		//draw main talents
		setButtonColor (0);
		if (GUI.Button (new Rect (Screen.width * 1 / 5 - 50, (Screen.height - 100) * 1/6 - 50, 100, 50), talentTree [0].Name)) {
			result = talentTree [0].ActivateTalent ();
		}
		setButtonColor (1);
		if (GUI.Button (new Rect (Screen.width * 1 / 5 - 50, (Screen.height - 100) * 2/6 - 50, 100, 50), talentTree [1].Name)) {
			result = talentTree [1].ActivateTalent ();
		}
		setButtonColor (2);
		if (GUI.Button (new Rect (Screen.width * 1 / 5 - 50, (Screen.height - 100) * 3/6 - 50, 100, 50), talentTree [2].Name)) {
			result = talentTree [2].ActivateTalent ();
		}
		setButtonColor (3);
		if (GUI.Button (new Rect (Screen.width * 1 / 5 - 50, (Screen.height - 100) * 4/6 - 50, 100, 50), talentTree [3].Name)) {
			result = talentTree [3].ActivateTalent ();
		}
		setButtonColor (4);
		if (GUI.Button (new Rect (Screen.width * 1 / 5 - 50, (Screen.height - 100) * 5/6 - 50, 100, 50), talentTree [4].Name)) {
			result = talentTree [4].ActivateTalent ();
		}
		#endregion
		#region SwordTalents
		setButtonColor (5);
		if (GUI.Button (new Rect (Screen.width * 2 / 5 - 50, (Screen.height - 100) * 1/6 - 50, 100, 50), talentTree [5].Name)) {
			result = talentTree [5].ActivateTalent ();
		}
		setButtonColor (6);
		if (GUI.Button (new Rect (Screen.width * 2 / 5 - 50, (Screen.height - 100) * 2/6 - 50, 100, 50), talentTree [6].Name)) {
			result = talentTree [6].ActivateTalent ();
		}
		setButtonColor (7);
		if (GUI.Button (new Rect (Screen.width * 2 / 5 - 50, (Screen.height - 100) * 3/6 - 50, 100, 50), talentTree [7].Name)) {
			result = talentTree [7].ActivateTalent ();
		}
		setButtonColor (8);
		if (GUI.Button (new Rect (Screen.width * 2 / 5 - 50, (Screen.height - 100) * 4/6 - 50, 100, 50), talentTree [8].Name)) {
			result = talentTree [8].ActivateTalent ();
		}
		setButtonColor (9);
		if (GUI.Button (new Rect (Screen.width * 2 / 5 - 50, (Screen.height - 100) * 5/6 - 50, 100, 50), talentTree [9].Name)) {
			result = talentTree [9].ActivateTalent ();
		}
		#endregion
		#region ShieldTalents
		setButtonColor (10);
		if (GUI.Button (new Rect (Screen.width * 3 / 5 - 50, (Screen.height - 100) * 1/6 - 50, 100, 50), talentTree [10].Name)) {
			result = talentTree [10].ActivateTalent ();
		}
		setButtonColor (11);
		if (GUI.Button (new Rect (Screen.width * 3 / 5 - 50, (Screen.height - 100) * 2/6 - 50, 100, 50), talentTree [11].Name)) {
			result = talentTree [11].ActivateTalent ();
		}
		setButtonColor (12);
		if (GUI.Button (new Rect (Screen.width * 3 / 5 - 50, (Screen.height - 100) * 3/6 - 50, 100, 50), talentTree [12].Name)) {
			result = talentTree[12].ActivateTalent ();
		}
		setButtonColor (13);
		if (GUI.Button (new Rect (Screen.width * 3 / 5 - 50, (Screen.height - 100) * 4/6 - 50, 100, 50), talentTree [13].Name)) {
			result = talentTree[13].ActivateTalent ();
		}
		setButtonColor (14);
		if (GUI.Button (new Rect (Screen.width * 3 / 5 - 50, (Screen.height - 100) * 5/6 - 50, 100, 50), talentTree [14].Name)) {
			result = talentTree[14].ActivateTalent ();
		}
		#endregion
		#region SpearTalents
		setButtonColor (15);
		if (GUI.Button (new Rect (Screen.width * 4 / 5 - 50, (Screen.height - 100) * 1/6 - 50, 100, 50), talentTree [15].Name)) {
			result = talentTree[15].ActivateTalent ();
		}
		setButtonColor (16);
		if (GUI.Button (new Rect (Screen.width * 4 / 5 - 50, (Screen.height - 100) * 2/6 - 50, 100, 50), talentTree [16].Name)) {
			result = talentTree[16].ActivateTalent ();
		}
		setButtonColor (17);
		if (GUI.Button (new Rect (Screen.width * 4 / 5 - 50, (Screen.height - 100) * 3/6 - 50, 100, 50), talentTree [17].Name)) {
			result = talentTree[17].ActivateTalent ();
		}
		setButtonColor (18);
		if (GUI.Button (new Rect (Screen.width * 4 / 5 - 50, (Screen.height - 100) * 4/6 - 50, 100, 50), talentTree [18].Name)) {
			result = talentTree[18].ActivateTalent ();
		}
		setButtonColor (19);
		if (GUI.Button (new Rect (Screen.width * 4 / 5 - 50, (Screen.height - 100) * 5/6 - 50, 100, 50), talentTree [19].Name)) {
			result = talentTree[19].ActivateTalent ();
		}
		#endregion
		
		switch (result) {
		case 0:
			Debug.Log("Talent Activated");
			break;
		case 1:
			Debug.Log ("Already Active");
			break;
		case 2:
			Debug.Log ("Prerequisite Needed");
			break;
		case 3:
			Debug.Log ("Fame Stocks are too low, Sire.");
			break;
		case 4:
			Debug.Log ("This talent requires quest completion");
			break;
		default:
			break;
		}
		
	}
	
	// OnDestroy is called when the object is destroyed
	void OnDestroy(){
		//Debug.Log ("Data Saved");
		//save necessary data
		for (int i=0; i<talentTree.Length; i++){
			PlayerPrefs.SetInt("Talent" + i.ToString(), talentTree[i].Active? 1 : 0);
		}
		PlayerPrefs.SetFloat ("Fame", 0);
		PlayerPrefs.SetInt("Completion", questlinesCompleted);
		PlayerPrefs.Save ();
	}

	void setButtonColor(int talentNum){
		if (talentTree [talentNum].Active)
			GUI.color = Color.green;
		else
			GUI.color = Color.white;
	}
}

/// <summary>
/// The Talent class holds the prerequisite, the cost, the gui?, and whether it is active.
/// </summary>
public class Talent{

	/// <summary>
	/// The name of the talent
	/// </summary>
	private string name;
	public string Name{
		get { return name; }
	}

	///<summary>
	/// An int that denotes the prerequisite talent
	/// </summary>
	private int[] prereqs;
//	public int Prerequisite{
//		get { return prereq; }
//	}

	///<summary>
	/// A float that denotes the cost of the talent
	/// </summary>
	private float cost;
	public float Cost{
		get { return cost; }
	}

	///<summary>
	/// A bool that denotes whether the talent is active
	/// </summary>
	private bool active;
	public bool Active {
		get { return active; }
		set { active = value; }
	}

	public Talent(string Name, float Cost, bool Active, int[] Prereqs){
		name = Name;
		prereqs = Prereqs;
		cost = Cost;
		active = Active;
	}
	
	/// <summary>
	/// Returns a <see cref="System.String"/> that represents the current <see cref="Talent"/>.
	/// </summary>
	/// <returns>A <see cref="System.String"/> that represents the current <see cref="Talent"/>.</returns>
	public override string ToString(){
		return name + ": Prereq: " + prereqs.ToString() + "; Cost: " + cost.ToString() + "; Active: " + active.ToString() + "\n";
	}

	/// <summary>
	/// Activate this Talent.
	/// </summary>
	/// <returns>If already active, 1. If prerequisite is not active, returns 2.  If not enough fame, returns 3.</returns>
	public int ActivateTalent(){
		//first, check if the talent is already active.
		if (active)
			return 1;
		//next check if the prerequisite is active
		if (prereqs.Length != 0) {
			bool allPrereqsMet = true;
			foreach (int i in prereqs) {
				if (i == -1){
					return 4;
				}
				if (!GameOverEvents.talentTree [i].Active){
					allPrereqsMet = false;
					break;
				}
			}
			if (!allPrereqsMet)
				return 2;
		}
		//next, check if there is enough fame
		float fame = PlayerPrefs.GetFloat("Fame");
		if (fame < cost){
		    return 3;
	    }
		PlayerPrefs.SetFloat("Fame", fame - cost);
		active = true;
		return 0;
	}
}
