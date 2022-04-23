using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float WalkSpeed = 5;
    [SerializeField]
    private float runSpeed = 10;
    [SerializeField]
    private float JumpForce = 5;

    //Components

    private Rigidbody rigidbody;
    public GameObject followTarget;

    Vector2 inputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;
    public Vector2 lookInput = Vector2.zero;
    public float AimSensetivity = 1;

    private Animator PlayerAnimator;
    public AudioSource audio;

    public bool isJumping;
    public bool isMoving;
    public bool isDead = false;
    public bool hasReached = false;

    public readonly int isMovingHash = Animator.StringToHash("IsMoving");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");

    public CheckHug LeftHand;
    public CheckHug rightHand;

    public GameObject endGamePanel;
    public TextMeshProUGUI endgame;
    public TimerCountDown timer;
    private void Awake()
    {


        rigidbody = GetComponent<Rigidbody>();
        PlayerAnimator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    void Start()
    {
    }

    void Update()
    {

        // if (playerController.isJumping) return;
        if (!(inputVector.magnitude > 0))
        {
            MoveDirection = Vector3.zero;
            isMoving = false;
        }
        else
            isMoving = true;
        if (!isDead || hasReached)
        {
            MoveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
            float currentSpeed = WalkSpeed;
            Vector3 MovementDirection = MoveDirection * currentSpeed * Time.deltaTime;
            transform.position += MovementDirection;

            //Aiming/Looking
            followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.x * AimSensetivity, Vector3.up);
            followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.y * AimSensetivity, Vector3.left);
            var angles = followTarget.transform.localEulerAngles;
            angles.z = 0;
            var angle = followTarget.transform.localEulerAngles.x;
            if (angle > 180 && angle < 360)
            {
                angles.x = 360;
            }
            else if (angle < 180 && angle > 20)
            {
                angles.x = 20;
            }
            followTarget.transform.localEulerAngles = angles;

            //player rotation
            transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
            followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            PlayerAnimator.SetBool(isJumpingHash, isJumping);

            PlayerAnimator.SetBool(isMovingHash, isMoving);
        }

        if (LeftHand.collided || rightHand.collided)
        {
            if (!hasReached)
            {
                audio.Play();
                hasReached = true;
                PlayerAnimator.SetBool("IsDancing", true);
                endGamePanel.SetActive(true);
                endgame.SetText("You found your friend");
            }
        }
    }


    public void OnMovement(InputValue value)
    {
      
        inputVector = value.Get<Vector2>();

    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
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
		if (collision.gameObject.CompareTag("Obstacle"))
		{
			isDead = true;
            PlayerAnimator.SetBool("IsDead", isDead);
            timer.timer = 0;
        }
	}
}
