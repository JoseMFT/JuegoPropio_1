using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistsBehavior: MonoBehaviour {
    float speed = 15f, timer = 5f;
    public Vector3 destination;
    void Start () {
        destination = transform.position * 100f;
    }

    // Update is called once per frame
    void Update () {

        if (timer >= 0f) {
            timer -= Time.deltaTime;
            //gameObject.transform.position += Vector3.Distance (gameObject.transform.position, destination) / *Time.deltaTime;
        } else {
            Destroy (gameObject);
        }
    }
}
