using System;
using TMPro;
using UnityEngine;

public class RobotUI : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private TMP_Text weightText;
    [SerializeField] private TMP_Text powerText;
    
    [SerializeField] private RobotBuilder robotBuilder;

    public event Action NextHeadRequested;
    public event Action PreviousHeadRequested;

    public event Action NextTorsoRequested;
    public event Action PreviousTorsoRequested;

    public event Action NextLegsRequested;
    public event Action PreviousLegsRequested;

    private void OnEnable()
    {
        robotBuilder.StatsChanged += UpdateStats;
    }

    private void OnDisable()
    {
        robotBuilder.StatsChanged -= UpdateStats;
    }
    public void OnNextHeadButton()
    {
        NextHeadRequested?.Invoke();
    }

    public void OnPreviousHeadButton()
    {
        PreviousHeadRequested?.Invoke();
    }

    public void OnNextTorsoButton()
    {
        NextTorsoRequested?.Invoke();
    }

    public void OnPreviousTorsoButton()
    {
        PreviousTorsoRequested?.Invoke();
    }

    public void OnNextLegsButton()
    {
        NextLegsRequested?.Invoke();
    }

    public void OnPreviousLegsButton()
    {
        PreviousLegsRequested?.Invoke();
    }

    private void UpdateStats(RobotStats stats)
    {
        weightText.text = $"Weight: {stats.Weight}";
        powerText.text = $"Power: {stats.Power}";
    }
}