using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Vector3 playerVelocity;
    private Vector3 moveDirection;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 3.0f;
    private float gravityValue = -9.81f;
    
    [SerializeField] private GameObject boomerangPrefab;
    [SerializeField] private float boomerangSpeed = 5.0f;
    [SerializeField] private float boomerangRadius = 5.0f;
    [SerializeField] private float autoAttackRange = 10.0f;
    [SerializeField] private float autoAttackCooldown = 1.0f;
    private float lastAttackTime;

    [SerializeField]private GameObject panel;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator anim;
    [SerializeField] private InputActionAsset asset;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        panel.gameObject.SetActive(false);
    }

    void Update()
    {
        // Do the Movement
        HandleMovement();
        // Check for auto-attack when enemies are nearby
        CheckForAutoAttack();
    }

    void HandleMovement()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        moveDirection = new Vector3(input.x, 0, input.y);


        controller.Move(moveDirection * (Time.deltaTime * playerSpeed));

        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion toRotation = Quaternion.AngleAxis(targetAngle, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 15f);
        }


        if (moveDirection == Vector3.zero)
        {
            anim.SetFloat("Speed", 0);
        }
        else
        {
            anim.SetFloat("Speed", 1);
        }

        moveDirection *= playerSpeed;


        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -0.5f;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    
    void CheckForAutoAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, autoAttackRange);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") && Time.time - lastAttackTime >= autoAttackCooldown)
            {
                // Trigger auto-attack
                Attack();

                // Update the last attack time
                lastAttackTime = Time.time;
            }
        }
    }

    public void Attack()
    {
        // Instantiate the Boomerang at the player's position
        GameObject boomerang = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);

        // Find all enemies within the specified radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, boomerangRadius);

        // Iterate through the colliders and apply DoTween to the Boomerang
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // Calculate the direction to the enemy
                Vector3 direction = (hitCollider.transform.position - transform.position).normalized;

                // Calculate the destination position for the Boomerang
                Vector3 destination = hitCollider.transform.position;

                // Use DoTween to move the Boomerang along a spline path
                Vector3[] path = new Vector3[]
                {
                    transform.position,
                    transform.position + transform.right * 2.0f, // Modify this point as needed
                    destination
                };

                boomerang.transform.DOLocalPath(path, Vector3.Distance(transform.position, destination) / boomerangSpeed, PathType.CatmullRom)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        // Damage the enemy when the boomerang reaches the destination
                        hitCollider.GetComponent<Health>().TakeDamage(50); // Adjust damage as needed
                    });
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerDeath(); // Call PlayerDeath when colliding with an object with the "Enemy" tag
        }
    }

    public void PlayerDeath()
    {
        panel.gameObject.SetActive(true);
    }

}