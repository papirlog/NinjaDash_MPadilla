using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterController : MonoBehaviour
{
    // Definiendo variables
    [Header("Constantes lógicas")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float fallMultiplier; //multiplicador de caida
    private bool isGrounded = true;

    [Header("Jump")]
    [SerializeField] InputActionReference jump;
    [SerializeField] private float jumpForce;
    [SerializeField] private float extraJumpForce;
    [SerializeField] private float eventWallJumpForce; //evento de cambio de direccion
    [SerializeField] private GameObject extraJumpParticles;
    private bool hasExtraJump = false;

    [Header("Parry")]
    [SerializeField] InputActionReference parry;
    [SerializeField] private float parryDuration;
    [SerializeField] private GameObject parryBarrier; //objeto que actua como trigger para bloquear las flechas
    private bool isParrying = false;

    [Header("Glide")]
    [SerializeField] private float glideLiftForce; //fuerza con la que asciende al glidear
    [SerializeField] private float maxDescentSpeed; //fuerza maxima a la que se desciende en este estado
    [SerializeField] private GameObject characterGlider;
    private float initialHeight;
    private bool isAscending = false;
    private bool especialStateGliding = false;
    private float activeRocket = 0; //combustible del ala delta

    [Header("Otros")]
    [SerializeField] Transform cameraGameObject;
    private VirtualCamerasScript virtualcamerascript;
    private Rigidbody rb;
    private Animator animator;
    private CapsuleCollider characterCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        virtualcamerascript = cameraGameObject.GetComponent<VirtualCamerasScript>();
        characterCollider = GetComponent<CapsuleCollider>();
    }

    private void OnEnable()
    {
        jump.action.performed += OnJump;
        parry.action.performed += OnParry;

        jump.action.Enable();
        parry.action.Enable();
    }

    private void OnDisable()
    {
        jump.action.performed -= OnJump;
        parry.action.performed -= OnParry;

        jump.action.Disable();
        parry.action.Disable();
    }

    private void FixedUpdate()
    {
        if(characterRespawn.isAlive)
        {
            HandleMovement();
            HandleFallAndGlide();
        }
    }

    private void HandleMovement()
    {
        rb.velocity = new Vector3(movementSpeed, rb.velocity.y, rb.velocity.z);
    }

    private void HandleFallAndGlide()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;

            if(especialStateGliding)
            {
                //ascender con un maximo cuando no tiene combustible
                if (jump.action.IsPressed() && !isAscending && activeRocket == 0)
                {
                    float glideAscendLimit = initialHeight - (rb.velocity.y * Time.fixedDeltaTime);
                    if(transform.position.y <= glideAscendLimit)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, glideLiftForce * (Time.deltaTime * 10), rb.velocity.z);
                        isAscending = true;
                    }
                }
                //este para cuando tiene combustible
                else if(jump.action.IsPressed() && !isAscending && activeRocket > 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, glideLiftForce, rb.velocity.z);
                    isAscending = true;
                    activeRocket -= 2.5f;
                }
                //descenso
                else
                {
                    isAscending = false;
                    rb.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;

                    if(rb.velocity.y < -maxDescentSpeed)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, -maxDescentSpeed, rb.velocity.z);
                    }
                }
            }
            //estado de correr para setupear animaciones
            else if(rb.velocity.y < -0.197)
            {
                animator.SetBool("Jumping", false);
                animator.SetBool("extraJumping", false);
                animator.SetBool("Falling", true);
            }
        }
        else if(especialStateGliding)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }

    // Entrar en el estado de parry
    private void OnParry(InputAction.CallbackContext context)
    {
        if (!isParrying && characterRespawn.isAlive && !especialStateGliding)
        {
            isParrying = true;
            animator.SetBool(isGrounded ? "groundParry" : "airParry", true);
            parryBarrier.SetActive(true);
            StartCoroutine(ParryTimerCount(parryDuration));
        }
    }

    // Para desactivar el estado de parry
    private IEnumerator ParryTimerCount(float duration)
    {
        yield return new WaitForSeconds(duration);
        isParrying = false;
        parryBarrier.SetActive(false);
        animator.SetBool("groundParry", false);
        animator.SetBool("airParry", false);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(isGrounded && characterRespawn.isAlive)
        {
            CharacterJump();
        }
        else if(!isGrounded && hasExtraJump)
        {
            PerformExtraJump();
            extraJumpParticles.SetActive(true);
        }
    }

    // Función para saltar
    private void CharacterJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        isGrounded = false;
        animator.SetBool("Jumping", true);
    }

    // Salto extra
    public void PerformExtraJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, extraJumpForce, rb.velocity.z);
        hasExtraJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            // Para verificar desde qué lado está colisionando
            Vector3 normal = collision.contacts[0].normal;

            if(normal.y > 0.5f)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                isGrounded = true;
                animator.SetBool("Falling", false);
                extraJumpParticles.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If para comparar tag con el gameobject eventWall
        if(other.CompareTag("eventWall"))
        {
            // Y este para hacer la animación del eventWall más rotar el personaje
            animator.SetTrigger("eventWall");
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            transform.rotation *= rotation;
            rb.velocity = new Vector3(rb.velocity.x, eventWallJumpForce, rb.velocity.z);
            movementSpeed = -movementSpeed;
            virtualcamerascript.changeVirtualCamera();
        }

        // Detecta para entrar en el modo especial
        if(other.CompareTag("Glider"))
        {
            initialHeight = transform.position.y;
            animator.SetBool("Gliding", true);
            characterGlider.SetActive(true);
            especialStateGliding = true;
            characterCollider.enabled = false;
            other.gameObject.SetActive(false);
        }
        else if(other.CompareTag("FiGlider"))
        {
            animator.SetBool("Gliding", false);
            characterGlider.SetActive(false);
            especialStateGliding = false;
            characterCollider.enabled = true;
            activeRocket = 0;
        }

        if(other.CompareTag("Rocket"))
        {
            activeRocket = 100;
            other.gameObject.SetActive(false);
        }
    }

    // Activa el salto extra
    public void EnableExtraJump()
    {
        hasExtraJump = true;
    }
}
