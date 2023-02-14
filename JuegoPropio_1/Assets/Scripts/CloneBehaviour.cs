using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneBehaviour: MonoBehaviour {
    float lifetime = 3f, speed = 60f;
    Vector3 mousePos;
    GameObject referencePos;
    CanvasGroup fistCanvas;


    void Start () {
        mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        referencePos = new GameObject(" ");
        referencePos.transform.position = mousePos;
        gameObject.transform.LookAt (referencePos.transform.position);
        Destroy (referencePos);
        fistCanvas = gameObject.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update () {
        if (lifetime >= 0f) {
            lifetime -= Time.deltaTime;
            transform.position += transform.forward * Time.deltaTime * speed;
        } else {
            gameObject.GetComponent<Collider2D> ().enabled = false;
            LeanTween.alphaCanvas (fistCanvas, 0f, 1f).setEaseOutCubic ().setOnComplete (() => {
                Destroy (gameObject);
            });
        }
    }
}
