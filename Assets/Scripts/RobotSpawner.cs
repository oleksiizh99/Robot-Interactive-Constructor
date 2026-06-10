using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    public GameObject ReplacePart(
        GameObject current,
        RobotPartData data,
        Transform point)
    {
        if (current != null)
            Destroy(current);

        return Instantiate(
            data.Prefab,
            point.position,
            point.rotation,
            point);
    }
}