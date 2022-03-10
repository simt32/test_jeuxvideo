using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    public float speedWalking = 1.5f;
    public float speedRunning = 2.5f;    
    public float jumpHeight = 1f;

    // Private vars
    float speed;
    float speedTarget;

    float groundDistance = 0.25f;
    LayerMask groundLayerMask = 1;
    Vector3 moveDirection;
    Rigidbody rb;
    bool isGrounded;

    float lerpSpeed;

    float vertical, horizontal;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        // Ground check
        // Créer un layer pour le personnage pour qu'il évite de se détecter lui-même
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayerMask, QueryTriggerInteraction.Ignore);

        

        // Inputs
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        // Animations de déplacement ========================
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Vitesse de déplacement et animation
            speedTarget = speedRunning;            
        }
        else
        {
            // Vitesse de déplacement et animation
            speedTarget = speedWalking;
        }

        // Interpolations
        lerpSpeed = Time.deltaTime * 5f;

        speed = Mathf.Lerp(speed, speedTarget, lerpSpeed);
        // ===================================================

        // Déplacements
        moveDirection = transform.forward * vertical;
        moveDirection += transform.right * horizontal;

        // ------------------------------------------------------------

        // Jump -------------------------------------------------------
        if (Input.GetButtonDown("Jump") && isGrounded)
        {            
            Jump();            
        }

        // Respawn ------------------------------------------------
        if (transform.position.y < -15f)
            transform.position = Vector3.zero;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {        
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
