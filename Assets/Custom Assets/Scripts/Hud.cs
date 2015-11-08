using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Hud : MonoBehaviour {

	playerInventory inv;
	public GUISkin skin;
	string deaths;
	public bool isDead = false;
	public RespawnAndDeaths respawns;
	public RigidbodyFirstPersonController control;
	public NetworkManager net;
	// Use this for initialization
	void Start () {
	 	inv = gameObject.GetComponent<playerInventory>();
	 	respawns = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnAndDeaths>();
		net = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NetworkManager>();
	}
	
	// Update is called once per frame
	void OnGUI () {
		if(!isDead){
			GUI.Label (new Rect(10,10,500,300),"Ammo:", skin.label);
			GUI.Label (new Rect(10,30,500,300),"Laser: " + inv.laserAmmo + "/" + inv.maxLaserAmmo, skin.label);
			GUI.Label (new Rect(10,50,500,300),"Laser Bolt: " + inv.laserBoltAmmo + "/" + inv.maxLaserBoltAmmo, skin.label);
			GUI.Label (new Rect(10,70,500,300),"Bullet: " + inv.bulletAmmo + "/" + inv.maxBulletAmmo, skin.label);
			GUI.Label (new Rect(10,90,500,300),"Pellet: " + inv.pelletAmmo + "/" + inv.maxPelletAmmo, skin.label);
			GUI.Label (new Rect(10,110,500,300),"Missiles: " + inv.missileAmmo + "/" + inv.maxMissileAmmo, skin.label);
			GUI.Label (new Rect(10,130,500,300),"Grenade Pills: " + inv.pillAmmo + "/" + inv.maxPillAmmo, skin.label);
			GUI.Label (new Rect(10,150,500,300),"Energy: " + inv.energyAmmo + "/" + inv.maxEnergyAmmo, skin.label);
			
			GUI.Label (new Rect(10, Screen.height - 30, 300, 30), "Health: " + inv.currentHealth.ToString ("F0") + "/" + inv.maxHealth.ToString ("F0"), skin.label);
		}
		else{
			GUI.Label (new Rect(Screen.width/2 - 150,Screen.height/2 - 60,300,30),"You have died.", skin.customStyles[1]);
			if(GUI.Button (new Rect(Screen.width/2 -75,Screen.height/2,150,30), "Respawn", skin.button)){
				RespawnPlayer();
			}
			Cursor.lockState = CursorLockMode.None;
		}
	}

	void RespawnPlayer(){
		net.PlayerSpawn ();
		Destroy (gameObject);

		/*inv.currentHealth = inv.maxHealth;
		Transform point = respawns.respawnPoints[Random.Range (0,respawns.respawnPoints.Length)];
		inv.laserAmmo = 0;
		inv.laserBoltAmmo = 0;
		inv.bulletAmmo = 0;
		inv.pelletAmmo = 0;
		inv.missileAmmo = 0;
		inv.pillAmmo = 0;
		inv.energyAmmo = 0;

		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.mass = 10;
		rb.drag = 0;
		gameObject.transform.position = point.position;
		gameObject.transform.rotation = point.rotation;
		control.enabled = true;
		isDead = false;*/
	}



}
