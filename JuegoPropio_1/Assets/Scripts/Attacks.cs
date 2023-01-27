using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacks: MonoBehaviour {
    [SerializeField]
    GameObject prefabShot;
    float attackCD = 3f, attackCDR = 0f;
    public Vector2 destination, mousePos;
    public Image rellenoCD;

    void Start () {
        destination = Vector3.right;
        rellenoCD.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update () {
        mousePos = Input.mousePosition;
        destination = Camera.main.ScreenToWorldPoint (mousePos);

        if (attackCD >= 0f) {
            attackCD -= Time.deltaTime;
        } else {

            if (Input.GetMouseButtonUp (0)) {
                Instantiate (prefabShot, gameObject.transform.position, Quaternion.LookRotation (destination));
                rellenoCD.fillAmount = 1f;
                attackCD = 3f - attackCDR;
            }
        }
    }
}
