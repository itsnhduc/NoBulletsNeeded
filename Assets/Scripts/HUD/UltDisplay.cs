using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltDisplay : MonoBehaviour
{

    public GameObject ultTick;
    public GameObject ultChargeMeter;
    private GameObject _hero;

    public void SetHero(GameObject character)
    {
        _hero = character;
    }

    void Update()
    {
        if (_hero)
        {
            float ultCharge = _hero.GetComponent<Hero>().GetUltCharge();
            ultChargeMeter.GetComponent<Slider>().value = ultCharge;
            ultTick.GetComponent<Image>().enabled = ultCharge >= 100f;
        }
    }
}
