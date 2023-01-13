using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Jugador: MonoBehaviour {
    Vector3 playerPos, prevPos, ogSize, dashImagePos;
    public bool rescale = true, canDash = true;
    float speed = 10f, jumpForce = 10f, dashCoolDown = 5f, jumpCoolDown = 7.5f;
    public int jumpCount = 0, jumpLimit = 2;
    Rigidbody2D rigidbodyCharacter;

    [SerializeField]
    Image jumpCD, dashCD;



    [SerializeField]
    TextMeshProUGUI textoCDDash, textoCDJump;

    private void Awake () {
        ogSize = gameObject.transform.localScale;
        rigidbodyCharacter = gameObject.GetComponent<Rigidbody2D> ();
    }

    private void Start () {
        dashImagePos = dashCD.transform.position;
        textoCDDash.text = " ";
    }

    void Update () {
        prevPos = playerPos;
        playerPos = gameObject.transform.position;

        if (jumpCount < jumpLimit && Input.GetKey ("space")) {
            jumpCount++;
            rigidbodyCharacter.AddForce (Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
            if (Input.GetKey ("d")) {
                gameObject.transform.position += Vector3.right * Time.deltaTime * speed;
                if (canDash == true) {
                    if (Input.GetKey ("left shift")) {
                    dashCD.fillAmount = 0;
                    gameObject.GetComponent<TrailRenderer> ().enabled = true;
                        canDash = false;
                        gameObject.transform.position += Vector3.right * speed;
                        speed += speed * .25f;
                    }
                }

            } else if (Input.GetKey ("a")) {
                gameObject.transform.position += Vector3.left * Time.deltaTime * speed;
                if (canDash == true) {
                    if (Input.GetKey ("left shift")) {
                    dashCD.fillAmount = 0f;
                    gameObject.GetComponent<TrailRenderer> ().enabled = true;
                        canDash = false;
                        gameObject.transform.position += Vector3.left * speed;
                    speed += speed * .25f;
                    }
                }
            }

        if (Input.GetKey ("s")) {
            rescale = false;
            if (gameObject.transform.localScale.y > ogSize.y * .65f) {
                gameObject.transform.localScale = new Vector3 (ogSize.x, ogSize.y * .65f, ogSize.z);
            }
        } else if (!Input.GetKey ("s")) {
            if (gameObject.transform.localScale.y != ogSize.y) {
                rescale = true;
            }
        }

        if (rescale == true) {
            gameObject.transform.localScale = ogSize;
        }

        if (canDash == false) {

            if (dashCoolDown >= 0f) {
                dashCoolDown -= Time.deltaTime;
                dashCD.fillAmount += 1f / 5f * Time.deltaTime;
                textoCDDash.text = dashCoolDown.ToString (".00") + " s";
            } else {
                speed = speed / 1.25f;
                gameObject.GetComponent<TrailRenderer> ().enabled = false;
                dashCoolDown = 5f;
                dashCD.fillAmount = 1f;
                textoCDDash.text = " ";
                canDash = true;
            }
        }

        if (jumpCount == 2) {
            jumpLimit--;
        }

        if (jumpLimit == 1) {

            jumpCD.fillAmount = 0f;

            if (jumpCoolDown >= 0f) {
                jumpCoolDown -= Time.deltaTime;
                jumpCD.fillAmount += 1f / 7.5f * Time.deltaTime;
                textoCDJump.text = jumpCoolDown.ToString (".00") + " s";
            } else {
                jumpLimit++;
                jumpCoolDown = 7.5f;
                jumpCD.fillAmount = 1f;
                textoCDJump.text = " ";
            }
        }

    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Floor") {
            jumpCount = 0;
        }
    }
}
