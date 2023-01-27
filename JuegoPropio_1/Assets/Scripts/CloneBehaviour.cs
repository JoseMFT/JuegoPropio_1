using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehaviour: MonoBehaviour {
    float lifetime = 2f;
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (lifetime >= 0f) {
            lifetime -= Time.deltaTime;
        } else {
            Destroy (gameObject);
        }
    }
}
