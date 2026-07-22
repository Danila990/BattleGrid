using TMPro;
using UnityEngine;

public class CountStepView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        PlayerStepCounter.OnCountStep += UpdateText;
        UpdateText(PlayerStepCounter.StepCount);
    }

    private void UpdateText(int countStep)
    {
        _text.text = countStep.ToString();
    }

    private void OnDestroy()
    {
        PlayerStepCounter.OnCountStep -= UpdateText;
    }
}
