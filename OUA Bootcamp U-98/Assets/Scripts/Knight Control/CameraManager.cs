using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    public Transform target; // camera to follow
    public Transform cameraPivot; // look up and down pivot
    public Transform cameraTransform;// asýl kamera
    public float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public float minimumCollisionOffset = 0.2f;
    public float cameraCollisionRadius = 0.2f;
    public LayerMask collisionLayers;// kameranýn collide edeceði layer
    public float cameraCollisionOffset = 0.4f;//kamera objeye çarptýðýnda sekeceði mesafe

    private float minPivotAngle = -35f;
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
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, target.position, ref cameraFollowVelocity , cameraFollowSpeed );
        transform.position = targetPos;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle += inputManager.cameraInputX * cameraLookSpeed;
        pivotAngle -= inputManager.cameraInputY * cameraPivotSpeed;
        pivotAngle = Mathf.Clamp( pivotAngle , minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    public void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;//neye çarptýðýmýzýn bilgisi
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();
        if(Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- distance - cameraCollisionOffset;
        }

        if(Mathf.Abs(targetPosition) < minimumCollisionOffset) 
        {
            targetPosition -= minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z , targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
