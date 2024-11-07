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

    void Update() {
        currTimer += Time.deltaTime;

        if (gameObject.transform.parent.tag == "Hand") {
            if ((gameObject.transform.parent.name == "Right hand" && rightTrigger.action.ReadValue<float>() > 0.5) || (gameObject.transform.parent.name == "Left hand" && leftTrigger.action.ReadValue<float>() > 0.5)) {
                if (SMGTimer - currTimer <= 0) {
                    //Debug.Log("SMG shot");
                    GameObject newSMGBullet = Instantiate(SMGBulletTemplate, transform.parent.position + (transform.parent.forward * 1f), transform.parent.rotation);
                    newSMGBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
                    Destroy(newSMGBullet, 2);

                    GameObject newSMGShell = Instantiate(SMGBulletTemplate, transform.parent.position + (transform.parent.right * 0.5f), transform.parent.rotation);
                    //newSMGShell.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
                    Destroy(newSMGShell, 1);

                    currTimer = 0f;
                }
            }
        }
    }
}