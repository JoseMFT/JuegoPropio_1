using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador: MonoBehaviour {
    float timeJump;
    Vector2 movimiento;
    bool puedeSaltar = true;
    bool saltando = false;
    const float kVelocidad = 15f;
    void Start () {
    }


    void Update () {
        movimiento.x = Input.GetAxis ("Horizontal") * Time.deltaTime * kVelocidad;
        if (timeJump == 1.5f)
            puedeSaltar = true;
        if (saltando == true) {
            movimiento.y = movimiento.y + kVelocidad * Time.deltaTime;
        }
        if (Input.GetKey ("space")) {
            for (timeJump = 1.5f; timeJump <= 0f; timeJump = timeJump - Time.deltaTime) {
                puedeSaltar = false;
                saltando = true;
            }
        }
    }
    private void FixedUpdate () {
        transform.Translate (movimiento);
    }
}
