using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class raycast : MonoBehaviour
{
    [SerializeField] private InputActionReference castingButtonLeft;
    [SerializeField] private InputActionReference castingButtonRight;

    // Start is called before the first frame update
    void Start()
    {
        castingButtonLeft.action.performed += DoRaycast;
        castingButtonRight.action.performed += DoRaycast;
    }

    void DoRaycast(InputAction.CallbackContext __) {
        RaycastHit hit;

        bool didHit = Physics.Raycast(
            transform.position + (transform.forward * 0.5f),
            transform.forward,
            out hit,
            Mathf.Infinity);

        if (didHit) {
            globals.levelTag = hit.transform.tag;
        }
    }
}
