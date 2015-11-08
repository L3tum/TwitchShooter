using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class baseGunAI : MonoBehaviour {
	
	public baseAmmo ammoType;
	//public int maxShotsStored;
	//public int currentStoredAmmo;
	public float timeBetweenShots;
	public float currentTimeBetweenShot;
	public float shotDamage;
	public int numberOfShotsToShoot = 1;
	public float shotVariation = 0;
	float shotSpeed;
	public AudioClip[] audioClips;
	public AudioSource audioSource;
	public GameObject[] projectiles;
	GameObject usedProj;
	public Transform firingPoint;

	//public 


	// Use this for initialization
	void Start () {
		//inv = gameObject.GetComponent<playerInventory>();
		if(ammoType == baseAmmo.LaserBolt){
			usedProj = projectiles[0];
			shotSpeed = 20;
			shotVariation = 0.1f;
		}
		else if(ammoType == baseAmmo.Laser){
			usedProj = projectiles[1];
			shotSpeed = 25;
			shotVariation = 0f;
		}
		else if(ammoType == baseAmmo.Bullet){
			usedProj = projectiles[2];
			shotSpeed = 40;
			shotVariation = 0.12f;
		}
		else if(ammoType == baseAmmo.Pellet){
			usedProj = projectiles[3];
			shotSpeed = 10;
			shotVariation = 0.2f;
		}
	}


	
	// Update is called once per frame
	void Update () {


	}

	void Fire(){
		currentTimeBetweenShot+= Time.deltaTime;

		if(currentTimeBetweenShot >= timeBetweenShots){
			currentTimeBetweenShot = 0;
			if(ammoType == baseAmmo.Laser){
				PlayAudio (0);
				for(int i = 0; i < numberOfShotsToShoot; i++){
					GameObject newProj = Instantiate(usedProj, firingPoint.position, firingPoint.localRotation) as GameObject;
					newProj.GetComponent<Rigidbody>().AddForce (-SprayDir(shotVariation, firingPoint) * shotSpeed * 100);
					newProj.GetComponent<ProjectileScript>().damage = shotDamage;
				}

			}
			if(ammoType == baseAmmo.LaserBolt){

				PlayAudio (0);

				for(int i = 0; i < numberOfShotsToShoot; i++){
					GameObject newProj = Instantiate(usedProj, firingPoint.position, firingPoint.localRotation) as GameObject;
					newProj.GetComponent<Rigidbody>().AddForce (-SprayDir(shotVariation, firingPoint) * shotSpeed * 100);
					newProj.GetComponent<ProjectileScript>().damage = shotDamage;
				}

			}
			if(ammoType == baseAmmo.Bullet){

				PlayAudio (0);

				for(int i = 0; i < numberOfShotsToShoot; i++){
					
					GameObject newProj = Instantiate(usedProj, firingPoint.position, firingPoint.localRotation) as GameObject;
					newProj.GetComponent<Rigidbody>().AddForce (-SprayDir(shotVariation, firingPoint) * shotSpeed * 100);
					newProj.GetComponent<ProjectileScript>().damage = shotDamage;
				}
			}
			if(ammoType == baseAmmo.Pellet){

				PlayAudio (0);

				for(int i = 0; i < numberOfShotsToShoot; i++){
					GameObject newProj = Instantiate(usedProj, firingPoint.position, Quaternion.Euler (-SprayDir(shotVariation, firingPoint))) as GameObject;
					newProj.GetComponent<Rigidbody>().AddForce (-firingPoint.transform.forward * shotSpeed * 100);
					newProj.GetComponent<ProjectileScript>().damage = shotDamage;
				}
			}
			if(ammoType == baseAmmo.Missile){
				
				PlayAudio (0);
				
				for(int i = 0; i < numberOfShotsToShoot; i++){
					GameObject newProj = Instantiate(usedProj, firingPoint.position, Quaternion.Euler (-SprayDir(shotVariation, firingPoint))) as GameObject;
					newProj.GetComponent<Rigidbody>().AddForce (-firingPoint.transform.forward * shotSpeed * 100);
					newProj.GetComponent<ProjectileScript>().damage = shotDamage;
				}
			}
		}
	}

	void PlayAudio(int audioNumber){
		audioSource.clip = audioClips[audioNumber];
		audioSource.Play();
	}

	 Vector3 SprayDir(float inaccuracy, Transform trans){
		return Vector3.Slerp (trans.TransformDirection(Vector3.forward), Random.onUnitSphere, inaccuracy);
	}
}
