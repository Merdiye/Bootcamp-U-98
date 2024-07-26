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

    private CharController controller;
    private string currentSurface = "";
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharController>();
        Debug.Log("FootstepSounds script started. CharController: " + (controller != null));
    }

    void Update()
    {
        Debug.Log("Update called. isGrounded: " + controller.isGrounded + ", velocity: " + controller.playerRigidbody.velocity.magnitude);

        if (controller.isGrounded && controller.playerRigidbody.velocity.magnitude > 2f && !audioSource.isPlaying && !isJumping)
        {
            Debug.Log("PlayFootstepSound �a�r�ld�");
            PlayFootstepSound();
        }

        if (!controller.isGrounded && !isJumping)
        {
            isJumping = true;
            Debug.Log("PlayJumpSound �a�r�ld�");
            PlayJumpSound();
        }

        if (controller.isGrounded && isJumping)
        {
            isJumping = false;
            Debug.Log("PlayLandSound �a�r�ld�");
            PlayLandSound();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        currentSurface = collision.collider.tag;
        Debug.Log("�arp��ma oldu: " + currentSurface);

        if (string.IsNullOrEmpty(currentSurface))
        {
            Debug.LogWarning("�arp��ma olan nesnenin etiketi bo�: " + collision.collider.name);
        }
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
            Debug.Log("Footstep ses �al�nd�: " + clip.name);
        }
    }

    void PlayJumpSound()
    {
        AudioClip clip = GetCurrentMovementSounds().jumpClip;
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
            Debug.Log("Jump ses �al�nd�: " + clip.name);
        }
    }

    void PlayLandSound()
    {
        AudioClip clip = GetCurrentMovementSounds().landClip;
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
            Debug.Log("Land ses �al�nd�: " + clip.name);
        }
    }

    MovementSounds GetCurrentMovementSounds()
    {
        Debug.Log("Current surface: " + currentSurface);
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
                Debug.LogWarning("Bilinmeyen zemin t�r�: " + currentSurface);
                return new MovementSounds();
        }
    }
}
