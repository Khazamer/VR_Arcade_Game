using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class doTween : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Transform cube;
    [SerializeField] ParticleSystem particles;

    public void moveCube() {
        cube.GetComponent<MeshRenderer>().enabled = true;

        transform.DOLocalMoveY(1f, 2f);

        cube.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
            //.SetLoops(1, LoopType.Restart)
            .SetEase(Ease.InElastic);

        particles.Play();

        Invoke("hideCube", 2f);
    }

    void hideCube() {
        cube.GetComponent<MeshRenderer>().enabled = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
