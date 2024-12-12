using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class grenadeLauncher : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] GameObject BulletTemplate;
    [SerializeField] float shootPower = 100f;
    [SerializeField] float Timer = 0.2f;
    private float currTimer = 0f;
    public InputActionReference rightTrigger;
    public InputActionReference leftTrigger;
    public ParticleSystem muzzleFlash;
    public AudioClip gunShot;
    private bool alreadyPushed = false;

    //reload check
    [SerializeField] int ammo = 6;
    [SerializeField] TMP_Text ammo_display;
    private int currAmmo;

    void Start() {
        currAmmo = ammo;

        updateAmmoCount();
    }

    void Update() {
        currTimer += Time.deltaTime;

        if (gameObject.transform.parent != null && gameObject.transform.parent.tag == "Hand") {
            if (currAmmo > 0) {
                if (!alreadyPushed && ((gameObject.transform.parent.name == "Right hand" && rightTrigger.action.ReadValue<float>() > 0.5) || (gameObject.transform.parent.name == "Left hand" && leftTrigger.action.ReadValue<float>() > 0.5))) {
                    if (Timer - currTimer <= 0) {
                        alreadyPushed = true;
                        currAmmo -= 1;
                        updateAmmoCount();

                        GameObject newBullet = Instantiate(BulletTemplate, transform.position + (transform.forward * 0.5f) + (transform.up * 0.03f), transform.rotation);
                        newBullet.transform.localRotation *= Quaternion.Euler(-90, 0, 0);
                        Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), GetComponent<Collider>());
                        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
                        //Destroy(newBullet, 2);

                        /*
                        GameObject newShell = Instantiate(ShellTemplate, transform.position + (transform.forward * -0.1f) + (transform.right * 0.04f) + (transform.up * 0.06f), transform.rotation);
                        newShell.transform.localRotation *= Quaternion.Euler(-90, 0, 0);
                        newShell.GetComponent<Rigidbody>().AddForce(transform.right * (shootPower / 10f));
                        newShell.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1 * (shootPower / 10f), 0));
                        Destroy(newShell, 1);
                        */

                        muzzleFlash.Play();

                        GetComponent<AudioSource>().PlayOneShot(gunShot);

                        gameObject.transform.parent.parent.GetComponent<XRBaseController>().SendHapticImpulse(0.3f, 0.1f);

                        currTimer = 0f;
                    }
                }
                else if (alreadyPushed && ((gameObject.transform.parent.name == "Right hand" && rightTrigger.action.ReadValue<float>() < 0.5) || (gameObject.transform.parent.name == "Left hand" && leftTrigger.action.ReadValue<float>() < 0.5))) {
                    alreadyPushed = false;
                }
            }
            if (transform.forward[1] > 0.9f) {
                    currAmmo = ammo;
                    updateAmmoCount();
            }
        }
    }

    void updateAmmoCount() {
        ammo_display.SetText(currAmmo.ToString());

        ammo_display.color = new Color(((float)ammo - (float)currAmmo)/(float)ammo, (float)currAmmo/(float)ammo, 0);
    }
}