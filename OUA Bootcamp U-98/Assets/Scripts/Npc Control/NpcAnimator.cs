using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NpcAnimator : MonoBehaviour
{
    public float maxRange = 4f;
    public float minRange = 0f;
    public Animator animator;
    public bool isPunchingBool;
    NpcAI npc;
    NpcHealth npcHealth;
    private void Awake()
    {
        npcHealth = GetComponent<NpcHealth>();
        npc = GetComponent<NpcAI>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (npcHealth.isDead)
            return;
        isPunching();
        isPunchingBool = animator.GetBool("isPunching");
    }

    public void isPunching()
    {
        if (npc.playerHealth.isDead == true)
        {
            animator.SetBool("isPunching", false);
            return;
        }

        Vector3 distanceToWarrior = transform.position - npc._player.position;
        if (minRange <= distanceToWarrior.magnitude && distanceToWarrior.magnitude <= maxRange)
        {
            npc.isRunningAway = false;
            animator.SetBool("isPunching", true);
            npc.LookTarget(npc._player);
        }
        else if (distanceToWarrior.magnitude < minRange)
        {
            npc.isRunningAway = true;
        }
        else
        {
            npc.isRunningAway = false;
            animator.SetBool("isPunching", false);
        }
    }

    public void PlayTargetAnimation(string targetAnim)
    {
        animator.CrossFade(targetAnim, 0.2f);
    }


}
