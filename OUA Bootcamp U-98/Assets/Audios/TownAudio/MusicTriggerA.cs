using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerA : MonoBehaviour
{
    public AudioSource audioSource;  // Tek bir AudioSource
    public float fadeDuration = 2.0f;  // Müzik azalýrken geçen süre

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOut(audioSource, fadeDuration));
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
