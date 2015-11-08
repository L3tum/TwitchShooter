using UnityEngine;
using System.Collections;

public class AmmoPickupScript : MonoBehaviour {

	public baseAmmo ammoType;
	public int ammoToRestore;

	// Use this for initialization
	void Start () {

		foreach (Transform child in transform){
			child.gameObject.SetActive (false);
		}

	
		if(ammoType == baseAmmo.Laser){
			transform.GetChild (2).gameObject.SetActive (true);
		}
		if(ammoType == baseAmmo.LaserBolt){
			transform.GetChild (6).gameObject.SetActive (true);
		}
		if(ammoType == baseAmmo.Bullet){
			transform.GetChild (0).gameObject.SetActive (true);
		}
		if(ammoType == baseAmmo.Pellet){
			transform.GetChild (5).gameObject.SetActive (true);
		}
		if(ammoType == baseAmmo.Missile){
			transform.GetChild (3).gameObject.SetActive (true);
		}
		if(ammoType == baseAmmo.GrenadePill){
			transform.GetChild (4).gameObject.SetActive (true);
		}
		if(ammoType == baseAmmo.Energy){
			transform.GetChild (1).gameObject.SetActive (true);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			playerInventory inv = other.gameObject.GetComponent<playerInventory>();
			if(inv.usedAmmo == 0){
				inv.laserAmmo += ammoToRestore;
				Destroy(gameObject);
				if(inv.laserAmmo > inv.maxLaserAmmo){
					inv.laserAmmo = inv.maxLaserAmmo;
				}
			}
			if(inv.usedAmmo == 1){
				inv.laserBoltAmmo += ammoToRestore;
				Destroy(gameObject);
				if(inv.laserBoltAmmo > inv.maxLaserBoltAmmo){
					inv.laserBoltAmmo = inv.maxLaserBoltAmmo;
				}
			}
			if(inv.usedAmmo == 2){
				inv.bulletAmmo += ammoToRestore;
				Destroy(gameObject);
				if(inv.bulletAmmo > inv.maxBulletAmmo){
					inv.bulletAmmo = inv.maxBulletAmmo;
				}
			}
			if(inv.usedAmmo == 3){
				inv.pelletAmmo += ammoToRestore;
				Destroy(gameObject);
				if(inv.pelletAmmo > inv.maxPelletAmmo){
					inv.pelletAmmo = inv.maxPelletAmmo;
				}
			}
			if(inv.usedAmmo == 4){
				inv.missileAmmo += ammoToRestore;
				Destroy(gameObject);
				if(inv.missileAmmo > inv.maxMissileAmmo){
					inv.missileAmmo = inv.maxMissileAmmo;
				}
			}
			if(inv.usedAmmo == 5){
				inv.pillAmmo += ammoToRestore;
				Destroy(gameObject);
				if(inv.pillAmmo > inv.maxPillAmmo){
					inv.pillAmmo = inv.maxPillAmmo;
				}
			}
			if(inv.usedAmmo == 6){
				inv.energyAmmo += ammoToRestore;
				Destroy(gameObject);
				if(inv.energyAmmo > inv.maxEnergyAmmo){
					inv.energyAmmo = inv.maxEnergyAmmo;
				}
			}
		}
	}
}
