using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    [Header("UI Slide Sound")]
    [SerializeField] private AudioSource _uiAudioSource;
	[SerializeField] private AudioClip _popUpSlideInSound;
	[SerializeField] private AudioClip _popUpSlideOutSound;

    [Header("Keyboard Click Sounds")]
    [SerializeField] private AudioSource _keyboardAudioSource;
	[SerializeField] private float _lowestKeyboardPitch = 0.95f;
	[SerializeField] private float _highestKeyboardPitch = 1.05f;
	[SerializeField] private List<AudioClip> _keyboardClickSounds;
    
    [Header("Mouse Click Sounds")]
    [SerializeField] private AudioSource _mouseAudioSource;
    [SerializeField] private float _lowestMousePitch = 0.95f;
    [SerializeField] private float _highestMousePitch = 1.05f;
    [SerializeField] private List<AudioClip> _mouseClickSounds;

    private void OnEnable()
	{
	    PlayerInput.OnKeyDown += HandleInput;
	}

	public void TriggerSlideInSound()
	{
		_uiAudioSource.PlayOneShot(_popUpSlideInSound, 1);
	}

	public void TriggerSlideOutSound()
	{
		_uiAudioSource.PlayOneShot(_popUpSlideOutSound, 1);
	}

	private void HandleInput(object sender, KeyType keyType)
	{
	    switch (keyType)
	    {
            case KeyType.Backspace:
            case KeyType.KeyboardKey:
                PlayKeyboardSound();
                break;

            case KeyType.MouseClick:
                PlayMouseSound();
                break;
        }
    }

    private void PlayMouseSound()
    {
        int randomClipIndex = Random.Range(0, _mouseClickSounds.Count);
        float randomPitch = Random.Range(_lowestMousePitch, _highestMousePitch);
        float randomVol = Random.Range(0.8f, 1f);

        _mouseAudioSource.pitch = randomPitch;
        _mouseAudioSource.PlayOneShot(_mouseClickSounds[randomClipIndex], randomVol);
    }

    private void PlayKeyboardSound()
    {
        int randomClickIndex = Random.Range(0, _keyboardClickSounds.Count);
        float randomPitch = Random.Range(_lowestKeyboardPitch, _highestKeyboardPitch);
        float randomVol = Random.Range(0.8f, 1f);

        _keyboardAudioSource.pitch = randomPitch;
        _keyboardAudioSource.PlayOneShot(_keyboardClickSounds[randomClickIndex], randomVol);
    }

    private void OnDisable()
    {
        PlayerInput.OnKeyDown -= HandleInput;
    }
}
