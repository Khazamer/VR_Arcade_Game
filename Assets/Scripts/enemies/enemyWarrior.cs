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
    
    private Animator animator;
    private bool movingForward = true;
    private bool isDead = false;

    //private bool framePassed = false;
    //private int count = 0;
    private int damageCount = 0;
    private int damageTimer = 0;
    private float attackDistance = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.transform.position);
        //Debug.Log(damageCount);

        if (Vector3.Distance(gameObject.transform.position, playerTarget.transform.position) < attackDistance) {
            movingForward = false;
            animator.SetBool("Moving Forward", movingForward);
            //animator.Play(PA_WarriorAttack_Clip, 0);
        }

        if (movingForward) {
            Vector3 lookNoY = new Vector3(playerTarget.transform.position[0], 0, playerTarget.transform.position[2]);
            //transform.LookAt(playerTarget.transform.position);
            transform.LookAt(lookNoY);
            transform.position += transform.forward * speed; 
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
                    playerTarget.GetComponent<playerHealth>().addDamage(1);
                    damageCount ++;
                }
            }
        }
    }

    public void died() {
        isDead = true;
        animator.SetBool("Dead", isDead);

        gameObject.GetComponent<BoxCollider>().enabled = false; //This could work for almost any component
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject, 3);
    }

    // need to check if moving forward (transistioning forward)
    // check if in attack range and go for it
    // have back up for if you don't kill that enemy
    // when it dies, change the animation, wait 1 frame (ideally) and then wait for it to finish before killing it
    // deactivate the collider and rigid body (isActive) and set rigid body to kinematic on death
}