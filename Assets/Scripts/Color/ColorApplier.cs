using UnityEngine;

public static class RobotColorApplier
{
    public static void Apply(GameObject part, Color color)
    {
        if (part == null) return;

        foreach (var renderer in part.GetComponentsInChildren<Renderer>())
            renderer.material.color = color;
    }
}