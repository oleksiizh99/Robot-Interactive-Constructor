using UnityEngine;
public class PartSelector
{
    private readonly RobotPartData[] _parts;

    public int CurrentIndex { get; private set; }
    public Color CurrentColor { get; private set; } = Color.white;

    public RobotPartData CurrentPart => _parts[CurrentIndex];

    public PartSelector(RobotPartData[] parts)
    {
        _parts = parts;
    }

    public void Next()
    {
        CurrentIndex = (CurrentIndex + 1) % _parts.Length;
    }

    public void Previous()
    {
        CurrentIndex = (CurrentIndex - 1 + _parts.Length) % _parts.Length;
    }

    public void SetColor(Color color) => CurrentColor = color;
}