using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    public void addDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            if (gameObject.GetComponent<enemyWarrior>()) {
                gameObject.GetComponent<enemyWarrior>().died();
            }
            else if (gameObject.GetComponent<enemyDrone>()) {
                gameObject.GetComponent<enemyDrone>().died();
            }

            gameObject.GetComponent<enemyHealth>().enabled = false;
        }
    }
}