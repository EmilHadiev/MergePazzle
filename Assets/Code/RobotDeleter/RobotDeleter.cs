using System;
using UnityEngine;

public class RobotDeleter : MonoBehaviour, IRobotDeleter
{
    public event Action Deleting;

    public void Delete()
    {
        Deleting?.Invoke();
        Destroy(gameObject);
    }
}