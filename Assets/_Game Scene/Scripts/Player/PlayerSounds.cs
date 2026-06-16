using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private AudioSource _walkingSound;
    [SerializeField] private AudioSource _interactionSound;

    private void Start() => VolumeUpdate(Settings.Instance.VolumeSounds);

    private void OnEnable()
    {
        _playerMovement.OnWalking += WalkingSound;
        Settings.Instance.UpdateVolume += VolumeUpdate;
        AlarmCanvas.Instance.OnInteraction += InteractionSoundOn;
    }

    private void OnDestroy()
    {
        _playerMovement.OnWalking -= WalkingSound;
        Settings.Instance.UpdateVolume -= VolumeUpdate;
        AlarmCanvas.Instance.OnInteraction -= InteractionSoundOn;
    }

    private void WalkingSound(bool walking)
    {
        if (walking)
        {
            if(_walkingSound.isPlaying == false)
            {
                _walkingSound.Play();
            }
        }
        else
        {
            _walkingSound.Stop();
        }
    }

    private void InteractionSoundOn(bool value) 
    {
        if(value)
        {
            _interactionSound.Play();
        }
        else
        {
            _interactionSound.Stop();
        }
    }

    private void VolumeUpdate(float volume) 
    { 
        _walkingSound.volume = volume / 100; 
        _interactionSound.volume = volume / 100;
    }
}
