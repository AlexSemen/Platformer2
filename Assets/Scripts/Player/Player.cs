using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAttack))]

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private bool _isAttack;

    private bool isFacingRight = true;
    public bool _isGrounded;
    private float _groundRadius = 0.2f;
    private float _move;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _playerMove.Jump();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _animator.SetTrigger(AnimatorPlayer.Triggers.Attack);
            _playerAttack.Attack();
        }
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _groundMask);

        if (_isAttack == false)
        {
            _move = Input.GetAxis("Horizontal");

            _playerMove.Move(_move);

            _animator.SetFloat(AnimatorPlayer.Params.Speed, Math.Abs(_move));

            if (_move > 0 && !isFacingRight)
            {
                isFacingRight = true;
                _spriteRenderer.flipX = false;
                _playerAttack.ChangeDirection();
            }
            else if (_move < 0 && isFacingRight)
            {
                isFacingRight = false;
                _spriteRenderer.flipX = true;
                _playerAttack.ChangeDirection();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Удар");
    }
}
