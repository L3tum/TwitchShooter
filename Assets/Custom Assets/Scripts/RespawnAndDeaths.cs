using UnityEngine;
using System.Collections;

public class RespawnAndDeaths : MonoBehaviour {

	string deaths;
	public GUISkin skin;
	public Transform[] respawnPoints;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label (new Rect(Screen.width-400, 10,390,600), deaths, skin.customStyles[0]);
	}

	public void AddToDeathMessages(string message){
		deaths += message + "\n";
	}

	public void SearchForRespawnPoints(){
		int i = 0;
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag ("Respawns")){
			respawnPoints.SetValue(obj.transform,i);
			i++;
		}
	}

	void OnLevelWasLoaded(int level){
		if(level == 1){
			SearchForRespawnPoints();
		}
	}
}
