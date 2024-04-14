using PlayerController;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip walkingSound;
    public AudioClip runningSound;
    public AudioClip jumpingSound;

    [Header("Sound Source")]
    public AudioSource audioSource;

    private PlayerInputs playerInputs;

    private void Awake()
    {
        playerInputs = FindAnyObjectByType<PlayerInputs>();
    }

    private void Update()
    {

        if(playerInputs.jump == true)
        {
            PlaySound(jumpingSound);
        }
        else if (playerInputs.move != Vector2.zero && !audioSource.isPlaying)
        {
            if (playerInputs.sprint == true)
            {
                PlaySound(runningSound);
            }

            PlaySound(walkingSound);
        }

    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
