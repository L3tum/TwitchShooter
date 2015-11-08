using UnityEngine;
using System.Collections;
using UnityStandardAssets.Effects;

public class ProjectileScript : MonoBehaviour {

	public bool destroyOnContact;
	public bool activateGravityAfterContact;
	public bool destroyAfterFewBounces = true;

	public float damage;
	public bool isExplosive;

	public float explosionRadius;
	public float explosionForce;

	public GameObject explosionPrefab;

	public float explosionDetonationTimer;

	public string nameOfShooter;

	int timesCollided;

	// Use this for initialization
	void Start () {
	Destroy (gameObject, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Rigidbody>().velocity.magnitude < 10 && !destroyOnContact){
			if(isExplosive){
				StartCoroutine (Explode());
			}
			else{
				OtherwiseDestroy ();
			}
		}
	}

	void OnCollisionEnter(Collision other){
		timesCollided++;
		if(activateGravityAfterContact){
			gameObject.GetComponent<Rigidbody>().useGravity = true;
		}
		if(destroyOnContact){

			if(isExplosive){
				StartCoroutine (Explode());
			}
			else{
				OtherwiseDestroy ();
			}
		}

		if(timesCollided >= 2 && destroyAfterFewBounces){
			Destroy (gameObject);
		}


		if(other.gameObject.tag == "Player"){
			playerInventory health = other.gameObject.GetComponent<playerInventory>();
			health.TakeDamage (damage, nameOfShooter);
		}
	}

	IEnumerator Explode(){
		yield return new WaitForSeconds(explosionDetonationTimer);
		 GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		explosion.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
		explosion.GetComponent<ExplosionPhysicsForceAndDamage>().nameOfShooter = nameOfShooter;

		/*Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach(Collider collider in hitColliders){
			Rigidbody rb = collider.GetComponent<Rigidbody>();
			if(rb == null) continue;

			if(collider.tag == "Player"){
				//rb.AddExplosionForce (explosionForce * 10, transform.position, explosionRadius, 1, ForceMode.Impulse);
				Instantiate(explosionPrefab, transform.position, Quaternion.Identity);
			}
			else{
				rb.AddExplosionForce (explosionForce, transform.position, explosionRadius, 1, ForceMode.Impulse);
			}


		}*/
		Destroy (gameObject);
	}

	void OtherwiseDestroy(){
		Destroy (gameObject);
	}


}
