using UnityEngine;
using System.Collections;


public enum menuLocation{
	None = 0,
	MainMenu = 1,
	SingleplayerMenu = 2,
	MultiplayerMenu = 3,
	MultiplayerSearch = 4,
	MultiplayerCreate = 5,
	Options = 6
}

public class MainMenu : MonoBehaviour {

	public GUISkin skin;

	public menuLocation loc;
	public NetworkManager net;

	public int multMaxPlayers = 4;
	public int multPortHost = 25000;
	public string multRoomName = "Untitled Room";
	public string playerName = "Conscript 001";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		if(loc == menuLocation.MainMenu){
			if(GUI.Button (new Rect(10,Screen.height/2 - 75, 200, 30),"Singleplayer", skin.button)){
				loc = menuLocation.SingleplayerMenu;
			}
			if(GUI.Button (new Rect(10,Screen.height/2 - 35, 200, 30),"Multiplayer", skin.button)){
				loc = menuLocation.MultiplayerMenu;
			}
			if(GUI.Button (new Rect(10,Screen.height/2 + 5, 200, 30),"Options", skin.button)){
				loc = menuLocation.Options;
			}
			if(GUI.Button (new Rect(10,Screen.height/2 + 45, 200, 30),"Quit", skin.button)){
				Application.Quit ();
			}
		}
		if(loc == menuLocation.SingleplayerMenu){
			if(GUI.Button (new Rect(10,Screen.height/2 - 75, 200, 30),"Back", skin.button)){
				loc = menuLocation.MainMenu;
			}
		}
		if(loc == menuLocation.MultiplayerMenu){
			if(GUI.Button (new Rect(10,Screen.height/2 - 75, 200, 30),"Back", skin.button)){
				loc = menuLocation.MainMenu;
			}

			GUI.BeginGroup (new Rect(Screen.width/2-400, Screen.height/2-300, 800, 600),"",skin.box);
				
			GUI.Label (new Rect(10,10, 200,30),"Multiplayer",skin.label);
			//GUI.Label (new Rect(10,90, 300,30),"Max Players:",skin.label);
			playerName = GUI.TextField (new Rect(400-205, 300-50,410,30),playerName,skin.textField);
			if(GUI.Button (new Rect(400-205,300-15,200,30),"Search Servers",skin.button)){
				loc = menuLocation.MultiplayerSearch;
			}
			if(GUI.Button (new Rect(405,300-15,200,30),"Create Server",skin.button)){
				loc = menuLocation.MultiplayerCreate;
			}
			
			GUI.EndGroup ();
		}
		if(loc == menuLocation.MultiplayerCreate){
			if(GUI.Button (new Rect(10,Screen.height/2 - 75, 200, 30),"Back", skin.button)){
				loc = menuLocation.MainMenu;
			}
			
			GUI.BeginGroup (new Rect(Screen.width/2-400, Screen.height/2-300, 800, 600),"",skin.box);
			
			GUI.Label (new Rect(10,10, 200,30),"Multiplayer",skin.label);
			GUI.Label (new Rect(10,90, 300,30),"Max Players:",skin.label);
			GUI.Label (new Rect(10,130, 300,30),"Host Port:",skin.label);
			GUI.Label (new Rect(10,50, 300,30),"Server Name:",skin.label);
			multRoomName = GUI.TextField(new Rect(175, 50, 300, 30),multRoomName,skin.textField);
			multMaxPlayers = int.Parse (GUI.TextField(new Rect(175, 90, 100, 30),multMaxPlayers.ToString (),skin.textField));
			multPortHost = int.Parse (GUI.TextField(new Rect(175, 130, 100, 30),multPortHost.ToString (),skin.textField));
			
			if (!Network.isClient && !Network.isServer)
			{
				if (GUI.Button(new Rect(10, 560, 200, 30), "Start Server",skin.button)){
					net.StartServer();
				}
			}
			
			GUI.EndGroup ();
		}
		if(loc == menuLocation.MultiplayerSearch){
			if(GUI.Button (new Rect(10,Screen.height/2 - 75, 200, 30),"Back", skin.button)){
				loc = menuLocation.MainMenu;
			}
			
			GUI.BeginGroup (new Rect(Screen.width/2-400, Screen.height/2-300, 800, 600),"",skin.box);
			
			GUI.Label (new Rect(10,10, 200,30),"Multiplayer",skin.label);
			if(GUI.Button(new Rect(340,560,150,30),"Refresh",skin.button)){
				net.RefreshHostList();
			}
			if(net.hostList != null){
				for(int i =0; i < net.hostList.Length; i++){
					if(GUI.Button(new Rect(500,0,300,30),net.hostList[i].gameName + " - " + net.hostList[i].connectedPlayers.ToString () + "/" + net.hostList[i].playerLimit,skin.button)){
						net.JoinServer (net.hostList[i]);
					}
				}
			}
			else{
				net.RefreshHostList();
			}
			
			GUI.EndGroup ();
		}
		if(loc == menuLocation.Options){
			if(GUI.Button (new Rect(10,Screen.height/2 - 75, 200, 30),"Back", skin.button)){
				loc = menuLocation.MainMenu;
			}

		}
		
	}
}
