using UnityEngine;
using System.Collections;

public class WeaponPickupScript : MonoBehaviour {

	public gunTypes gunType;
	gunTypes randomGun;
	public GameObject[] weaponObjs;
	public playerInventory inv;
	public int ammoInGun;

	// Use this for initialization
	void Start () {

		if(gunType == gunTypes.None){
			randomGun = (gunTypes)Random.Range (1,8);
			//Debug.Log (randomGun.ToString ());
		}
		else{
			randomGun = gunType;
		}

		foreach(GameObject weapon in weaponObjs){
			weapon.SetActive (false);
		}

		if(randomGun == gunTypes.LaserRifle){
			weaponObjs[1].SetActive (true);

		}
		else if(randomGun == gunTypes.Machinegun){
			weaponObjs[2].SetActive (true);
		}
		else if(randomGun == gunTypes.PulseRifle){
			weaponObjs[0].SetActive (true);
		}
		else if(randomGun == gunTypes.Shotgun){
			weaponObjs[3].SetActive (true);
		}
		else if(randomGun == gunTypes.MissileLauncher){
			weaponObjs[4].SetActive (true);
		}
		else if(randomGun == gunTypes.GrenadeLauncher){
			weaponObjs[5].SetActive (true);
		}
		else if(randomGun == gunTypes.Pistol){
			weaponObjs[6].SetActive (true);
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		if(other.transform.tag == "Player"){
			inv = other.gameObject.GetComponent<playerInventory>();
			GameObject weaponParent = other.gameObject.transform.GetChild (0).GetChild (0).gameObject;
			if(inv.firstWeapon == null){
				SetFirstWeapon(weaponParent);
			}
			else{
				if(inv.firstWeapon != null && inv.secondWeapon == null){
					SetSecondWeapon(weaponParent);
				}
				else{
					if(inv.firstWeapon != null && inv.secondWeapon != null){
						if(inv.firstWeaponOut){
							SetFirstWeapon(weaponParent);
						}
						else{
							SetSecondWeapon(weaponParent);
						}
					}
				}
			}
		}
	}

	void SetFirstWeapon(GameObject weaponParent){
		if(randomGun == gunTypes.LaserRifle){
			//weaponParent.transform.GetChild (1).gameObject.SetActive (true);
			inv.usedAmmo = 1;
			inv.laserAmmo += ammoInGun;
			if(inv.firstWeapon != null)
				if(inv.firstWeapon.name != "LaserRifle")
					inv.firstWeapon.SetActive (false);
			inv.firstWeapon = weaponParent.transform.GetChild (1).gameObject;
		}
		else if(randomGun == gunTypes.Machinegun){
			//weaponParent.transform.GetChild (2).gameObject.SetActive (true);
			inv.usedAmmo = 2;
			inv.bulletAmmo += ammoInGun;
			if(inv.firstWeapon != null)
				if(inv.firstWeapon.name != "Chaingun")
					inv.firstWeapon.SetActive (false);;
			inv.firstWeapon = weaponParent.transform.GetChild (2).gameObject;
		}
		else if(randomGun == gunTypes.PulseRifle){
			//weaponParent.transform.GetChild (0).gameObject.SetActive (true);
			inv.usedAmmo = 0;
			inv.laserBoltAmmo += ammoInGun;
			if(inv.firstWeapon != null)
				if(inv.firstWeapon.name != "BoltRifle")
					inv.firstWeapon.SetActive (false);
			inv.firstWeapon = weaponParent.transform.GetChild (0).gameObject;
		}
		else if(randomGun == gunTypes.Shotgun){
			//weaponParent.transform.GetChild (3).gameObject.SetActive (true);
			inv.usedAmmo = 3;
			inv.pelletAmmo += ammoInGun;
			if(inv.firstWeapon != null)
				if(inv.firstWeapon.name != "Shotgun")
					inv.firstWeapon.SetActive (false);
			inv.firstWeapon = weaponParent.transform.GetChild (3).gameObject;
		}
		else if(randomGun == gunTypes.MissileLauncher){
			//weaponParent.transform.GetChild (4).gameObject.SetActive (true);
			inv.usedAmmo = 4;
			inv.missileAmmo += ammoInGun;
			if(inv.firstWeapon != null)
				if(inv.firstWeapon.name != "MissileLauncher")
					inv.firstWeapon.SetActive (false);
			inv.firstWeapon = weaponParent.transform.GetChild (4).gameObject;
		}
		else if(randomGun == gunTypes.GrenadeLauncher){
			//weaponParent.transform.GetChild (4).gameObject.SetActive (true);
			inv.usedAmmo = 5;
			inv.pillAmmo += ammoInGun;
			if(inv.firstWeapon != null)
				if(inv.firstWeapon.name != "GrenadeLauncher")
					inv.firstWeapon.SetActive (false);
			inv.firstWeapon = weaponParent.transform.GetChild (5).gameObject;
		}
		else if(randomGun == gunTypes.Pistol){
			//weaponParent.transform.GetChild (4).gameObject.SetActive (true);
			inv.usedAmmo = 6;
			inv.energyAmmo += ammoInGun;
			if(inv.firstWeapon != null)
				if(inv.firstWeapon.name != "Pistol")
					inv.firstWeapon.SetActive (false);
			inv.firstWeapon = weaponParent.transform.GetChild (6).gameObject;
		}
		Destroy (gameObject);
	}

	void SetSecondWeapon(GameObject weaponParent){
		if(randomGun == gunTypes.LaserRifle){
			//weaponParent.transform.GetChild (1).gameObject.SetActive (true);
			inv.usedAmmo = 1;
			inv.laserAmmo += ammoInGun;
			if(inv.secondWeapon != null)
				if(inv.secondWeapon.name != "LaserRifle")
					inv.secondWeapon.SetActive (false);
			inv.secondWeapon = weaponParent.transform.GetChild (1).gameObject;
		}
		else if(randomGun == gunTypes.Machinegun){
			//weaponParent.transform.GetChild (2).gameObject.SetActive (true);
			inv.usedAmmo = 2;
			inv.bulletAmmo += ammoInGun;
			if(inv.secondWeapon != null)
				if(inv.secondWeapon.name != "Chaingun")
					inv.secondWeapon.SetActive (false);
			inv.secondWeapon = weaponParent.transform.GetChild (2).gameObject;
		}
		else if(randomGun == gunTypes.PulseRifle){
			//weaponParent.transform.GetChild (0).gameObject.SetActive (true);
			inv.usedAmmo = 0;
			inv.laserBoltAmmo += ammoInGun;
			if(inv.secondWeapon != null)
				if(inv.secondWeapon.name != "BoltRifle")
					inv.secondWeapon.SetActive (false);
			inv.secondWeapon = weaponParent.transform.GetChild (0).gameObject;
		}
		else if(randomGun == gunTypes.Shotgun){
			//weaponParent.transform.GetChild (3).gameObject.SetActive (true);
			inv.usedAmmo = 3;
			inv.pelletAmmo += ammoInGun;
			if(inv.secondWeapon != null)
				if(inv.secondWeapon.name != "Shotgun")
					inv.secondWeapon.SetActive (false);
			inv.secondWeapon = weaponParent.transform.GetChild (3).gameObject;
		}
		else if(randomGun == gunTypes.MissileLauncher){
			//weaponParent.transform.GetChild (4).gameObject.SetActive (true);
			inv.usedAmmo = 4;
			inv.missileAmmo += ammoInGun;
			if(inv.secondWeapon != null)
				if(inv.secondWeapon.name != "RocketLauncher")
					inv.secondWeapon.SetActive (false);
			inv.secondWeapon = weaponParent.transform.GetChild (4).gameObject;
		}
		else if(randomGun == gunTypes.GrenadeLauncher){
			//weaponParent.transform.GetChild (4).gameObject.SetActive (true);
			inv.usedAmmo = 5;
			inv.pillAmmo += ammoInGun;
			if(inv.secondWeapon != null)
				if(inv.secondWeapon.name != "GrenadeLauncher")
					inv.secondWeapon.SetActive (false);
			inv.secondWeapon = weaponParent.transform.GetChild (5).gameObject;
		}
		else if(randomGun == gunTypes.Pistol){
			//weaponParent.transform.GetChild (4).gameObject.SetActive (true);
			inv.usedAmmo = 6;
			inv.energyAmmo += ammoInGun;
			if(inv.secondWeapon != null)
				if(inv.secondWeapon.name != "Pistol")
					inv.secondWeapon.SetActive (false);
			inv.secondWeapon = weaponParent.transform.GetChild (6).gameObject;
		}
		Destroy (gameObject);
	}
}
