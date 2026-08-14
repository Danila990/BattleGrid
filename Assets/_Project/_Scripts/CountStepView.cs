using TMPro;
using UnityEngine;
using UnityServiceLocator;

namespace BattleGridGame
{
    public class CountStepView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [Inject] private PlayerStepCounter _counter;

        private void Start()
        {
            _counter.OnCountStep += UpdateText;
            UpdateText(_counter.StepCount);
        }

        private void UpdateText(int countStep)
        {
            _text.text = countStep.ToString();
        }

        private void OnDestroy()
        {
            _counter.OnCountStep -= UpdateText;
        }
    }
}
