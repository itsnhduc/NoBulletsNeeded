using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltDisplay : MonoBehaviour
{

    public GameObject ultTick;
    public GameObject ultChargeMeter;
    public GameObject character;

    void Update()
    {
        float ultCharge = character.GetComponent<Hero>().GetUltCharge();
		ultChargeMeter.GetComponent<Slider>().value = ultCharge;
		ultTick.GetComponent<Image>().enabled = ultCharge >= 100f;
    }
}
