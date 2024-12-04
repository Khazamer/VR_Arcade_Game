using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class handRayCast : MonoBehaviour
{
    [SerializeField] private InputActionReference buttonPress;
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameObject origin;

    // Start is called before the first frame update
    void Start()
    {
        origin = FindObjectOfType<XROrigin>().gameObject;

        buttonPress.action.performed += DoRaycast;
    }

    void DoRaycast(InputAction.CallbackContext __) {
        //Debug.Log("Started");
        RaycastHit hit;

        bool didHit = Physics.Raycast(
            transform.position + (transform.forward * 0.5f),
            transform.forward,
            out hit,
            Mathf.Infinity,
            mask);

        if (didHit) {
            //globals.levelTag = hit.transform.tag;
            origin.GetComponent<gameManager>().startLevel(hit);
            Debug.Log(hit.transform.tag);
        }
    }
}
