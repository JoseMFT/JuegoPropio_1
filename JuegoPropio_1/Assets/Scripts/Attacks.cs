using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [SerializeField]
    GameObject prefabFist;
    float attackCD = .15f, attackCDR = 0f, spawnAngle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCD >= 0f) {
            attackCD -= Time.deltaTime;
        } else {
            Instantiate (prefabFist, gameObject.transform.position, Quaternion.identity);
            attackCD = 2f - attackCDR;
        }
    }
}
