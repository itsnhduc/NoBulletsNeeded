using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Adam : Hero
{
    [Header("Movements")]
    public float groundMoveSpeed;
    public float airMoveSpeed;
    public float jumpMultiplier;
    [Header("Abilities")]
    public float ability1Cooldown;
    public float ability2Cooldown;
    public float pulseMag;

    // Movements
    private Rigidbody2D _rb;
    private readonly List<string> _jumpable = new List<string> { "Ground", "Interactive" };
    private bool _isGrounded = false;
    private bool _hasJumped = false;
    private bool[] _onCooldown = { false, false };


    // Abilities
    private List<GameObject> _inRangePool = new List<GameObject>();
    private GameObject _pulse;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pulse = transform.GetChild(0).gameObject;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interactive" && !_inRangePool.Find(obj => obj.name == other.name))
        {
            _inRangePool.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Interactive")
        {
            _inRangePool = _inRangePool.Where(obj => obj.name != other.name).ToList();
        }
    }

    protected override void Jump()
    {
        if (_isGrounded && !_hasJumped)
        {
            _hasJumped = true;
            _rb.AddForce(Vector2.up * jumpMultiplier);
        }
    }

    protected override void Move(bool isLeft)
    {
        int dirSign = isLeft ? -1 : 1;
        float moveSpeed = _isGrounded ? groundMoveSpeed : airMoveSpeed;
        _rb.velocity = new Vector2(moveSpeed * dirSign, _rb.velocity.y);

        Quaternion curRotation = _rb.transform.rotation;
        int yRotation = isLeft ? 180 : 0;
        _rb.transform.rotation = Quaternion.Euler(curRotation.x, yRotation, curRotation.z);
    }

    protected override void Ability1(bool mouseLeft = false)
    {
        print(_onCooldown[0]);
        if (!_onCooldown[0])
        {
            _inRangePool.ForEach(obj =>
            {
                Vector2 diff = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(diff.normalized * pulseMag);
            });
            _pulse.GetComponent<Flasher>().Flash();
            StartCoroutine(StartCooldown(0));
        }
    }

    protected override void Ability2(bool mouseLeft = false)
    {
        throw new System.NotImplementedException();
    }

    protected override void Ultimate(bool mouseLeft = false)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator StartCooldown(int ability)
    {
        _onCooldown[ability] = true;
        yield return new WaitForSeconds(ability == 0 ? ability1Cooldown : ability2Cooldown);
        _onCooldown[ability] = false;
    }
}
