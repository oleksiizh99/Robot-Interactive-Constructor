using UnityEngine;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private RobotBuilder robotBuilder;
    [SerializeField] private RobotPartType targetPart;
    [SerializeField] private Color color;

    public void OnClick()
    {
        robotBuilder.SetPartColor(targetPart, color);
    }
}