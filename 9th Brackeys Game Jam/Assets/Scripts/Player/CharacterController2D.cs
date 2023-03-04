using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float ExternalMoveSpeed { get; set; } = 10f;
    private Vector2 _moveDirection;
    private Rigidbody2D _rigidBody2D;   // Set Interpolate to "Interpolate"

    [SerializeField] private List<AnimationClip> animationClips;
    public AnimationClip Anim_FaceUp { get { return animationClips[0]; } }
    public AnimationClip Anim_FaceDown { get { return animationClips[1]; } }
    public AnimationClip Anim_HurtDown { get { return animationClips[2]; } }

    private bool enableMovement = true;
    
    private Player _player;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _player.OnBeginKnockback += () => enableMovement = false;
        _player.OnDoneKnockback += () => enableMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        if (enableMovement) MoveCharacter();
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
        _rigidBody2D.velocity = _moveDirection * ExternalMoveSpeed;
    }
}
