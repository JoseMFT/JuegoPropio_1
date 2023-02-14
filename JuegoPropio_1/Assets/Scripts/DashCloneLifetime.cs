using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCloneLifetime : MonoBehaviour
{
    public float lifetime = 1f;

    void Update()
    {
        if (lifetime >= 0f) {
            lifetime -= Time.deltaTime;
        } else {
            LeanTween.alphaCanvas (gameObject.GetComponent<CanvasGroup> (), 0f, .75f).setEaseOutCubic ().setOnComplete (() => {
                Destroy (gameObject);
            });
        }
    }
}
