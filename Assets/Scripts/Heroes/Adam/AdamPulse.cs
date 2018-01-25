using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamPulse : HeroAbility
{

    public float pulseMag;
    public int pulseDamage;

    private Flasher _flasher;
    private readonly List<string> _affected = new List<string> { "Hero", "Interactive" };
    private List<GameObject> _inRangeObjs = new List<GameObject>();

    void Start()
    {
        _flasher = GetComponent<Flasher>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_affected.Contains(other.tag) && !_inRangeObjs.Contains(other.gameObject))
        {
            _inRangeObjs.Add(other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (_affected.Contains(other.tag) && _inRangeObjs.Contains(other.gameObject))
        {
            _inRangeObjs.Remove(other.gameObject);
        }
    }

    public void Activate(GameObject initiator)
    {
        _inRangeObjs.ForEach(obj =>
        {
            Vector2 diff = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(diff.normalized * pulseMag);
            Mortality mort = obj.GetComponent<Mortality>();
            if (mort) mort.AlterHealth(-pulseDamage, initiator);
        });
        _flasher.Flash();
    }
}
