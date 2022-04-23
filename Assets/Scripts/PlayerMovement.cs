using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float WalkSpeed = 5;
    [SerializeField]
    private float runSpeed = 10;
    [SerializeField]
    private float JumpForce = 5;
    [SerializeField]
    private float rotationSpeed = 50;
    
    public bool isJumping;
    public bool isMoving;
    //Components

    private Rigidbody rigidbody;
    private Animator PlayerAnimator;
    public GameObject followTarget;
    public AudioSource audioSource;
   
    Vector2 inputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;
    public Vector2 lookInput = Vector2.zero;
    public float AimSensetivity = 1;


    private Vector3 targetPosition;
    private Vector3 startingPosition;

    public readonly int isMovingHash = Animator.StringToHash("IsMoving");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");

    public CheckHug LeftHand;
    public CheckHug rightHand;
    public LayerMask playerMask;
    public float distance;
    public PlayerMovement SecondPlayer;
    public bool CantMove = false;
    public bool isDead = false;
    private void Awake()
    {


        rigidbody = GetComponent<Rigidbody>();
        PlayerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
    }

    void Update()
    {
        if (!CantMove )
        {
            transform.position += (MoveDirection) * WalkSpeed * Time.deltaTime;

            PlayerAnimator.SetBool(isJumpingHash, isJumping);

            PlayerAnimator.SetBool(isMovingHash, isMoving);


            if (LeftHand && rightHand)
            {
                if (LeftHand.collided || rightHand.collided)
                {
                    CantMove = true;
                    SecondPlayer.CantMove = true;
                
                }
            }

        }
        if(CantMove && !isDead)
		{
            PlayerAnimator.SetBool("IsDancing", true);

        }
        if (CantMove && isDead)
        {
            PlayerAnimator.SetBool("IsDead", true);

        }
    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        if (!CantMove || !isDead)
        {
            if (inputVector.magnitude > 0)
            {
                if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
                {
                    if (inputVector.x > 0)
                    {
                        MoveDirection = Vector3.right;
                        isMoving = true;

                    }
                    else if (inputVector.x < 0)
                    {
                        MoveDirection = Vector3.left;
                        isMoving = true;

                    }
                }
                else
                {
                    if (inputVector.y < 0)
                    {
                        MoveDirection = Vector3.back;
                        isMoving = true;

                    }
                    else if (inputVector.y > 0)
                    {
                        MoveDirection = Vector3.forward;
                        isMoving = true;

                    }
                }
                Quaternion toRotation = Quaternion.LookRotation(MoveDirection, Vector3.up);
                transform.rotation = toRotation;




            }
            else
            {
                isMoving = false;
                MoveDirection = Vector3.zero;
            }
        }
    }

    public void OnJump(InputValue value)
	{
        if (isJumping) return;
        isJumping = value.isPressed;
        rigidbody.AddForce((transform.up + MoveDirection) * JumpForce, ForceMode.Impulse);

    }



	private void OnCollisionEnter(Collision collision)
	{
        isJumping = false;
        if(collision.gameObject.CompareTag("Obstacle"))
		{
            isDead = true;
            SecondPlayer.isDead = true;
            CantMove = true;
            SecondPlayer.CantMove = true;

        }
    }


    

}
