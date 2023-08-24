using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
    public class CameraHandler : MonoBehaviour
    {

        public Transform targetTransform; // The target transform the camera will go to
        public Transform cameraTransform; // The transform of the actual camera
        public Transform cameraPivotTransform; // The transform of the camera rotation, turn on a swivel
        private Transform myTransform; // The transform the THIS game object
        private Vector3 cameraTransformPosition; // Vector position of the camera transform
        private LayerMask ignoreLayers; // Will be covered next lesson

        public static CameraHandler singleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35;
        public float maximumPivot = 35;


        private void Awake()
        {
            singleton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.x;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }

        // Called every frame, so the camera follows the player
        // targetTransform variable is just the player transform
        public void FollowTarget(float delta)
        {
            Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed);
            myTransform.position = targetPosition;
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;

        }
    }
}
