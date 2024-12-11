using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.XR.CoreUtils;
using UnityEngine;

public class enemyWarrior : MonoBehaviour
{
    [SerializeField] GameObject playerTarget;
    [SerializeField] float speed = 3f;
    [SerializeField] GameObject playerObject;
    
    private Animator animator;
    private bool movingForward = true;
    private bool isDead = false;

    //private bool framePassed = false;
    //private int count = 0;
    private int damageCount = 0;
    private int damageTimer = 0;
    private float attackDistance = 3f;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        playerObject = FindObjectOfType<XROrigin>().gameObject;
        playerTarget = FindObjectOfType<Camera>().gameObject;

        //Debug.Log(playerObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.transform.position);
        //Debug.Log(damageCount);

        // For floor debugging
        //Debug.Log(gameObject.transform.rotation.eulerAngles);

        if (Vector3.Distance(gameObject.transform.position, playerTarget.transform.position) < attackDistance) {
            movingForward = false;
            animator.SetBool("Moving Forward", movingForward);
            //animator.Play(PA_WarriorAttack_Clip, 0);
        }
        else {
            movingForward = true;
            animator.SetBool("Moving Forward", movingForward);
        }

        if (movingForward && !isDead) {
            Vector3 lookSmallY = new Vector3(playerTarget.transform.position[0], transform.position[1] + 0.5f, playerTarget.transform.position[2]);
            //transform.LookAt(playerTarget.transform.position);
            /*
            if (transform.rotation.eulerAngles[0] > 20) {
                transform.rotation *= Quaternion.Euler(-1 * (transform.rotation.eulerAngles[0] - 20), 0, 0);
            }
            else if (transform.rotation.eulerAngles[0] < 0) {
                transform.rotation *= Quaternion.Euler(-1 * transform.rotation.eulerAngles[0], 0, 0);
            }
            */
            transform.LookAt(lookSmallY);
            transform.position += transform.forward * speed;

            //transform.LookAt(playerTarget.transform);
            //transform.position += transform.forward * speed; // add something here to move them above the mesh since they seem to fall through very easily
        }

        if (!movingForward && !isDead) {
            //some sort of attack
            //playerTarget.GetComponent<playerHealth>().addDamage(1/180);
            /*
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                count ++;
                playerTarget.GetComponent<playerHealth>().addDamage(1);
            }
            */
            damageTimer ++;

            if (damageTimer > 100) {
                //Debug.Log(Mathf.RoundToInt(animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
                Debug.Log(damageCount);
                if (damageCount < Mathf.RoundToInt(animator.GetCurrentAnimatorStateInfo(0).normalizedTime)) {
                    //count ++;
                    playerObject.GetComponent<playerHealth>().addDamage(1);
                    damageCount ++;
                }
            }
        }
    }

    // Correct for falling through floor
    /*
    void OnTriggerEnter() {
        transform.position += transform.up * 0.25f;
    }
    */

    public void died() {
        isDead = true;
        animator.SetBool("Dead", isDead);

        globals.numKills ++;

        gameObject.GetComponent<BoxCollider>().enabled = false; //This could work for almost any component
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Destroy(gameObject, 0.5f);
    }

    // need to check if moving forward (transistioning forward)
    // check if in attack range and go for it
    // have back up for if you don't kill that enemy
    // when it dies, change the animation, wait 1 frame (ideally) and then wait for it to finish before killing it
    // deactivate the collider and rigid body (isActive) and set rigid body to kinematic on death
}