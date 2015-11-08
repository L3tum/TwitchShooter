using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ExplosionPhysicsForceAndDamage : MonoBehaviour
    {
        public float explosionForce = 4;
		public float explosionMaxDamage = 120;
		public string nameOfShooter;


        private IEnumerator Start()
        {

            // wait one frame because some explosions instantiate debris which should then
            // be pushed by physics force
            yield return null;

            float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;

            float r = 10*multiplier;
            var cols = Physics.OverlapSphere(transform.position, r);
            var rigidbodies = new List<Rigidbody>();
            foreach (var col in cols)
            {
                if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
                {
                    rigidbodies.Add(col.attachedRigidbody);
                }
            }
            foreach (var rb in rigidbodies)
            {
                if(rb.tag == "Player"){
					rb.AddExplosionForce(explosionForce*multiplier, transform.position, r, 10*multiplier, ForceMode.Impulse);
					playerInventory health = rb.gameObject.GetComponent<playerInventory>();
					//health.TakeDamage();
					//Debug.Log (r.ToString());
					float distance =  Vector3.Distance (transform.position, rb.transform.position);
					float damageMult = ((r-1) - distance) * 10;
					float realDamage = (explosionMaxDamage /100) * damageMult;

					//Debug.Log ("Distance: " + Vector3.Distance (transform.position, rb.transform.position).ToString());
					//Debug.Log ("Damage Mult: " + damageMult.ToString ());
					//Debug.Log ("Real Damage: " + realDamage.ToString ());

					health.TakeDamage (realDamage, nameOfShooter);
					continue;
				}
				rb.AddExplosionForce(explosionForce*multiplier, transform.position, r, 1*multiplier, ForceMode.Impulse);
            }
        }
    }
}
