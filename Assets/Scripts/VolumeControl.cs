using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Set the initial slider value to match the audio source volume
        if (audioSource != null && volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume;
        }

        // Add a listener to handle changes in slider value
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
