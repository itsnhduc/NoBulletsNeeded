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
        SpawnHero(0, Adam, Vector2.zero);
        SpawnHero(1, Adam, Vector2.right);
    }

    public void SpawnHero(int playerNumber, GameObject hero, Vector2 position)
    {
		// Spawn prefab
        GameObject curHero = Instantiate(hero, position, new Quaternion());
		curHero.name.Replace("(Clone)", string.Empty);
        curHero.GetComponent<Hero>().SetPlayer(playerNumber);
        curHero.transform.parent = GameObject.Find("Heroes").transform;
		if (!_spawnedHeroes.Contains(curHero.name))
		{
			_spawnedHeroes.Add(curHero.name);
		}
		else
		{
            Color newColor = new Color(
                Random.Range(0, 1f),
                Random.Range(0, 1f),
                Random.Range(0, 1f)
            );
			curHero.GetComponent<SpriteRenderer>().color = newColor;
		}

		// Configure HUD
		GameObject.Find("Player" + playerNumber + "Stat").GetComponent<UltDisplay>().SetHero(curHero);
    }
}