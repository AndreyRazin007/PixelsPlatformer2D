using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _SFXSource;

    [Header("AudioClip")]
    public AudioClip _background;
    public AudioClip _checkPoint;
    public AudioClip _inPortal;
    public AudioClip _outPortal;

    private void Start()
    {
        _musicSource.clip = _background;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        _SFXSource.PlayOneShot(clip);
    }
}
