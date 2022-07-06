using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public event UnityAction<bool> OnHouseEntrance;
    private bool _isEntrance = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Walk>(out Walk walk))
        {
            OnHouseEntrance.Invoke(_isEntrance);
            _isEntrance = !_isEntrance;
        }
    }
}
