using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

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

    void Update() {
        currTimer += Time.deltaTime;

        if (gameObject.transform.parent != null && gameObject.transform.parent.tag == "Hand") {
            if (!alreadyPushed && ((gameObject.transform.parent.name == "Right hand" && rightTrigger.action.ReadValue<float>() > 0.5) || (gameObject.transform.parent.name == "Left hand" && leftTrigger.action.ReadValue<float>() > 0.5))) {
                if (Timer - currTimer <= 0) {
                    alreadyPushed = true;

                    GameObject newBullet = Instantiate(BulletTemplate, transform.position + (transform.forward * 0.4f) + (transform.up * 0.05f), transform.rotation);
                    newBullet.transform.localRotation *= Quaternion.Euler(-90, 0, 0);
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

                    currTimer = 0f;
                }
            }
            else if (alreadyPushed && ((gameObject.transform.parent.name == "Right hand" && rightTrigger.action.ReadValue<float>() < 0.5) || (gameObject.transform.parent.name == "Left hand" && leftTrigger.action.ReadValue<float>() < 0.5))) {
                alreadyPushed = false;
            }
        }
    }
}