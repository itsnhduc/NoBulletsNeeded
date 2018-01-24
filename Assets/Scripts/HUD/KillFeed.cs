using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KillFeed : MonoBehaviour
{

    public GameObject killFeedLine;
    public int maxLineCount;
    public float lineDuration;
    private List<HealthExchange> _lines = new List<HealthExchange>();

    void Update()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Hero");
        foreach (GameObject hero in heroes)
        {
            Mortality mort = hero.GetComponent<Mortality>();
            if (mort.health <= 0)
            {
                HealthExchange exchange = mort.GetKiller();
                if (!_lines.Contains(exchange))
                {
                    if (_lines.Count > 0)
                    {
                        // push down
                        foreach (RectTransform line in transform)
                        {
                            line.Translate(0, -1f, 0);
                        }
                        // remove last if necessary
                        if (_lines.Count >= maxLineCount)
                        {
                            _lines = _lines.Skip(1).ToList();
                            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
                        }
                    }
                    _lines.Add(exchange);
                    GameObject curLine = Instantiate(killFeedLine, transform);
                    foreach (RectTransform element in curLine.transform)
                    {
                        if (element.name == "Killer")
                        {
                            if (exchange.dealer.tag == "KillZone")
                            {
                                Destroy(element.gameObject);
                            }
                            else
                            {
                                element.GetComponentInChildren<Text>().text = exchange.dealer.name;
                            }
                        }
                        else if (element.name == "Victim")
                        {
                            element.GetComponentInChildren<Text>().text = exchange.receiver.name;
                        }
                    }
                    StartCoroutine(TimeDestroyLine(curLine));
                }
            }
        }
    }

    IEnumerator TimeDestroyLine(GameObject line)
    {
        yield return new WaitForSeconds(lineDuration);
        Destroy(line);
    }
}
