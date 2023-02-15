using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;

    [SerializeField] private float _movementSmoothing = 0.5f;
    // [SerializeField] private Animator _pointLight2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SmoothFollowTarget();
    }

    private void SmoothFollowTarget()
    {
        if (transform.position != _followTarget.position)
        {
            Vector3 targetPosition = new Vector3(_followTarget.position.x, _followTarget.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, _movementSmoothing);
        }
    }
}
