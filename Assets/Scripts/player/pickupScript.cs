using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class pickupScript : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;
    [SerializeField] LayerMask pickupMask;

    [HeaderAttribute("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;

    //Input detection
    [SerializeField] private InputActionReference trigger;

    private bool alreadyDone = false;

    private void Update() 
    {
        if (trigger.action.ReadValue<float>() > 0.5f && !alreadyDone)
        {
            alreadyDone = true;

            if (heldObj == null) 
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, pickupMask)) 
                {
                    //Pickup Object
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        else if (trigger.action.ReadValue<float>() < 0.2f)
        {
            alreadyDone = false;
        }
    }

    void PickupObject(GameObject pickObj) 
    {
        if(pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;

            //heldObjRB.position = holdArea.transform.position;
            /*
            Vector3 moveDirection = holdArea.position - heldObjRB.position;
            heldObjRB.AddForce(moveDirection * 100);
            heldObjRB.rotation = holdArea.transform.rotation;
            */

            heldObjRB.drag = 10;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;

            heldObjRB.transform.localPosition = new Vector3(0, 0, 0);
            heldObjRB.transform.rotation = holdArea.rotation;

            heldObjRB.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void DropObject() 
    {
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObjRB.transform.parent = null;
        heldObj = null;
    }
}