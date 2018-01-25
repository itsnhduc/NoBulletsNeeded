using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAbility : MonoBehaviour
{
    public GameObject parentHero { get; private set; }

    public void SetHero(GameObject targetHero)
    {
        parentHero = targetHero;
    }
}
