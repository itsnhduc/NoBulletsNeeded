using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Mortality mort = other.GetComponent<Mortality>();
        if (mort)
        {
            mort.Kill(gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
