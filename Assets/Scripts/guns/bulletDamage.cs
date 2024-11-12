using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    [SerializeField] int damage = 1;
    void OnCollisionEnter(Collision collision) {
        collision.gameObject.GetComponent<enemyHealth>().addDamage(damage);
        Destroy(gameObject);
    }
}
