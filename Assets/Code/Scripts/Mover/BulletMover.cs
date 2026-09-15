using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private void Update()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
    }
}