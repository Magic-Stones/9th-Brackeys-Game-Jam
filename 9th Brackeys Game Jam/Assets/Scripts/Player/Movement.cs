using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerStatus status;
    private Vector2 _moveDirection;
    private Rigidbody2D _rb2D;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void ProcessInput()
    {
        float xMove = 0f, yMove = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            yMove += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xMove -= 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yMove -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xMove += 1f;
        }

        _moveDirection = new Vector2(xMove, yMove).normalized;
    }

    private void MoveCharacter()
    {
        _rb2D.velocity = _moveDirection * status.GetMoveSpeed;
    }
}
