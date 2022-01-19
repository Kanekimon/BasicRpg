using Assets.Scripts.Manager;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;

        public float speed = 12f;
        public float sprintMultiplier = 2f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        private Vector3 velocity;
        private bool isGrounded;

        // Update is called once per frame
        void Update()
        {
           
            ApplyPhysics();
            if (!ChatManager.Instance.isChatOpen)
            {
                Move();

                if (Input.GetButtonDown("Jump") && isGrounded)
                    Jump();
            }
        }


        private void Move()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            if (Input.GetKey(KeyCode.LeftShift))
                controller.Move((move * (speed * sprintMultiplier) * Time.deltaTime));
            else
                controller.Move((move * speed * Time.deltaTime));

        }

        private void Jump()
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        private void ApplyPhysics()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
