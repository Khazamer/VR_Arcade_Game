using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class smgShoot : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] GameObject SMGBulletTemplate;
    [SerializeField] GameObject SMGShellTemplate;
    [SerializeField] float shootPower = 100f;
    private float currTimer = 0f;
    private float SMGTimer = 0.05f;
    public InputActionReference rightTrigger;
    public InputActionReference leftTrigger;
    public ParticleSystem muzzleFlash;
    public AudioClip gunShot;

    void Update() {
        currTimer += Time.deltaTime;

        if (gameObject.transform.parent.tag == "Hand") {
            if ((gameObject.transform.parent.name == "Right hand" && rightTrigger.action.ReadValue<float>() > 0.5) || (gameObject.transform.parent.name == "Left hand" && leftTrigger.action.ReadValue<float>() > 0.5)) {
                if (SMGTimer - currTimer <= 0) {
                    //Debug.Log("SMG shot");
                    GameObject newSMGBullet = Instantiate(SMGBulletTemplate, transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), transform.rotation);
                    newSMGBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
                    Destroy(newSMGBullet, 2);

                    GameObject newSMGShell = Instantiate(SMGBulletTemplate, transform.position + (transform.forward * -0.05f) + (transform.right * 0.04f) + (transform.up * 0.1f), transform.rotation);
                    newSMGShell.GetComponent<Rigidbody>().AddForce(transform.right * (shootPower / 10f));
                    newSMGShell.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1 * (shootPower / 10f), 0));
                    Destroy(newSMGShell, 1);

                    muzzleFlash.Play();

                    GetComponent<AudioSource>().PlayOneShot(gunShot);
                    //GetComponent<AudioSource>().Play(0);

                    currTimer = 0f;
                }
            }
        }
    }
}