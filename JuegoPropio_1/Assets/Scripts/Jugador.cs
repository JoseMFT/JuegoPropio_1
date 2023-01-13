using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Jugador: MonoBehaviour {
    Vector3 playerPos, prevPos, ogSize, dashImagePos;
    public bool canJump, rescale = true, canDash = true;
    float speed = 10f, jumpForce = 7f, coolDown = 5f;
    public int jumpCount = 0;
    Rigidbody2D rigidbodyCharacter;

    [SerializeField]
    GameObject jumpCD, dashCD;



    [SerializeField]
    TextMeshProUGUI textoCD;

    private void Awake () {
        ogSize = gameObject.transform.localScale;
        rigidbodyCharacter = gameObject.GetComponent<Rigidbody2D> ();
    }
    private void Start () {
        dashImagePos = dashCD.transform.position;
        textoCD.text = " ";
    }
    void Update () {
        prevPos = playerPos;
        playerPos = gameObject.transform.position;

        if (canJump == true && Input.GetKey ("space")) {
            jumpCount++;
            canJump = false;
            rigidbodyCharacter.AddForce (Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey ("d")) {
            gameObject.transform.position += Vector3.right * Time.deltaTime * speed;
            if (canDash == true) {
                if (Input.GetKey ("left shift")) {
                    canDash = false;
                    rigidbodyCharacter.AddForce (Vector3.right * speed * 2f, ForceMode2D.Impulse);
                }
            }

        } else if (Input.GetKey ("a")) {
            gameObject.transform.position += Vector3.left * Time.deltaTime * speed;
            if (canDash == true) {
                if (Input.GetKey ("left shift")) {
                    canDash = false;
                    rigidbodyCharacter.AddForce (Vector3.left * speed * 2f, ForceMode2D.Impulse);
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

        if (prevPos.y > playerPos.y) {
            if (jumpCount < 2) {
                canJump = true;
            }
        }

        if (rescale == true) {
            gameObject.transform.localScale = ogSize;
        }

        if (canDash == false) {

            if (coolDown >= 0f) {
                coolDown -= Time.deltaTime;
                dashCD.transform.localScale -= new Vector3 (0f, dashCD.transform.localScale.y / coolDown * Time.deltaTime, 0f);
                dashCD.transform.position = dashImagePos - new Vector3 (0f, dashCD.transform.localScale.y / coolDown * Time.deltaTime, 0f);
                textoCD.text = coolDown.ToString (".00") + " s";
            } else {
                coolDown = 5f;
                dashCD.transform.localScale = Vector3.one * .5f;
                dashCD.transform.position = dashImagePos;
                textoCD.text = " ";
                canDash = true;
            }
        }

    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Floor") {
            jumpCount = 0;
            canJump = true;
        }
    }
}
