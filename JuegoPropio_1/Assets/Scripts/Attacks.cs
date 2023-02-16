using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacks: MonoBehaviour {
    [SerializeField]
    GameObject prefabShot;
    public float attackCD, attackCDR, attackCDreference;
    public Vector2 destination, mousePos;
    public Image rellenoCD;

    void Start () {
        if (float.IsNaN (attackCD)) {
            attackCD = .75f;
        }
        if (float.IsNaN (attackCDR)) {
            attackCDR = 1f;
        }
        attackCD = attackCD / attackCDR;
        attackCDreference = attackCD;
    }

    // Update is called once per frame
    void Update () {

        mousePos = Input.mousePosition;
        destination = Camera.main.ScreenToWorldPoint (mousePos);

        if (attackCD >= 0f) {
            attackCD -= Time.deltaTime;
        } else {
            if (Input.GetMouseButtonUp (0)) {
                Instantiate (prefabShot, gameObject.transform.position, Quaternion.Euler (ReturnSpawnAngle ()));
                rellenoCD.enabled = true;
                rellenoCD.fillAmount = 1f;
                attackCD = attackCDreference / attackCDR;
            }
        }

        if (rellenoCD.fillAmount <= 0f && rellenoCD.IsActive () == true) {
            rellenoCD.enabled = false;
        } else if (rellenoCD.fillAmount > 0f && rellenoCD.IsActive () == true) {
            rellenoCD.fillAmount -= Time.deltaTime * (attackCDreference / attackCDR);
        }
    }

    public Vector3 ReturnSpawnAngle () {
        float orientation = 0f;
        Vector3 y = Vector3.zero;
        if (destination.x >= gameObject.transform.position.x) {
            orientation = 0f;
        } else {
            orientation = 180f;
        }
        if ((destination.y <= gameObject.transform.position.y + 15f) && (destination.y >= gameObject.transform.position.y - 15f)) {
            y = new Vector3 (0f, orientation, 0f);
        } else if (destination.y < gameObject.transform.position.y - 15f) {
            y = new Vector3 (0f, orientation, -45f);
        } else if (destination.y > gameObject.transform.position.y + 15f) {
            y = new Vector3 (0f, orientation, 45f);
        }
        return y;
    }
}
