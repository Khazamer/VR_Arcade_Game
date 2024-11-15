using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.XR.CoreUtils;
using UnityEngine;

public class enemyDrone : MonoBehaviour
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
    private float attackDistance = 5f;

    //new stuff
    public float gunRange = 50f;
    public float laserDuration = 0.05f;
 
    LineRenderer laserLine;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.transform.position);
        //Debug.Log(damageCount);

        if (Vector3.Distance(gameObject.transform.position, playerTarget.transform.position) < attackDistance) {
            movingForward = false;
            animator.SetBool("Moving Forward", movingForward);
        }

        if (movingForward) {
            transform.LookAt(playerTarget.transform.position);
            transform.position += transform.forward * speed; 
        }

        /*
        if (!movingForward && !isDead) {
            damageTimer ++;

            if (damageTimer > 100) {
                damageTimer = 0;
                laserLine.SetPosition(0, gameObject.transform.position + (gameObject.transform.forward * 0.5f));
                Vector3 rayOrigin = gameObject.transform.position;
                RaycastHit hit;
                if(Physics.Raycast(rayOrigin, gameObject.transform.forward, out hit, gunRange))
                {
                    laserLine.SetPosition(1, hit.point);
                    //Destroy(hit.transform.gameObject);
                }
                else
                {
                    laserLine.SetPosition(1, rayOrigin + (gameObject.transform.forward * gunRange));
                }
                StartCoroutine(ShootLaser());
            }

            IEnumerator ShootLaser()
            {
                laserLine.enabled = true;
                yield return new WaitForSeconds(laserDuration);
                laserLine.enabled = false;
            }
        }
        */

        if (!movingForward && !isDead) {
            damageTimer ++;

            if(damageTimer == 100) {
                laserLine.SetPosition(0, gameObject.transform.position + (gameObject.transform.forward * 0.5f));
                Vector3 rayOrigin = gameObject.transform.position;
                RaycastHit hit;
                if(Physics.Raycast(rayOrigin, gameObject.transform.forward, out hit, gunRange))
                {
                    laserLine.SetPosition(1, hit.point);
                }
                else
                {
                    laserLine.SetPosition(1, rayOrigin + (gameObject.transform.forward * gunRange));
                }

                laserLine.material.color = Color.blue;
                laserLine.startWidth = 0.025f;
                laserLine.endWidth = 0.025f;
                laserLine.enabled = true;
            }
            else if (damageTimer == 200) {
                laserLine.startWidth = 0.1f;
                laserLine.endWidth = 0.1f;
                laserLine.material.color = Color.red;
            }
            else if (damageTimer == 250) {
                damageTimer = 0;

                laserLine.enabled = false;
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