using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    private bool isDead = false;
    public void addDamage(int damage) {
        if (!isDead) {
            health -= damage;

            if (health <= 0) {
                isDead = true;
                
                if (gameObject.GetComponent<enemyWarrior>()) {
                    gameObject.GetComponent<enemyWarrior>().died();
                }
                else if (gameObject.GetComponent<enemyDrone>()) {
                    gameObject.GetComponent<enemyDrone>().died();
                }
            }
        }
    }
}