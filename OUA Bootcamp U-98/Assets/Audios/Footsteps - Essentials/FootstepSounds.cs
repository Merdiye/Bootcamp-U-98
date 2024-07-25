using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    [System.Serializable]
    public class MovementSounds
    {
        public AudioClip[] walkClips;
        public AudioClip[] runClips;
        public AudioClip jumpClip;
        public AudioClip landClip;
    }

    public MovementSounds woodSounds;
    public MovementSounds grassSounds;
    public MovementSounds concreteSounds;
    public MovementSounds sandSounds;
    public MovementSounds dirtSounds; 

    public AudioSource audioSource;

    private CharacterController controller;
    private string currentSurface = "";
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 2f && !audioSource.isPlaying && !isJumping)
        {
            PlayFootstepSound();
        }

        if (!controller.isGrounded && !isJumping)
        {
            isJumping = true;
            PlayJumpSound();
        }

        if (controller.isGrounded && isJumping)
        {
            isJumping = false;
            PlayLandSound();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        currentSurface = hit.collider.tag;
    }

    void PlayFootstepSound()
    {
        AudioClip clip = null;
        var movementSounds = GetCurrentMovementSounds();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            clip = movementSounds.runClips[Random.Range(0, movementSounds.runClips.Length)];
        }
        else
        {
            clip = movementSounds.walkClips[Random.Range(0, movementSounds.walkClips.Length)];
        }

        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void PlayJumpSound()
    {
        AudioClip clip = GetCurrentMovementSounds().jumpClip;
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void PlayLandSound()
    {
        AudioClip clip = GetCurrentMovementSounds().landClip;
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    MovementSounds GetCurrentMovementSounds()
    {
        switch (currentSurface)
        {
            case "Wood":
                return woodSounds;
            case "Grass":
                return grassSounds;
            case "Concrete":
                return concreteSounds;
            case "Sand":
                return sandSounds;
            case "Dirt": 
                return dirtSounds;
            default:
                return new MovementSounds();
        }
    }
}
