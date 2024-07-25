using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MusicTrigger : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public float fadeDuration = 2.0f;  // Müzik azalýrken geçen süre

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!audioSource1.isPlaying)
                audioSource1.Play();
            if (!audioSource2.isPlaying)
                audioSource2.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOut(audioSource1, fadeDuration));
            StartCoroutine(FadeOut(audioSource2, fadeDuration));
        }
    }

    private IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;  // Ses seviyesini eski haline getir
    }
}
