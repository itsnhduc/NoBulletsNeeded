using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdamAbilities : MonoBehaviour, ICharacterAbilities
{
    // private Rigidbody2D _rb;
    public float pulseMag;
    private List<GameObject> _inRangePool = new List<GameObject>();
    private GameObject _pulse;

    void Start()
    {
        // _rb = GetComponent<Rigidbody2D>();
        _pulse = transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        bool firstAbilityKey = Input.GetKeyDown(KeyCode.E);
        bool secondAbilityKey = Input.GetKeyDown(KeyCode.LeftShift);

        if (firstAbilityKey) this.Ability1();
        if (secondAbilityKey) this.Ability2();
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

    public void Ability1(bool mouseLeft = false)
    {
        _inRangePool.ForEach(obj =>
        {
            Vector2 diff = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(diff.normalized * pulseMag / diff.magnitude);
        });
        _pulse.GetComponent<Flasher>().Flash();
    }

    public void Ability2(bool mouseLeft = false)
    {
        throw new System.NotImplementedException();
    }

    public void Ultimate(bool mouseLeft = false)
    {
        throw new System.NotImplementedException();
    }
}
