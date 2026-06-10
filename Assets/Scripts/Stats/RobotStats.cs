using UnityEngine;

public class RobotStats
{
    public int Weight { get; }
    public int Power { get; }

    public RobotStats(int weight, int power)
    {
        Weight = weight;
        Power = power;
    }
}