using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class handRayCast : MonoBehaviour
{
    [SerializeField] private InputActionReference teleportButtonPress;
    [SerializeField] private GameObject origin;

    // Start is called before the first frame update
    void Start()
    {
        teleportButtonPress.action.performed += DoRaycast;
    }

    void DoRaycast(InputAction.CallbackContext __) {
        Debug.Log("Started");
        RaycastHit hit;

        bool didHit = Physics.Raycast(
            transform.position + (transform.forward * 0.5f),
            transform.forward,
            out hit,
            Mathf.Infinity);

        if (didHit) {
            //globals.levelTag = hit.transform.tag;
            origin.GetComponent<gameManager>().startLevel(hit);
            Debug.Log(hit.transform.tag);
        }
    }
}
