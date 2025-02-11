using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    private float _maxVolume = 1f;
    private float _volumeSpeed = 0.5f;
    private float _targetVolume = 0f;

    private void Update()
    {
        _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _targetVolume, _volumeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ThiefMarker>() != null)
        {
            _targetVolume = _maxVolume;

            if (!_alarmSound.isPlaying)
            {
                _alarmSound.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ThiefMarker>() != null)
        {
            _targetVolume = 0f;
        }
    }
}
