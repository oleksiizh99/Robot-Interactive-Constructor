using System;
using System.Collections.Generic;
using UnityEngine;

public class RobotBuilder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RobotSpawner spawner;
    [SerializeField] private RobotUI robotUI;

    [Header("Points")]
    [SerializeField] private Transform headPoint;
    [SerializeField] private Transform torsoPoint;
    [SerializeField] private Transform legsPoint;

    [Header("Parts")]
    [SerializeField] private RobotPartData[] heads;
    [SerializeField] private RobotPartData[] torsos;
    [SerializeField] private RobotPartData[] legs;

    private Dictionary<RobotPartType, PartSelector> _selectors;
    private Dictionary<RobotPartType, GameObject> _currentParts;
    private Dictionary<RobotPartType, Transform> _points;

    public RobotStats CurrentStats { get; private set; }
    public event Action<RobotStats> StatsChanged;
    private void Start()
    {
        _selectors = new Dictionary<RobotPartType, PartSelector>
        {
            { RobotPartType.Head, new PartSelector(heads) },
            { RobotPartType.Torso, new PartSelector(torsos) },
            { RobotPartType.Legs, new PartSelector(legs) }
        };

        _currentParts = new Dictionary<RobotPartType, GameObject>();

        _points = new Dictionary<RobotPartType, Transform>
        {
            { RobotPartType.Head, headPoint },
            { RobotPartType.Torso, torsoPoint },
            { RobotPartType.Legs, legsPoint }
        };

        BuildRobot();
    }

    private void OnEnable()
    {
        robotUI.NextHeadRequested += NextHead;
        robotUI.PreviousHeadRequested += PreviousHead;
        robotUI.NextTorsoRequested += NextTorso;
        robotUI.PreviousTorsoRequested += PreviousTorso;
        robotUI.NextLegsRequested += NextLegs;
        robotUI.PreviousLegsRequested += PreviousLegs;
    }

    private void OnDisable()
    {
        robotUI.NextHeadRequested -= NextHead;
        robotUI.PreviousHeadRequested -= PreviousHead;
        robotUI.NextTorsoRequested -= NextTorso;
        robotUI.PreviousTorsoRequested -= PreviousTorso;
        robotUI.NextLegsRequested -= NextLegs;
        robotUI.PreviousLegsRequested -= PreviousLegs;
    }

    private void BuildRobot()
    {
        Replace(RobotPartType.Head);
        Replace(RobotPartType.Torso);
        Replace(RobotPartType.Legs);

        UpdateStats();
    }

    private void Replace(RobotPartType type)
    {
        var selector = _selectors[type];

        _currentParts[type] = spawner.ReplacePart(
            _currentParts.ContainsKey(type) ? _currentParts[type] : null,
            selector.CurrentPart,
            _points[type]);

        RobotColorApplier.Apply(_currentParts[type], selector.CurrentColor);
    }

    public void SetPartColor(RobotPartType part, Color color)
    {
        _selectors[part].SetColor(color);

        if (_currentParts.ContainsKey(part))
            RobotColorApplier.Apply(_currentParts[part], color);
    }

    private void NextPart(RobotPartType type)
    {
        _selectors[type].Next();
        Replace(type);
        UpdateStats();
    }

    private void PreviousPart(RobotPartType type)
    {
        _selectors[type].Previous();
        Replace(type);
        UpdateStats();
    }

    private void NextHead() => NextPart(RobotPartType.Head);
    private void PreviousHead() => PreviousPart(RobotPartType.Head);

    private void NextTorso() => NextPart(RobotPartType.Torso);
    private void PreviousTorso() => PreviousPart(RobotPartType.Torso);

    private void NextLegs() => NextPart(RobotPartType.Legs);
    private void PreviousLegs() => PreviousPart(RobotPartType.Legs);

    private void UpdateStats()
    {
        CurrentStats = RobotStatsCalculator.Calculate(
            _selectors[RobotPartType.Head].CurrentPart,
            _selectors[RobotPartType.Torso].CurrentPart,
            _selectors[RobotPartType.Legs].CurrentPart
        );

        StatsChanged?.Invoke(CurrentStats);
    }
}