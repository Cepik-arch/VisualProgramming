using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInputs))]

    public class PlayerController : MonoBehaviour
    {

        [Header("Player")]
        public float moveSpeed = 4.0f;
        public float sprintSpeed = 6.0f;
        public float rotationSpeed = 1.0f;
        public float speedChangeRate = 10.0f;

        private float speed;
        private float rotationVelocity;
        private float verticalVelocity;
        private float terminalVelocity = 53.0f;

        public float gravity = -15.0f;
        public float jumpHeight = 1.2f;

        public float jumpTimeout = 0.1f;
        public float fallTimeout = 0.15f;

        private float jumpTimeoutDelta;
        private float fallTimeoutDelta;

        [Header("Player Grounded")]
        public bool grounded = true;
        public float groundedOffset = -0.14f;
        public float groundedRadius = 0.5f;
        public LayerMask GroundLayers;

        [Header("Camera")]
        public GameObject cameraTarget;
        public float topClamp = 90.0f;
        public float bottomClamp = -90.0f;
        private float targetPitch;

        private CharacterController controller;
        private PlayerInputs input;
        private GameObject mainCamera;

        private const float threshold = 0.01f;

        private void Awake()
        {
            //Get a reference to our main camera
            if (mainCamera == null)
            {
                mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInputs>();

        }

        private void Update()
        {
            JumpAndGravity();
            GroundedCheck();
            Move();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void GroundedCheck()
        {
            //Set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            grounded = Physics.CheckSphere(spherePosition, groundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        }

        private void CameraRotation()
        {
            //If input
            if (input.look.sqrMagnitude >= threshold)
            {
                float deltaTimeMultiplier = 1.0f;

                targetPitch += input.look.y * rotationSpeed * deltaTimeMultiplier;
                rotationVelocity = input.look.x * rotationSpeed * deltaTimeMultiplier;

                //Clamp pitch rotation
                targetPitch = ClampAngle(targetPitch, bottomClamp, topClamp);

                //Update Cinemachine camera target pitch
                cameraTarget.transform.localRotation = Quaternion.Euler(targetPitch, 0.0f, 0.0f);

                //Rotate the player
                transform.Rotate(Vector3.up * rotationVelocity);
            }
        }

        private void Move()
        {
            //Set target speed
            float targetSpeed = input.sprint ? sprintSpeed : moveSpeed;

            if (input.move == Vector2.zero) targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;
            float speedOffset = 0.1f;
            float inputMagnitude = 1f;

            //Sccelerate or decelerate
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);
                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetSpeed;
            }

            Vector3 inputDirection = new Vector3(input.move.x, 0.0f, input.move.y).normalized;

            //If there is a move input rotate player when the player is moving
            if (input.move != Vector2.zero)
            {
                inputDirection = transform.right * input.move.x + transform.forward * input.move.y;
            }

            //Move the player
            controller.Move(inputDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private void JumpAndGravity()
        {
            if (grounded)
            {
                //Reset the fall timeout timer
                fallTimeoutDelta = fallTimeout;

                //Stop velocity dropping
                if (verticalVelocity < 0.0f)
                {
                    verticalVelocity = -2f;
                }

                //Jump
                if (input.jump && jumpTimeoutDelta <= 0.0f)
                {
                    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                if (jumpTimeoutDelta >= 0.0f)
                {
                    jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                jumpTimeoutDelta = jumpTimeout;

                //Fall timeout
                if (fallTimeoutDelta >= 0.0f)
                {
                    fallTimeoutDelta -= Time.deltaTime;
                }

                //If grounded, dont jump
                input.jump = false;
            }

            //Apply gravity over time
            if (verticalVelocity < terminalVelocity)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
