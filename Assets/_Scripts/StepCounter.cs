using System;
using UnityEngine;

public class StepCounter : MonoBehaviour
{
    public static event Action<int> OnCountStep;

    [field: SerializeField] public int MaxStepCount = 3;

    public static int StepCount { get; private set; }

    public bool CanStep => StepCount > 0;

    private void Awake()
    {
        ResetStepCounter();
    }

    public void Step()
    {
        StepCount--;
        OnStep();
    }

    public void ResetStepCounter()
    {
        StepCount = MaxStepCount;
        OnStep();
    }

    public void OnStep()
    {
        OnCountStep?.Invoke(StepCount);
    }
}
