using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    [SerializeField] Material main;
    [SerializeField] Material damaged;
    private bool isDead = false;
    public void addDamage(int damage) {
        if (!isDead) {
            /*
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

            skin.material = damaged;

            Invoke("resetMat", 0.05f);
            */
            if (gameObject.GetComponent<enemyWarrior>()) {
                health -= damage;

                if (health <= 0) {
                    isDead = true;
                    gameObject.GetComponent<enemyWarrior>().died();
                }

                gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = damaged;

                Invoke("resetSkinMat", 0.1f);
            }
            else if (gameObject.GetComponent<enemyDrone>()) {
                health -= damage;

                if (health <= 0) {
                    isDead = true;
                    gameObject.GetComponent<enemyDrone>().died();
                }

                gameObject.GetComponentInChildren<MeshRenderer>().material = damaged;

                Invoke("resetMeshMat", 0.1f);
            }
        }
    }

    void resetSkinMat() {
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = main;
    }

    void resetMeshMat() {
        gameObject.GetComponentInChildren<MeshRenderer>().material = main;
    }
}