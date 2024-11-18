using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    [SerializeField] int damage = 1; //change to is trigger and use that instead
    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Enemy") {
            collider.gameObject.GetComponent<enemyHealth>().addDamage(damage);
        }
        Destroy(gameObject);
    }
}
