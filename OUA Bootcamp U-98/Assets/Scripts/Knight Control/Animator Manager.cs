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

    public void UpdateAnimatorValues(float horizontalMove, float verticalMove)
    {
        //animation snapping
        float snappedHorizontal;
        float snappedVertical;

        #region Snapped Horizontal
        if (horizontalMove > 0.1f && horizontalMove < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if(horizontalMove > 0.55f)
        {
            snappedHorizontal = 1f;
        }
        else if (horizontalMove < -0.1f && horizontalMove > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if(horizontalMove < -0.55f)
        {
            snappedHorizontal = -1f;
        }
        else
        {
            snappedHorizontal= 0f;
        }
        #endregion

        #region Snapped Vertical
        if (verticalMove > 0.1f && verticalMove < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMove > 0.55f)
        {
            snappedVertical = 1f;
        }
        else if (verticalMove < -0.1f && verticalMove > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMove < -0.55f)
        {
            snappedVertical = -1f;
        }
        else
        {
            snappedVertical = 0f;
        }
        #endregion


        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        if (playerManager.isUsingRootMotion)
        {
            charController.playerRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0f;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            charController.playerRigidbody.velocity = velocity;
        }
    }
}
