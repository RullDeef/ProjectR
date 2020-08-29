using UnityEngine;

namespace Core.RT
{
    public class RTCameraController : MonoBehaviour
    {
        public Transform followingObject;
        public float smoothness = 0.2f;

        public float observingAngle = 45.0f; // degrees
        public float observingDistance = 10.0f;

        public float observingYaw = 0.0f;

        public float rotationSpeed = 30.0f; // degrees / sec
        public float zoomSpeed = 4.0f;


        private void FixedUpdate()
        {
            if (GameManager.IsPaused())
                return;
            
            if (followingObject != null)
            {
                UpdateCameraPosition();
                UpdateCameraRotation();
                UpdateCameraZoom();
            }
        }

        private void UpdateCameraRotation()
        {
            if (Input.GetMouseButton(1))
            {
                float dx = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
                transform.RotateAround(followingObject.position, Vector3.up, dx);
                observingYaw += dx;

                float dy = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;
                observingAngle -= dy;
                observingAngle = Mathf.Clamp(observingAngle, 15.0f, 75.0f);
            }

            transform.LookAt(followingObject);
        }

        private void UpdateCameraPosition()
        {
            float angle = observingAngle * Mathf.Deg2Rad;

            float backStepSize = observingDistance * Mathf.Cos(angle);
            float upStepSize = observingDistance * Mathf.Sin(angle);

            Vector3 backStep = backStepSize * followingObject.TransformDirection(Vector3.back);
            backStep = Quaternion.AngleAxis(observingYaw, Vector3.up) * backStep;
            Vector3 upStep = upStepSize * Vector3.up;

            Vector3 desiredCameraPosition = followingObject.position + backStep + upStep;

            Vector3 delta = desiredCameraPosition - transform.position;

            transform.position += 1.0f / smoothness * delta * Time.fixedDeltaTime;
        }

        private void UpdateCameraZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            observingDistance -= zoomSpeed * scroll;
            observingDistance = Mathf.Max(2.0f, observingDistance);
        }
    }
}