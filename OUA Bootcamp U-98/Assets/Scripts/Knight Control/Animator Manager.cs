using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    int horizontal;
    int vertical;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting) 
    {
        animator.SetBool("isInteracting", isInteracting);
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
}
