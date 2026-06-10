using UnityEngine;

[CreateAssetMenu(menuName = "Robot/Part")]
public class RobotPartData : ScriptableObject
{
   public RobotPartType partType;
   
   public GameObject Prefab;
   
   public int Weight;
   public int Power;
}
