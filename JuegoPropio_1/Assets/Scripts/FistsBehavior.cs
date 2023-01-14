using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistsBehavior : MonoBehaviour
{
    float speed = 2f;
    Vector3 destiny;

    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler (0f, 0f, Random.Range (0f, 359.99f));
        destiny = gameObject.transform.position + new Vector3 (2 * Mathf.Cos (gameObject.transform.rotation.z), 2* Mathf.Sin (gameObject.transform.rotation.z), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (destiny != gameObject.transform.position) {
            gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, destiny, speed * Time.deltaTime);
        } else {
            LeanTween.alphaCanvas (gameObject.GetComponent <CanvasGroup>(), 0f, 1f).setOnComplete (() => {
                Destroy (gameObject);
            });
        }
    }
}