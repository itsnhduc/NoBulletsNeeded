using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public GameObject hpCurrent;
    public GameObject hpMax;
    public GameObject hpBar;

    private GameObject _hero;

    public void SetHero(GameObject character)
    {
        _hero = character;
        hpMax.GetComponent<Text>().text = _hero.GetComponent<Mortality>().maxHealth.ToString();
    }

    void Update()
    {
        if (_hero)
        {
            int curHealth = _hero.GetComponent<Mortality>().health;
            hpCurrent.GetComponent<Text>().text = curHealth.ToString();
            hpBar.GetComponent<Slider>().value = curHealth;
        }
    }
}
