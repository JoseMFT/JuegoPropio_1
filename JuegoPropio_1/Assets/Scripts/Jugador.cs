using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Jugador: MonoBehaviour {
    Vector3 playerPos, prevPos, ogSize, dashImagePos;
    public bool rescale = true, canDash = true, onAir = false;
    float speed = 15f, jumpForce = 10f, dashCoolDown = 5f, doubleJumpCoolDown = 7.5f, cloneCD = .05f;
    public int jumpCount = 0, jumpLimit = 2;
    Rigidbody2D rigidbodyCharacter;

    [SerializeField]
    Image jumpCD, dashCD;

    [SerializeField]
    GameObject face, clone;

    [SerializeField]
    TextMeshProUGUI textoCDDash, textoCDJump;

    private void Awake () {
        face.SetActive (true);
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

        if (jumpCount < jumpLimit) {
            if (jumpCount == 0) {
                if (Input.GetKey ("space")) {
                    jumpCount++;
                    rigidbodyCharacter.AddForce (Vector3.up * jumpForce, ForceMode2D.Impulse);
                }
            } else if (jumpCount == 1) {
                if (playerPos.y < prevPos.y && Input.GetKey ("space")) {
                    jumpCD.fillAmount = 0f;
                    jumpCount++;
                    rigidbodyCharacter.AddForce (Vector3.up * jumpForce * 1.5f, ForceMode2D.Impulse);
                }
            }
        }
        if (Input.GetKey ("d")) {
            if (face.transform.rotation != Quaternion.Euler (0f, 0f, 0f)) {
                face.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
            }
            gameObject.transform.position += Vector3.right * Time.deltaTime * speed;
            if (canDash == true) {
                if (Input.GetKey ("left shift")) {
                    face.transform.localScale = new Vector3 (1f, .33f, 1f);
                    dashCD.fillAmount = 0;
                    gameObject.GetComponent<TrailRenderer> ().enabled = true;
                    canDash = false;
                    gameObject.transform.position += Vector3.right * speed;
                    speed = 20f;
                }
            }

        } else if (Input.GetKey ("a")) {
            if (face.transform.rotation != Quaternion.Euler (0f, 180f, 0f)) {
                face.transform.rotation = Quaternion.Euler (0f, 180f, 0f);
            }
            gameObject.transform.position += Vector3.left * Time.deltaTime * speed;
            if (canDash == true) {
                if (Input.GetKey ("left shift")) {
                    face.transform.localScale = new Vector3 (1f, .33f, 1f);
                    dashCD.fillAmount = 0f;
                    gameObject.GetComponent<TrailRenderer> ().enabled = true;
                    canDash = false;
                    gameObject.transform.position += Vector3.left * speed;
                    speed = 20f;
                }
            }
        }

        if (onAir == false) {
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
        } else if (onAir == true) {
            if (Input.GetKey ("left ctrl")) {
                rigidbodyCharacter.mass = 120f;
                gameObject.transform.localScale = new Vector3 (ogSize.x, ogSize.y * .65f, ogSize.z);
                rescale = false;
            }
        }
        if (!Input.GetKey ("left ctrl")) {
            rigidbodyCharacter.mass = 1f;
            rescale = true;
        }
        if (rescale == true) {
            gameObject.transform.localScale = ogSize;
        }

        if (canDash == false) {

            if (dashCoolDown >= 0f) {
                dashCoolDown -= Time.deltaTime;
                dashCD.fillAmount += 1f / 5f * Time.deltaTime;
                textoCDDash.text = dashCoolDown.ToString (".00") + " s";
                if (dashCoolDown < 2f) {
                    gameObject.GetComponent<TrailRenderer> ().enabled = false;
                    face.transform.localScale = Vector3.one;
                    speed = 15f;
                }
            } else {
                dashCoolDown = 5f;
                dashCD.fillAmount = 1f;
                textoCDDash.text = " ";
                canDash = true;
            }

            if (dashCoolDown >= 2f) {
                if (cloneCD >= 0f) {
                    cloneCD -= Time.deltaTime;
                } else {
                    Instantiate (clone, transform.position, Quaternion.identity);
                    cloneCD = .05f;
                }
            }
        }

        if (jumpCount == 2) {
            jumpLimit = 1;
        } else if (jumpCount != 0) {
            onAir = true;
        }

        if (jumpLimit == 1) {

            if (doubleJumpCoolDown >= 0f) {
                doubleJumpCoolDown -= Time.deltaTime;
                jumpCD.fillAmount += 1f / 7.5f * Time.deltaTime;
                textoCDJump.text = doubleJumpCoolDown.ToString (".00") + " s";
            } else {
                jumpLimit = 2;
                doubleJumpCoolDown = 7.5f;
                jumpCD.fillAmount = 1f;
                textoCDJump.text = " ";
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Floor") {
            jumpCount = 0;
            onAir = false;
        }
    }
}
