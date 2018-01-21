using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    public float groundMoveSpeed;
    public float airMoveSpeed;
    public float jumpMultiplier;
    private bool _isGrounded = false;
    private bool _isSideCollision = false;
    private bool _hasJumped = false;
    private Rigidbody2D _rb;
    private BoxCollider2D _coll;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate() {
        bool jumpKey = Input.GetKey(KeyCode.Space);
        bool leftKey = Input.GetKey(KeyCode.A);
        bool rightKey = Input.GetKey(KeyCode.D);

        if (jumpKey && _isGrounded && !_hasJumped) {
            _hasJumped = true;
            _rb.AddForce(Vector2.up * jumpMultiplier);
        }

        if ((leftKey || rightKey) && !_isSideCollision) {
            int dirSign = leftKey ? -1 : 1;
            float moveSpeed = _isGrounded ? groundMoveSpeed : airMoveSpeed;
            _rb.velocity = new Vector2(moveSpeed * dirSign, _rb.velocity.y);

            Quaternion curRotation = _rb.transform.rotation;
            int yRotation = leftKey ? 180 : 0;
            _rb.transform.rotation = Quaternion.Euler(curRotation.x, yRotation, curRotation.z);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        _isSideCollision = _rb.position.y - other.contacts[0].point.y < 0.6f;
        if (!_isSideCollision && other.collider.tag == "Ground")
        {
            _isGrounded = true;
            _hasJumped = false;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Ground" && _rb.velocity.y > 0.001f) {
            _isSideCollision = false;
            _isGrounded = false;
        }
    }
}
