using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public MainMenu menu;
	public HostData[] hostList;
	private const string typeName = "NeonTwitchVenhip";
	public GameObject playerPrefab;
	RespawnAndDeaths objs;
	/*public string gameName = "RoomName";*/

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}

	void Start(){
		objs = gameObject.GetComponent<RespawnAndDeaths>();
	}

	public void StartServer()
	{
		Network.InitializeServer(menu.multMaxPlayers, menu.multPortHost, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, menu.multRoomName);
		Application.LoadLevel (1);
	}

	void OnServerInitialized()
	{
		StartCoroutine (SpawnPlayer());
	}


	
	public void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	public void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
		Application.LoadLevel (1);
	}
	
	void OnConnectedToServer()
	{
		StartCoroutine (SpawnPlayer());
	}

	public void PlayerSpawn(){
		StartCoroutine (SpawnPlayer());
	}

	public IEnumerator SpawnPlayer()
	{
		Debug.Log ("Spawn");
		yield return new WaitForSeconds(0.5f);
		Transform point = objs.respawnPoints[Random.Range (0,objs.respawnPoints.Length)];
		GameObject playerObj = Network.Instantiate(playerPrefab, point.position, point.rotation, 0) as GameObject;
		playerObj.GetComponent<playerData>().playerName = menu.playerName;
	}


}
