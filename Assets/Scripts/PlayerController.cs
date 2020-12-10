using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    public float jumpForce;
    private Vector3 moveDirection;
    public float gravityScale = 5f;
    public CharacterController controller;


    private Camera theCam;
    public GameObject playerModel;
    public float rotateSpeed;
    public Animator anim;

    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;

    public GameObject[] playerPieces;

    public float bounceForce = 8f;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        theCam = Camera.main;
    }

    void Update()
    {
        if (!isKnocking)
        {
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection *= moveSpeed;
            moveDirection.y = yStore;
            if (controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            controller.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                // playerModel.transform.rotation = newRotation;
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }
        if (isKnocking)
        {
            knockbackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;

            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
            }
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            controller.Move(moveDirection * Time.deltaTime);

            if (knockbackCounter <= 0)
            {
                isKnocking = false;
            }
        }
        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim.SetBool("Grounded", controller.isGrounded);
    }

    public void Knockback()
    {
        isKnocking = true;
        knockbackCounter = knockBackLength;
        moveDirection.y = knockbackPower.y;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void Bounce(){
        moveDirection.y = bounceForce;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
