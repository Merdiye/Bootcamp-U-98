using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    public Transform target; // camera to follow
    public Transform cameraPivot; // look up and down pivot
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public float minPivotAngle = -35f;
    private float maxPivotAngle = 35f;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 0.5f;
    public float cameraPivotSpeed = 0.5f;
    public float lookAngle; // left and right angle
    public float pivotAngle; // up and down angle

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        target = FindObjectOfType<PlayerManager>().transform;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }

    private void FollowTarget()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, target.position, ref cameraFollowVelocity , cameraFollowSpeed );
        transform.position = targetPos;
    }

    private void RotateCamera()
    {
        lookAngle += inputManager.cameraInputX * cameraLookSpeed;
        pivotAngle -= inputManager.cameraInputY * cameraPivotSpeed;
        pivotAngle = Mathf.Clamp( pivotAngle , minPivotAngle, maxPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    public void HandleCameraCollisions()
    {

    }
}
