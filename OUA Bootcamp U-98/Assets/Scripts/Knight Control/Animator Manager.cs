using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    PlayerManager playerManager;
    CharController charController;
    int horizontal;
    int vertical;
    private void Awake()
    {
        charController = GetComponent<CharController>();
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool useRootMotion = false) 
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.SetBool("isUsingRootMotion", useRootMotion);
        animator.CrossFade(targetAnim, 0.2f);
    }

    private float SnapValue(float moveValue)
    {
        if (moveValue > 0.1f && moveValue < 0.55f)
        {
            return 0.5f;
        }
        else if (moveValue > 0.55f)
        {
            return 1f;
        }
        else if (moveValue < -0.1f && moveValue > -0.55f)
        {
            return -0.5f;
        }
        else if (moveValue < -0.55f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    public void UpdateAnimatorValues(float horizontalMove, float verticalMove)
    {
        float snappedHorizontal = SnapValue(horizontalMove);
        float snappedVertical = SnapValue(verticalMove);

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    /*private void OnAnimatorMove()
    {
        if (playerManager.isUsingRootMotion)
        {
            charController.playerRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0f;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            charController.playerRigidbody.velocity = velocity;
        }
    }*/

    private void OnAnimatorMove()
    {
        if (playerManager.isUsingRootMotion && playerManager.isInteracting)
        {
            charController.playerRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0f;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            charController.playerRigidbody.velocity = velocity;
        }
    }

}
