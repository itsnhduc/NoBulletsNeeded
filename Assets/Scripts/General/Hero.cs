using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    // INSPECTOR ATTRIBUTES
    [Header("Movements")]
    public float groundMoveSpeed;
    public float airMoveSpeed;
    public float jumpMultiplier;
    [Header("Abilities")]
    public float ability1Cooldown;
    public float ability2Cooldown;
    public float passiveUltGen;

    // SHARED ATTRIBUTES
    // Identity
    protected Rigidbody2D rb;

    // INTERNAL ATTRIBUTES
    // Identity
    private int _playerNumber = 0;
    private PlayerInput _input;
    // Movements
    private bool _isControllable = true;
    private readonly List<string> _jumpable = new List<string> { "Ground" };
    private bool _isGrounded = false;
    private bool _hasJumped = false;
    // Abilities
    private float _ultCharge = 0;
    private bool[] _onCooldown = { false, false }; // { orb, pulse }

    // ABSTRACT HERO-BASED METHODS
    // Abilities
    protected abstract IEnumerator Ability1(bool onCooldown, Action callback);
    protected abstract IEnumerator Ability2(bool onCooldown, Action callback);
    protected abstract IEnumerator Ultimate(float ultCharge, Action callback);

    // MONOBEHAVIOR METHODS
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("_StartPassiveUltGen");
    }
    void Update()
    {
        if (_isControllable)
        {
            // Movements input
            bool jumpKey = Input.GetKey(_input.up);
            bool leftKey = Input.GetKey(_input.left);
            bool rightKey = Input.GetKey(_input.right);

            if (jumpKey) _BaseJump();
            if (leftKey || rightKey) _BaseMove(leftKey);

            // Abilities input
            bool firstAbilityKey = Input.GetKeyDown(_input.a);
            bool secondAbilityKey = Input.GetKeyDown(_input.b);
            bool ultimateKey = Input.GetKeyDown(_input.c);

            if (firstAbilityKey) _BaseAbility(0);
            if (secondAbilityKey) _BaseAbility(1);
            if (ultimateKey) _BaseUltimate();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (_jumpable.Contains(other.collider.tag))
        {
            _isGrounded = true;
            _hasJumped = false;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (_jumpable.Contains(other.collider.tag) && Mathf.Abs(rb.velocity.y) > 0.001f)
        {
            _isGrounded = false;
        }
    }

    // INTERNAL COROUTINES
    private IEnumerator _StartCooldown(int ability)
    {
        _onCooldown[ability] = true;
        yield return new WaitForSeconds(ability == 0 ? ability1Cooldown : ability2Cooldown);
        _onCooldown[ability] = false;
    }
    private IEnumerator _StartPassiveUltGen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (_ultCharge <= 100f) _ultCharge += passiveUltGen;
        }
    }

    // BASE METHODS
    // Movements
    private void _BaseMove(bool isLeft)
    {
        int dirSign = isLeft ? -1 : 1;
        float moveSpeed = _isGrounded ? groundMoveSpeed : airMoveSpeed;
        // only controllable while in slow speed
        if (Mathf.Abs(rb.velocity.x) <= moveSpeed)
        {
            rb.velocity = new Vector2(dirSign * moveSpeed, rb.velocity.y);
        }

        Quaternion curRotation = rb.transform.rotation;
        int yRotation = isLeft ? 180 : 0;
        rb.transform.rotation = Quaternion.Euler(curRotation.x, yRotation, curRotation.z);
    }
    private void _BaseJump()
    {
        if (_isGrounded && !_hasJumped)
        {
            _hasJumped = true;
            if (Mathf.Abs(rb.velocity.x) > airMoveSpeed)
            {
                rb.velocity = new Vector2(airMoveSpeed * Mathf.Sign(rb.velocity.x), rb.velocity.y);
            }
            rb.AddForce(Vector2.up * jumpMultiplier);
            _isGrounded = false;
        }
    }
    // Abilities
    private void _BaseAbility(int abilityNumber)
    {
        Action callback = () => StartCoroutine(_StartCooldown(abilityNumber));
        if (abilityNumber == 0) StartCoroutine(Ability1(_onCooldown[abilityNumber], callback));
        else StartCoroutine(Ability2(_onCooldown[abilityNumber], callback));
    }
    private void _BaseUltimate()
    {
        if (_ultCharge >= 100)
        {
            StopCoroutine("_StartPassiveUltGen");
            StartCoroutine(Ultimate(_ultCharge, () =>
            {
                _ultCharge = 0;
                StartCoroutine("_StartPassiveUltGen");
            }));
        }
    }

    // SERVICE METHODS
    // Identity
    public void SetPlayer(int playerNumber)
    {
        _playerNumber = playerNumber;
        _input = InputConfig.GetPlayerInput(_playerNumber);
    }
    // Movement
    public void SetControllable(bool controlState)
    {
        _isControllable = controlState;
    }
    // Abilities
    public void GainUltCharge(float amount)
    {
        _ultCharge += amount;
    }
    public float GetUltCharge()
    {
        return _ultCharge;
    }
}