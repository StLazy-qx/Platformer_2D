using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;

    private Vector3 _currentPosition;

    private void Start()
    {
        _currentPosition = _offset;
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        _currentPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _currentPosition, _smothSpeed * Time.deltaTime);
    }
}
