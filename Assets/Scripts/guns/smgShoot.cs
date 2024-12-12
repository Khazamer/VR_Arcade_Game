using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class smgShoot : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] GameObject BulletTemplate;
    [SerializeField] GameObject ShellTemplate;
    [SerializeField] float shootPower = 100f;
    [SerializeField] float Timer = 0.05f;
    private float currTimer = 0f;
    public InputActionReference rightTrigger;
    public InputActionReference leftTrigger;
    public ParticleSystem muzzleFlash;
    public AudioClip gunShot;

    //reload check
    [SerializeField] int ammo = 30;
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
                if ((gameObject.transform.parent.name == "Right hand" && rightTrigger.action.ReadValue<float>() > 0.5) || (gameObject.transform.parent.name == "Left hand" && leftTrigger.action.ReadValue<float>() > 0.5)) {
                    if (Timer - currTimer <= 0) {
                        currAmmo -= 1;
                        updateAmmoCount();

                        //Debug.Log("SMG shot");
                        GameObject newBullet = Instantiate(BulletTemplate, transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), transform.rotation);
                        Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), GetComponent<Collider>());
                        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
                        Destroy(newBullet, 3);

                        GameObject newShell = Instantiate(ShellTemplate, transform.position + (transform.forward * -0.05f) + (transform.right * 0.04f) + (transform.up * 0.1f), transform.rotation);
                        newShell.GetComponent<Rigidbody>().AddForce(transform.right * (shootPower / 10f));
                        newShell.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1 * (shootPower / 10f), 0));
                        Destroy(newShell, 1);

                        muzzleFlash.Play();

                        GetComponent<AudioSource>().PlayOneShot(gunShot);
                        //GetComponent<AudioSource>().Play(0);

                        gameObject.transform.parent.parent.GetComponent<XRBaseController>().SendHapticImpulse(0.1f, 0.1f);

                        currTimer = 0f;
                    }
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