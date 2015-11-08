using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class playerInventory : MonoBehaviour {
	public int usedAmmo;

	public int laserAmmo;
	public int laserBoltAmmo;
	public int bulletAmmo;
	public int pelletAmmo;
	public int missileAmmo;
	public int pillAmmo;
	public int energyAmmo;

	public int maxLaserAmmo = 25;
	public int maxLaserBoltAmmo = 100;
	public int maxBulletAmmo = 200;
	public int maxPelletAmmo = 50;
	public int maxMissileAmmo = 10;
	public int maxPillAmmo = 25;
	public int maxEnergyAmmo = 100;

	public GameObject firstWeapon;
	public GameObject secondWeapon;
	public bool firstWeaponOut = true;

	public bool isDead;

	public int maxHealth = 100;
	public float currentHealth = 100;
	public Hud hud;
	public playerData pD;
	public RigidbodyFirstPersonController control;
	public PhysicMaterial defaultMat;

	public RespawnAndDeaths deathManager;
	public Transform gunAnchor;
	public GameObject[] weapons;
	// Use this for initialization
	void Start () {
		deathManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnAndDeaths>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Y)){
			Application.LoadLevel (Application.loadedLevel);
		}



		/*if(firstWeapon != null){
			if(firstWeaponOut && !firstWeapon.activeSelf){
				firstWeapon.SetActive (true);
				if(secondWeapon != null)
					secondWeapon.SetActive (false);
			}
		}
		if(secondWeapon != null){
			if(!firstWeaponOut && secondWeapon.activeSelf){
				secondWeapon.SetActive (true);
				if(firstWeapon != null)
					firstWeapon.SetActive (false);
			}
		}*/
		if(CrossPlatformInputManager.GetAxis ("Mouse ScrollWheel") != 0 || Input.GetKeyDown (KeyCode.Tab)){
			//Debug.Log ("Switch Weapon");
			if(firstWeaponOut && secondWeapon != null){
				firstWeaponOut = !firstWeaponOut;
			}
			else if(!firstWeaponOut && firstWeapon != null){
				firstWeaponOut = !firstWeaponOut;
			}
		}

		if(firstWeaponOut){
			if(firstWeapon != null)
			firstWeapon.SetActive (true);
			if(secondWeapon != null)
				secondWeapon.SetActive (false);
		}
		else{
			if(secondWeapon != null)
			secondWeapon.SetActive (true);
			if(firstWeapon != null)
				firstWeapon.SetActive (false);
		}


	}

	public void Death(string killerName){

		if(pD.playerName == killerName){
			deathManager.AddToDeathMessages(pD.playerName + " committed suicide");
		}
		else{
			deathManager.AddToDeathMessages(pD.playerName + " was killed by " + killerName);
		}
		//Debug.Log ("Killed");
		isDead = true;
		StripCharacter ();
		hud.isDead = true;
	}

	public void TakeDamage(float damage, string attackerName){
		if(!isDead){
			currentHealth -= damage;
			//Debug.Log ("Damaged. Health now: " + currentHealth.ToString ());
			if(currentHealth <= 0.5f){
				Death (attackerName);
			}
		}
	}

	void StripCharacter(){
		foreach(GameObject gun in weapons){
			gun.SetActive(false);
			Debug.Log ("Strip:" + gun.name);
		}
		firstWeapon = null;
		secondWeapon = null;
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		rb.freezeRotation = false;
		rb.mass = rb.mass/2;
		rb.drag = 0;
		//rb.AddForce (new Vector3(Random.Range (-20,20),Random.Range (-20,20),Random.Range (-20,20)),ForceMode.Impulse);
		gameObject.GetComponent<Collider>().material = defaultMat;
		control.enabled = false;
	}
}
