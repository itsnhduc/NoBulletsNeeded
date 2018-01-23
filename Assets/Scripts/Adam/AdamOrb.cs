using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdamOrb : MonoBehaviour
{
    public float pullMag;
    private readonly List<string> _affected = new List<string> { "Interactive" };
    private List<GameObject> _inRangePool = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_affected.Contains(other.tag) && !_inRangePool.Find(obj => obj.name == other.name))
        {
            _inRangePool.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_affected.Contains(other.tag))
        {
            _inRangePool = _inRangePool.Where(obj => obj.name != other.name).ToList();
        }
    }

    public void Activate()
    {
		_inRangePool.ForEach(obj => {
			Vector2 diff = transform.position - obj.transform.position;
			obj.GetComponent<Rigidbody2D>().AddForce(diff.normalized * pullMag);
		});
		Destroy(gameObject);
    }

}
