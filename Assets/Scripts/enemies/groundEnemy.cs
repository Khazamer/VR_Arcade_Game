using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundEnemy : MonoBehaviour
{
    private Animation anim;
    //private bool activate = true;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.transform.position);

        count += 1;
        if (count == 100) {
            anim.Play();
        }
    }
}
