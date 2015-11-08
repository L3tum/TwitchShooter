using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public enum gunTypes{
	None,
	PulseRifle,
	LaserRifle,
	Machinegun,
	Shotgun,
	MissileLauncher,
	GrenadeLauncher,
	Pistol
}

public enum fireStyles{
	Automatic,
	Semiautomatic
}

public enum weaponMod{
	Spread,
	Precise,
	Bouncy,
	Fast
}

public class baseGun : MonoBehaviour {

	public GameObject player;
	public baseAmmo ammoType;
	public fireStyles fireStyle;
	//public int maxShotsStored;
	//public int currentStoredAmmo;
	public float timeBetweenShots;
	float defaultTime;
	public float currentTimeBetweenShot;
	public float shotDamage;
	public int numberOfShotsToShoot = 1;
	public float shotVariation = 0;
	public float z = 10f;
	public float shotSpeed;
	public AudioClip[] audioClips;
	public AudioSource audioSource;
	public GameObject[] projectiles;
	GameObject usedProj;
	public Transform firingPoint;
	public playerInventory inv;
	public Animator animator;
	public GameObject weaponAnchor;

	public Vector3 defaultLocation;
	public Vector3 zoomLocation;
	public float zoomFOV;
	public float defaultFOV;
	public Camera cam;
	public Camera weapCam;

	public playerData pD;
	string playerName;
	/*
	public baseAmmo 	Ammo Type;
	public float 		Time Between Shots;
	public float 		Shot Damage;
	public float 		Shot Variation;
	public int 			Number Of Shots To Shoot;
	public float 		Accuracy;
						Zoomed Field Of View;
						Ammo To Restore On Pickup;
						Shot Speed;
		*/				
	//public 


	// Use this for initialization
	void Start () {
		pD = player.GetComponent<playerData>();
		defaultTime = timeBetweenShots;
		cam = Camera.main;
		defaultFOV = cam.fieldOfView;
		defaultLocation = weaponAnchor.transform.localPosition;
		animator = gameObject.GetComponent<Animator>();
		//inv = gameObject.GetComponent<playerInventory>();
		if(ammoType == baseAmmo.LaserBolt){
			usedProj = projectiles[0];
			//shotSpeed = 20;
			shotVariation = 0.1f;
		}
		else if(ammoType == baseAmmo.Laser){
			usedProj = projectiles[1];
			//shotSpeed = 25;
			shotVariation = 0f;
		}
		else if(ammoType == baseAmmo.Bullet){
			usedProj = projectiles[2];
			//shotSpeed = 40;
			shotVariation = 0.12f;
		}
		else if(ammoType == baseAmmo.Pellet){
			usedProj = projectiles[3];
			//shotSpeed = 10;
			shotVariation = 0.2f;
		}
		else if(ammoType == baseAmmo.Missile){
			usedProj = projectiles[4];
			//shotSpeed = 20;
			shotVariation = 0.2f;
		}
		else if(ammoType == baseAmmo.GrenadePill){
			usedProj = projectiles[5];
			//shotSpeed = 10;
			shotVariation = 0.2f;
		}
		else if(ammoType == baseAmmo.Energy){
			usedProj = projectiles[6];
			//shotSpeed = 20;
			shotVariation = 0.2f;
		}
		Cursor.lockState = CursorLockMode.Locked;
	}


	
	// Update is called once per frame
	void Update () {
		currentTimeBetweenShot+= Time.deltaTime;
		if(player.GetComponent<NetworkView>().isMine){
			if(fireStyle == fireStyles.Automatic && CrossPlatformInputManager.GetButton("Fire1")){
			Fire();
			}
			else if(fireStyle == fireStyles.Semiautomatic && CrossPlatformInputManager.GetButtonDown("Fire1")){
				Fire ();
			}
			else{
				if(animator != null){
					animator.SetInteger ("state",0);
				}
			}
			Zoom();
		}



	}

	void Fire(){

		//Debug.Log ("Gets Click");
		if(currentTimeBetweenShot >= timeBetweenShots){
			currentTimeBetweenShot = 0;
			if(ammoType == baseAmmo.Laser){
				//Debug.Log ("Chooses Laser");
				if(inv.laserAmmo > 0){
					//Debug.Log ("Fires Shot");
					PlayAudio (1);
					inv.laserAmmo--;
					ActuallyShoot();
				}else{
					PlayAudio (0);
					//Gun Click
				}
			}
			if(ammoType == baseAmmo.LaserBolt){
				if(inv.laserBoltAmmo > 0){
					PlayAudio (1);
					inv.laserBoltAmmo--;
					ActuallyShoot ();
				}else{
					PlayAudio (0);
					//Gun Click
				}
			}
			if(ammoType == baseAmmo.Bullet){
				if(inv.bulletAmmo > 0){
					PlayAudio (1);
					inv.bulletAmmo--;
					animator.SetInteger ("state",1);
					ActuallyShoot ();
				}else{
					PlayAudio (0);
					//Gun Click
				}
			}
			if(ammoType == baseAmmo.Pellet){
				if(inv.pelletAmmo > 0){
					PlayAudio (1);
					inv.pelletAmmo--;
					ActuallyShoot ();
				}else{
					PlayAudio (0);
					//Gun Click
				}
			}
			if(ammoType == baseAmmo.Missile){
				if(inv.missileAmmo > 0){
					PlayAudio (1);
					inv.missileAmmo--;
					ActuallyShoot ();
					
				}else{
					PlayAudio (0);
					//Gun Click
				}
			}
			if(ammoType == baseAmmo.GrenadePill){
				if(inv.pillAmmo > 0){
					PlayAudio (1);
					inv.pillAmmo--;
					ActuallyShoot ();
					
				}else{
					PlayAudio (0);
					//Gun Click
				}
			}
			if(ammoType == baseAmmo.Energy){
				if(inv.energyAmmo > 0){
					PlayAudio (1);
					inv.energyAmmo--;
					ActuallyShoot ();
					
				}else{
					PlayAudio (0);
					//Gun Click
				}
			}
		}
		

	}

	void FirstShoot(){

	}

	void PlayAudio(int audioNumber){
		audioSource.clip = audioClips[audioNumber];
		audioSource.Play();
	}

	void OnGUI(){
		//GUI.Label (new Rect(10,10,500,300),currentStoredAmmo.ToString ());
	}

	 Vector3 SprayDir(float inaccuracy, Transform trans){
		return Vector3.Slerp (trans.TransformDirection(Vector3.forward), Random.onUnitSphere, inaccuracy);
	}

	void Zoom(){
		if(CrossPlatformInputManager.GetButton("Fire2")){
			weaponAnchor.transform.localPosition = Vector3.MoveTowards (weaponAnchor.transform.localPosition,zoomLocation,1*Time.deltaTime);
			cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, zoomFOV, Time.deltaTime * 5);
			weapCam.fieldOfView = cam.fieldOfView;
		}
		else{
			weaponAnchor.transform.localPosition = Vector3.MoveTowards (weaponAnchor.transform.localPosition, defaultLocation,1*Time.deltaTime);
			cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, defaultFOV, Time.deltaTime * 5);
			weapCam.fieldOfView = cam.fieldOfView;
		}
	}

	void ActuallyShoot(){
		for(int i = 0; i < numberOfShotsToShoot; i++){
			Vector3 direction = Random.insideUnitCircle * shotVariation;
			direction.z = z; // circle is at Z units 
			direction = firingPoint.transform.TransformDirection( direction.normalized );
			GameObject newProj = Instantiate(usedProj, firingPoint.position, firingPoint.transform.rotation) as GameObject;
			newProj.GetComponent<Rigidbody>().AddForce (-direction * shotSpeed * 1);
			ProjectileScript nP = newProj.GetComponent<ProjectileScript>();
			nP.nameOfShooter = pD.playerName;
			nP.damage = shotDamage;

			//StartCoroutine (WaitMeth(timeBetweenShots));
		}
	}

	IEnumerator WaitMeth(float time){
		yield return new WaitForSeconds(time);
	}

	public void AddEffects(float shotDelay, float shotDmg, float shotAccuracy, int numberOfBullets, float zoomFOV, int ammoOnPickup, float shotSpeed){

	}
	/*
	public baseAmmo 	Ammo Type;
	public float 		Time Between Shots;
	public float 		Shot Damage;
	public float 		Shot Variation;
	public int 			Number Of Shots To Shoot;
	public float 		Accuracy;
						Zoomed Field Of View;
						Ammo To Restore On Pickup;
						Shot Speed;
		*/	
}
