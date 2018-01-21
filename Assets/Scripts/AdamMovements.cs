using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdamMovements : MonoBehaviour, ICharacterMovements
{
    public float groundMoveSpeed;
    public float airMoveSpeed;
    public float jumpMultiplier;

    private readonly List<string> _jumpable = new List<string> { "Ground", "Interactive" };
    private bool _isGrounded = false;
    private bool _hasJumped = false;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        bool jumpKey = Input.GetKey(KeyCode.Space);
        bool leftKey = Input.GetKey(KeyCode.A);
        bool rightKey = Input.GetKey(KeyCode.D);

        if (jumpKey && _isGrounded && !_hasJumped)
        {
            this.Jump();
        }

        if (leftKey || rightKey)
        {
            this.Move(leftKey);
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
        if (_jumpable.Contains(other.collider.tag) && _rb.velocity.y > 0.001f)
        {
            _isGrounded = false;
        }
    }

    public void Jump()
    {
        _hasJumped = true;
        _rb.AddForce(Vector2.up * jumpMultiplier);
    }

    public void Move(bool isLeft)
    {
        int dirSign = isLeft ? -1 : 1;
        float moveSpeed = _isGrounded ? groundMoveSpeed : airMoveSpeed;
        _rb.velocity = new Vector2(moveSpeed * dirSign, _rb.velocity.y);

        Quaternion curRotation = _rb.transform.rotation;
        int yRotation = isLeft ? 180 : 0;
        _rb.transform.rotation = Quaternion.Euler(curRotation.x, yRotation, curRotation.z);
    }
}
