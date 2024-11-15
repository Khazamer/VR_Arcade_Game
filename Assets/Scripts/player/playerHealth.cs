using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    [SerializeField] int health = 100;
    public void addDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            //Debug.Log("Game Over");
        }
    }
}
