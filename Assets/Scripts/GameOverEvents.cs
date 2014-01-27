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
	public static Talent[] talentTree = new Talent[13];

	// Use this for initialization
	void Start () {
		//Load the saved talents to the tree
		talentTree [0] = new Talent ("Self 1", 10, PlayerPrefs.GetInt("Talent0") == 0 ? false : true, new int[] {});
		talentTree [1] = new Talent ("Self 2", 50, PlayerPrefs.GetInt("Talent1") == 0 ? false : true, new int[] {0});
		talentTree [2] = new Talent ("Self 3", 100, PlayerPrefs.GetInt("Talent2") == 0 ? false : true, new int[] {1});
		talentTree [3] = new Talent ("Self 4", 200, PlayerPrefs.GetInt("Talent3") == 0 ? false : true, new int[] {2});
		talentTree [4] = new Talent ("Group 1", 10, PlayerPrefs.GetInt("Talent4") == 0 ? false : true, new int[] {});
		talentTree [5] = new Talent ("Group 2", 50, PlayerPrefs.GetInt("Talent5") == 0 ? false : true, new int[] {4});
		talentTree [6] = new Talent ("Group 3", 100, PlayerPrefs.GetInt("Talent6") == 0 ? false : true, new int[] {5});
		talentTree [7] = new Talent ("Group 4", 200, PlayerPrefs.GetInt("Talent7") == 0 ? false : true, new int[] {6});
		talentTree [8] = new Talent ("World 1", 10, PlayerPrefs.GetInt("Talent8") == 0 ? false : true, new int[] {});
		talentTree [9] = new Talent ("World 2", 50, PlayerPrefs.GetInt("Talent9") == 0 ? false : true, new int[] {8});
		talentTree [10] = new Talent ("World 3", 100, PlayerPrefs.GetInt("Talent10") == 0 ? false : true, new int[] {9});
		talentTree [11] = new Talent ("World 4", 200, PlayerPrefs.GetInt("Talent11") == 0 ? false : true, new int[] {10});
		talentTree [12] = new Talent ("Talent 5", 500, PlayerPrefs.GetInt("Talent12") == 0 ? false : true, new int[] {3, 7, 11});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//GUI
	void OnGUI(){
		int result = -1;
		
		// Make a background box
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height - 50, 100, 50), "You Are Dead\n Try Again?"))
			Application.LoadLevel ("MainMenu");

		GUI.Box (new Rect(Screen.width - 100, 0, 100, 100), PlayerPrefs.GetFloat("Fame").ToString());

		setButtonColor (0);
		if (GUI.Button (new Rect (Screen.width * 1 / 4 - 50, (Screen.height - 100) * 1/6 - 50, 100, 50), talentTree [0].Name)) {
			result = talentTree [0].ActivateTalent ();
		}
		setButtonColor (1);
		if (GUI.Button (new Rect (Screen.width * 1 / 4 - 50, (Screen.height - 100) * 2/6 - 50, 100, 50), talentTree [1].Name)) {
			result = talentTree [1].ActivateTalent ();
		}
		setButtonColor (2);
		if (GUI.Button (new Rect (Screen.width * 1 / 4 - 50, (Screen.height - 100) * 3/6 - 50, 100, 50), talentTree [2].Name)) {
			result = talentTree [2].ActivateTalent ();
		}
		setButtonColor (3);
		if (GUI.Button (new Rect (Screen.width * 1 / 4 - 50, (Screen.height - 100) * 4/6 - 50, 100, 50), talentTree [3].Name)) {
			result = talentTree [3].ActivateTalent ();
		}
		setButtonColor (4);
		if (GUI.Button (new Rect (Screen.width * 2 / 4 - 50, (Screen.height - 100) * 1/6 - 50, 100, 50), talentTree [4].Name)) {
			result = talentTree [4].ActivateTalent ();
		}
		setButtonColor (5);
		if (GUI.Button (new Rect (Screen.width * 2 / 4 - 50, (Screen.height - 100) * 2/6 - 50, 100, 50), talentTree [5].Name)) {
			result = talentTree [5].ActivateTalent ();
		}
		setButtonColor (6);
		if (GUI.Button (new Rect (Screen.width * 2 / 4 - 50, (Screen.height - 100) * 3/6 - 50, 100, 50), talentTree [6].Name)) {
			result = talentTree [6].ActivateTalent ();
		}
		setButtonColor (7);
		if (GUI.Button (new Rect (Screen.width * 2 / 4 - 50, (Screen.height - 100) * 4/6 - 50, 100, 50), talentTree [7].Name)) {
			result = talentTree [7].ActivateTalent ();
		}
		setButtonColor (8);
		if (GUI.Button (new Rect (Screen.width * 3 / 4 - 50, (Screen.height - 100) * 1/6 - 50, 100, 50), talentTree [8].Name)) {
			result = talentTree [8].ActivateTalent ();
		}
		setButtonColor (9);
		if (GUI.Button (new Rect (Screen.width * 3 / 4 - 50, (Screen.height - 100) * 2/6 - 50, 100, 50), talentTree [9].Name)) {
			result = talentTree [9].ActivateTalent ();
		}
		setButtonColor (10);
		if (GUI.Button (new Rect (Screen.width * 3 / 4 - 50, (Screen.height - 100) * 3/6 - 50, 100, 50), talentTree [10].Name)) {
			result = talentTree [10].ActivateTalent ();
		}
		setButtonColor (11);
		if (GUI.Button (new Rect (Screen.width * 3 / 4 - 50, (Screen.height - 100) * 4/6 - 50, 100, 50), talentTree [11].Name)) {
			result = talentTree [11].ActivateTalent ();
		}
		setButtonColor (12);
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height * 5 / 6 - 50, 100, 50), talentTree [12].Name)) {
			result = talentTree[12].ActivateTalent ();
		}
		
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
		default:
			break;
		}
		
	}
	
	// OnDestroy is called when the object is destroyed
	void OnDestroy(){
		Debug.Log ("Data Saved");
		//save necessary data
		PlayerPrefs.SetInt ("Talent0", talentTree [0].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent1", talentTree [1].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent2", talentTree [2].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent3", talentTree [3].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent4", talentTree [4].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent5", talentTree [5].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent6", talentTree [6].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent7", talentTree [7].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent8", talentTree [8].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent9", talentTree [9].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent10", talentTree [10].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent11", talentTree [11].Active ? 1 : 0);
		PlayerPrefs.SetInt ("Talent12", talentTree [12].Active ? 1 : 0);
		PlayerPrefs.SetFloat ("Fame", 0);
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
			bool onePrereqMet = false;
			foreach (int i in prereqs) {
				if (GameOverEvents.talentTree [i].Active){
					onePrereqMet = true;
					break;
				}
			}
			if (!onePrereqMet)
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
