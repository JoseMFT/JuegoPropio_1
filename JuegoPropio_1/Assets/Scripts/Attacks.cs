using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacks: MonoBehaviour {
    [SerializeField]
    GameObject prefabShot;
    public float attackCD, attackCDR , attackCDreference;    
    public Vector2 destination, mousePos;
    public Image rellenoCD;

    void Start () {
        if (attackCD == null) {
            attackCD = .75f;
        }
        if (attackCDR == null) {
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
                Instantiate (prefabShot, gameObject.transform.position, Quaternion.identity);
                rellenoCD.enabled = true;
                rellenoCD.fillAmount = 1f;
                attackCD = attackCDreference/attackCDR;
            }
        }

        if (rellenoCD.fillAmount <= 0f && rellenoCD.IsActive () == true) {
            rellenoCD.enabled = false;
        } else if (rellenoCD.fillAmount > 0f && rellenoCD.IsActive() == true) {
            rellenoCD.fillAmount -= Time.deltaTime * (attackCDreference / attackCDR);
        }
    }
}
