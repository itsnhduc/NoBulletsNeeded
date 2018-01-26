using System;
using UnityEngine;

public class HealthExchange
{
    public DateTime timestamp { get; set; }
    public GameObject dealer { get; set; }
    public GameObject receiver { get; set; }
    public int offset { get; set; }
    public bool isFinalBlow { get; set; }
    public bool isSelf { get; set; }
}