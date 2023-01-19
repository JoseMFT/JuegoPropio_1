using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacks: MonoBehaviour {
    [SerializeField]
    GameObject prefabShot;
    float attackCD = .15f, attackCDR = 0f;
    public Vector3 direction;
    public Image rellenoCD;

    public static Attacks ataques;
    void Start () {
        rellenoCD.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update () {
        if (attackCD >= 0f) {
            attackCD -= Time.deltaTime;
        } else {
            if (Input.GetKey ("j")) {
                Instantiate (prefabShot, gameObject.transform.position, Quaternion.identity);
                rellenoCD.fillAmount = 1f;
                attackCD = 2f - attackCDR;
            }

            if (Input.GetKey ("a")) {
                direction = Vector3.left;
            } else if (Input.GetKey ("d")) {
                direction = Vector3.right;
            }
        }
    }
}
