using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PlayerShoot : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] GameObject pistolBulletTemplate;
    [SerializeField] GameObject SMGBulletTemplate;
    [SerializeField] GameObject shotgunBulletTemplate;
    [SerializeField] GameObject grenadeLauncherBulletTemplate;
    [SerializeField] float shootPower = 100f;
    private float currTimer = 0f;
    private bool resetTrigger = true;
    private float pistolTimer = 0.25f;
    private float SMGTimer = 0.05f;
    private float shotgunTimer = 0.75f;
    private float grenadeLauncherTimer = 1.5f;
    private Transform gunTransform;
    public InputActionReference trigger;

    void Update() {
        currTimer += Time.deltaTime;

        if (trigger.action.ReadValue<float>() > 0.5 && gameObject.transform.childCount > 0) {
            gunTransform = gameObject.transform.GetChild(0);

            //Debug.Log(currTimer);
            Debug.Log(gunTransform.gameObject.name);

            if (gunTransform.tag == "Pistol") {
                Debug.Log("Pistol");
                if (pistolTimer - currTimer <= 0 && resetTrigger) {
                    Debug.Log("Pistol shot");
                    GameObject newPistolBullet = Instantiate(pistolBulletTemplate, transform.position + (transform.forward * 1f), transform.rotation);
                    newPistolBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);

                    Destroy(newPistolBullet, 2);

                    currTimer = 0f;
                    resetTrigger = false;
                }
            }
            else if (gunTransform.tag == "SMG") {
                Debug.Log("SMG");
                if (SMGTimer - currTimer <= 0) {
                    Debug.Log("SMG shot");
                    GameObject newSMGBullet = Instantiate(SMGBulletTemplate, transform.position + (transform.forward * 1f), transform.rotation);
                    newSMGBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);

                    Destroy(newSMGBullet, 2);

                    currTimer = 0f;
                }
            }
            else if (gunTransform.tag == "Shotgun") {
                if (shotgunTimer - currTimer <= 0 && resetTrigger) {
                    GameObject newShotgunBullet = Instantiate(shotgunBulletTemplate, transform.position + (transform.forward * 1f), transform.rotation);
                    newShotgunBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);

                    Destroy(newShotgunBullet, 2);

                    currTimer = 0f;
                    resetTrigger = false;
                }
            }
            else if (gunTransform.tag == "GrenadeLauncher") {
                if (grenadeLauncherTimer - currTimer <= 0 && resetTrigger) {
                    GameObject newGrenadeLauncherBullet = Instantiate(grenadeLauncherBulletTemplate, transform.position + (transform.forward * 1f), transform.rotation);
                    newGrenadeLauncherBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);

                    Destroy(newGrenadeLauncherBullet, 2);

                    currTimer = 0f;
                    resetTrigger = false;
                }
            }
        }
        else if (trigger.action.ReadValue<float>() < 0.2) {
            resetTrigger = true;
        }
    }
}