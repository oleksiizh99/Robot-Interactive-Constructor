using UnityEngine;

public class RobotRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    private bool _isRotating;

    private void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        if (_isRotating)
        {
            transform.Rotate(
                Vector3.up * rotationSpeed * Time.deltaTime,
                Space.World);
        }
    }

    public void StartRotation()
    {
        _isRotating = true;
    }

    public void StopRotation()
    {
        _isRotating = false;
    }
}