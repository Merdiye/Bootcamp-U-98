using UnityEngine;

public class FrogController : MonoBehaviour
{
    private Animator animator;
    private float timer;
    public float jumpInterval = 1.0f; // 1 saniyede bir zýplama

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
        timer = jumpInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            animator.SetTrigger("JumpTrigger");
            timer = jumpInterval;
        }
    }
}
