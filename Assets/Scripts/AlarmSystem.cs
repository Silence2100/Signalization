using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private AlarmTrigger _alarmTrigger;

    private float _maxVolume = 1f;
    private float _volumeSpeed = 0.5f;
    private float _targetVolume = 0f;
    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        _alarmTrigger.ThiefStateChanged += HandleThiefStateChange;
    }

    private void OnDestroy()
    {
        _alarmTrigger.ThiefStateChanged -= HandleThiefStateChange;
    }

    private void HandleThiefStateChange(bool isInside)
    {
        _targetVolume = isInside ? _maxVolume : 0f;
        StartVolumeChange();
    }

    private void StartVolumeChange()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        if (_targetVolume > 0f && _alarmSound.isPlaying == false)
        {
            _alarmSound.Play();
        }

        _volumeCoroutine = StartCoroutine(ChangeVolume());
    }

    private IEnumerator ChangeVolume()
    {
        while (Mathf.Approximately(_alarmSound.volume, _targetVolume) == false)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _targetVolume, _volumeSpeed * Time.deltaTime);
            yield return null;
        }

        if (_alarmSound.volume == 0f)
        {
            _alarmSound.Stop();
        }
    }
}