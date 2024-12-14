using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class grenadeExplosion : MonoBehaviour
{
    [SerializeField] LayerMask layers;
    [SerializeField] float radius = 3.0f;
    [SerializeField] float power = 100f;
    [SerializeField] int damage = 3;
    public ParticleSystem explosion;
    public AudioClip exploded;
    [SerializeField] ParticleSystem trail;
    void OnCollisionEnter() {
        //Debug.Log("Kaboom");

        // for rockets
        if (trail != null) {
            trail.Stop();
        }

        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius, layers);

        foreach (Collider hit in colliders) {
            if (hit.gameObject.tag == "Enemy") {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
                //health check here
                hit.GetComponent<enemyHealth>().addDamage(damage);
            }
        }
        explosion.Play();
        GetComponent<AudioSource>().PlayOneShot(exploded);

        Destroy(gameObject, 1);
    }
}
