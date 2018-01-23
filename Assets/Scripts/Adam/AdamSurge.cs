using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamSurge : MonoBehaviour
{

    public float pullMag;
    public float pushMag;
    public float duration;

    private SpriteRenderer _sr;
    private readonly List<string> _affected = new List<string> { "Hero", "Interactive" };
    private Action _callBack;
    private float _curPullMag = 0;
    private List<GameObject> _stuckObjs = new List<GameObject>();

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (_curPullMag > 0 && _affected.Contains(other.tag))
        {
            Vector2 dir = transform.position - other.transform.position;
            if (!_stuckObjs.Contains(other.gameObject))
            {
                other.transform.position = transform.position - new Vector3(dir.normalized.x, dir.normalized.y, 0);
                _stuckObjs.Add(other.gameObject);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().AddForce(dir * pullMag);
            }
        }
    }

    public void Activate(Action callBack)
    {
        _callBack = callBack;
        StartCoroutine(TimeDetonate(duration));
    }

    IEnumerator TimeDetonate(float duration)
    {
        _curPullMag = pullMag;
        _sr.enabled = true;
        yield return new WaitForSeconds(duration);
        _curPullMag = 0;
        _sr.enabled = false;
        _stuckObjs.ForEach(obj =>
        {
            Vector2 dir = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(dir * pushMag);
        });
        _callBack();
    }
}
