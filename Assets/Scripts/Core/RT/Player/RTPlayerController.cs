using UnityEngine;

namespace Core.RT.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class RTPlayerController : MonoBehaviour
    {
        public float movementSpeed = 5.0f;
        public float jumpForce = 10.0f;
        public RTCameraController cameraController;

        private bool isGrounded = false;

        private Rigidbody body;

        void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (GameManager.IsPaused())
                return;

            float dHor = Input.GetAxis("Horizontal");
            float dVer = Input.GetAxis("Vertical");

            Vector3 delta = new Vector3(dHor, 0, dVer);
            delta = transform.TransformDirection(delta);
            delta = Quaternion.AngleAxis(cameraController.observingYaw, Vector3.up) * delta;
            delta *= movementSpeed;

            delta.y = body.velocity.y;
            body.velocity = delta;

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                body.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
        }

        void OnCollisionEnter(Collision other)
        {
            isGrounded = true;
        }

        void OnCollisionExit(Collision other)
        {
            isGrounded = false;
        }
    }
}
