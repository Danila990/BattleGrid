using UnityServiceLocator;
using System.Collections;
using UnityEngine;

namespace BattleGridGame
{
    public class GameContext : SceneContext
    {
        [SerializeField] private GameOptions _gameOptions;
        [SerializeField] private GridOptions _gridOptions;

        private GridMap _gridMap;
        private GridUnitCreator _unitCreator;

        protected override void Configurate(IServiceRegister register)
        {
            register.RegisterInstantiate(_gameOptions);

            //grid
            register.RegisterInstantiate(_gridOptions);
            _gridMap = register.RegisteNewGameobject<GridMap>();
            _unitCreator = register.RegisteNewGameobject<GridUnitCreator>();
            register.RegisteNewGameobject<GridUnitInteractor>();
        }

        protected override void BuildComplete()
        {
            _gridMap.CreateGrid();
            _unitCreator.CreateUnitTest();
        }

        protected override IEnumerator GameStart()
        {
            /*WaitForClick InputCell = new WaitForClick();
            yield return InputCell;
            Debug.Log(InputCell.Cell);*/
            yield return null;
        }
    }
}