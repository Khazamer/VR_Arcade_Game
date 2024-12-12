using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
//using Unity.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class playerHealth : MonoBehaviour
{
    [SerializeField] int startHealth = 100;
    [SerializeField] TMP_Text wordDisplay;
    [SerializeField] TMP_Text healthDisplay;
    [SerializeField] GameObject levels;
    private bool isDead = false;
    //[SerializeField] float intensity = 0.5f;
    //[SerializeField] PostProcessVolume volume;
    [SerializeField] ParticleSystem damageParticles;
    //Vignette _vignette;
    //private float tempIntensity;
    private int health;

    void Start() {
        //vingnette = volume.gameObject.GetComponent<Vignette>();
        //volume.profile.TryGetSettings<Vignette>(out _vignette);

        //_vignette.enabled.Override(false);

        //damageParticles = transform.GetComponentInChildren<ParticleSystem>();

        health = startHealth;
    }

    public void addDamage(int damage) {
        if (!isDead) {
            health -= damage;

            //StartCoroutine(takeDamageEffect());

            damageParticles.Play();

            healthDisplay.SetText("Health left: " + health.ToString());

            if (health <= 0) {
                isDead = true;

                //Debug.Log("Game Over");
                wordDisplay.SetText("You died. Game over");

                //globals.gameOver = true;

                foreach(Transform child in levels.transform) {
                    if (child.gameObject.activeSelf == true) {
                        child.GetComponent<levelManager>().playerDied();
                    }
                }

                Invoke("gameOver", 5f);
            }
        }
    }

    /*
    IEnumerator takeDamageEffect() {
        _vignette.enabled.Override(true);
        _vignette.intensity.Override(intensity);

        yield return new WaitForSeconds(0.1f);

        tempIntensity = intensity;

        while (tempIntensity > 0) {
            tempIntensity -= 0.05f;

            if (tempIntensity < 0) tempIntensity = 0;

            _vignette.intensity.Override(tempIntensity);

            yield return new WaitForSeconds(0.1f);
        }

        _vignette.enabled.Override(false);
        yield break;
    }
    */

    void gameOver() {
        transform.GetComponent<gameManager>().restartLevelSelect();

        healthDisplay.SetText("");

        health = startHealth;
        isDead = false;
    }
}
