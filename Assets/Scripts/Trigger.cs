using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public event UnityAction<bool> EntryHouse;
    private bool _isEntrance = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Way>(out Way Way))
        {
            EntryHouse.Invoke(_isEntrance);
            _isEntrance = !_isEntrance;
        }
    }
}
