using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeExplosion : MonoBehaviour
{
    [SerializeField] LayerMask layers;
    [SerializeField] float radius = 5.0f;
    [SerializeField] float power = 10f;
    [SerializeField] int damage = 4;
    public ParticleSystem explosion;
    void OnCollisionEnter(Collision collider) {
        Debug.Log("Kaboom");
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

        Destroy(gameObject);
    }
}
