using UnityEngine;
using System.Collections;

public class BasicEnemyScript : MonoBehaviour {
	GameObject player;
	public bool isDead = false;
	public NavMeshAgent agent;
	public float health;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance (player.transform.position, transform.position) < 20 && !isDead){
			//transform.LookAt (player.transform.position);
			//transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
			agent.SetDestination (player.transform.position);
		}
		if(isDead){
			//agent.Stop();
			Destroy (agent);
		}
	}

	void OnCollisionEnter(Collision other){
		//Debug.Log ("hit");
		if(other.gameObject.tag == "Projectile"){
			TakeDamage(other.gameObject);
		}
	}

	void TakeDamage(GameObject other){

		health -= other.GetComponent<ProjectileScript>().damage;

		if(health <= 0){
			Dissasemble ();
			Dissasemble ();
			Dissasemble ();
		}
	}

	void Dissasemble(){
		foreach(Transform child in transform){
			GameObject thisChild = child.gameObject;
			Rigidbody rb = thisChild.AddComponent <Rigidbody>() as Rigidbody;
			BoxCollider bc = thisChild.AddComponent <BoxCollider>() as BoxCollider;
			thisChild.transform.parent = null;
			Vector3 dirVec = transform.position - thisChild.transform.position;
			//rb.AddForce (dirVec*1000);
			isDead = true;
			if(child.tag == "AmmoCrystal"){
				AmmoPickupScript script = thisChild.AddComponent<AmmoPickupScript>();

			}
			else if(child.tag == "AIWeapon"){

			}
			else{
				Destroy (thisChild, Random.Range (20.0f,30.0f));
			}
		}
	}
}
