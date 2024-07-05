using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DemonNPCAnimator : MonoBehaviour
{

    public Animator animator;
    public bool isPunchingBool;
    DemonNPC npc;
    DemonHealth npcHealth;
    private void Awake()
    {
        npcHealth = GetComponent<DemonHealth>();
        npc = GetComponent<DemonNPC>();
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
        Vector3 distanceToWarrior = transform.position - npc._player.position;
        if (distanceToWarrior.magnitude <= 4.0f)
        {
            animator.SetBool("isPunching", true);
            npc.LookTarget(npc._player);
        }
        else
        {
            animator.SetBool("isPunching", false);
        }
    }

    public void PlayTargetAnimation(string targetAnim)
    {
        animator.CrossFade(targetAnim, 0.2f);
    }


}
