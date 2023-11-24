using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private bool isFacingRight = true;
    public bool _isGrounded;
    private float _groundRadius = 0.2f;
    private float move;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
        }
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, _groundRadius, whatIsGround);

        move = Input.GetAxis("Horizontal");

        _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);

        _animator.SetFloat(AnimatorPlayerController.Params.Speed, Math.Abs(move));

        if (move > 0 && !isFacingRight)
        {
            isFacingRight = true;
            _spriteRenderer.flipX = false;
        }
        else if (move < 0 && isFacingRight)
        {
            isFacingRight = false;
            _spriteRenderer.flipX = true;
        }
    }
}
