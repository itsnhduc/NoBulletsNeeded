using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("Heroes")]
    public GameObject Adam;

    private List<string> _spawnedHeroes = new List<string>();

    void Start()
    {
        // dev-only
        SpawnHero(0, Adam, new Vector2(-1, 5));
        SpawnHero(1, Adam, new Vector2(1, 5));
    }

    public void SpawnHero(int playerNumber, GameObject hero, Vector2 position)
    {
        // Spawn prefab
        GameObject curHero = Instantiate(hero, position, new Quaternion());
        curHero.name = curHero.name.Replace("(Clone)", string.Empty);
        curHero.GetComponent<Hero>().SetPlayer(playerNumber);
        curHero.transform.parent = GameObject.Find("Heroes").transform;

        if (!_spawnedHeroes.Contains(curHero.name))
        {
            _spawnedHeroes.Add(curHero.name);
        }
        else
        {
            curHero.name += " v2";
            Color newColor = new Color(
                Random.Range(0, 1f),
                Random.Range(0, 1f),
                Random.Range(0, 1f)
            );
            curHero.GetComponent<SpriteRenderer>().color = newColor;
        }

        // Configure HUD
        GameObject stat = GameObject.Find("Player" + playerNumber + "Stat");
        stat.GetComponent<UltDisplay>().SetHero(curHero);
        stat.GetComponent<HealthDisplay>().SetHero(curHero);
    }
}