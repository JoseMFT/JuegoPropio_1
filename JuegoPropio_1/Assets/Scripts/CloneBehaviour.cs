using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneBehaviour: MonoBehaviour {
    public float lifetime = 3f, speed = 60f;
    CanvasGroup fistCanvas;


    void Start () {
        fistCanvas = gameObject.GetComponent<CanvasGroup> ();
    }

    // Update is called once per frame
    void Update () {
        if (lifetime >= 0f) {
            lifetime -= Time.deltaTime;
            transform.position += transform.forward * Time.deltaTime * speed;
            if (transform.position.z != 0f) {
                transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
            }
        } else {
            gameObject.GetComponent<Collider2D> ().enabled = false;
            LeanTween.alphaCanvas (fistCanvas, 0f, 1f).setEaseOutCubic ().setOnComplete (() => {
                Destroy (gameObject);
            });
        }
    }
}
