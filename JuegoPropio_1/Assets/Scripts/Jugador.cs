using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador: MonoBehaviour {
    Vector2 movimiento;
    bool puedeSaltar = true;
    const float kVelocidad = 15f;
    void Update () {
        movimiento.x = Input.GetAxis ("Horizontal") * Time.deltaTime * kVelocidad;
        if (/*puedeSaltar == true &&*/ Input.GetKey ("space")) {
            //saltando ();
            for (float i = 1.5f; i <= 0f; i -= Time.deltaTime) {
                movimiento.y += kVelocidad * Time.deltaTime;
            }
        }
    }
    /*private void saltando () {
        for (float i = 1.5f; i <= 0f; i -= Time.deltaTime) {
            movimiento.y += kVelocidad * Time.deltaTime;
            puedeSaltar = false;
        }
        puedeSaltar = true;
    }*/
    private void FixedUpdate () {
        transform.Translate (movimiento);

    }
}
