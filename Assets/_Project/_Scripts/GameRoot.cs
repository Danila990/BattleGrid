using System.Collections;
using UnityEngine;
using UnityServiceLocator;

namespace BattleGridGame
{
    public class GameRoot : ContextRoot
    {
        [Inject] private UnitCreator _unitCreator;
        [Inject] private PlayerUnitInteractor _playerUnitInteraction;
        [Inject] private PlayerStepCounter _playerStepCounter;

        public override void OnAwake()
        {
            _unitCreator.CreateUnitTest();
        }

        public override void OnStart()
        {
            _playerStepCounter.ResetStepCounter();
            StartCoroutine(MainGameRoot());
        }

        public override void OnEnd()
        {
            StopAllCoroutines();
        }
        public IEnumerator MainGameRoot()
        {
            //player loop
            while (true)
            {
                yield return new WaitUntil(() => _playerStepCounter.CanStep);
                yield return _playerUnitInteraction.UnitInteraction();
                _playerStepCounter.Step();
            }
        }
    }
}