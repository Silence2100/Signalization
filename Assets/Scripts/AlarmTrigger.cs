using System;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    public event Action<bool> ThiefStateChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ThiefMarker>(out ThiefMarker thiefMarker) == true)
        {
            ThiefStateChanged?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ThiefMarker>(out ThiefMarker thiefMarker) == true)
        {
            ThiefStateChanged?.Invoke(false);
        }
    }
}
