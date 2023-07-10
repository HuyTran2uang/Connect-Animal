using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDuration : MonoBehaviour
{
    [SerializeField] float _duration;

    private void OnEnable()
    {
        Invoke(nameof(Die), _duration);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
