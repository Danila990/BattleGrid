using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace BattleGridGame
{
    public class CompleteStepButton : MonoBehaviour
    {
        [SerializeField] private Button _stepButton;

        [Inject] private PlayerStepCounter _counter;

        private void Start()
        {
            _stepButton.onClick.AddListener(OnStepComplete);
        }


        private void OnDestroy()
        {
            _stepButton.onClick.RemoveListener(OnStepComplete);

        }

        private void OnStepComplete()
        {
            _counter.ResetStepCounter();
        }
    }
}
