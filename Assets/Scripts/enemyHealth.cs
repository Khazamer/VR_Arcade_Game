using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    public void addDamage(int damage) {
        health -= damage;
    }
    void Update() {
        if (health >= 0) {
            Destroy(gameObject);
        }
    }
}
