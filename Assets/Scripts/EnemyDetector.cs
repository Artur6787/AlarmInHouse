using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public delegate void EnemyDetectedHandler();
    public event EnemyDetectedHandler OnEnemyEntered;
    public event EnemyDetectedHandler OnEnemyExited;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            OnEnemyEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            OnEnemyExited?.Invoke();
        }
    }
}