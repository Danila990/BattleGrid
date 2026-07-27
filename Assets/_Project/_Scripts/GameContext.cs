using GameCore.UnityServiceLocator;
using System.Collections;
using UnityEngine;

namespace BattleGridGame
{
    public partial class GameContext : SceneContext
    {
        [SerializeField] private GameOptions _gameOptions;
        [SerializeField] private GridOptions _gridOptions;

        private GridMap _gridMap;
        private GridUnitCreator _unitCreator;

        protected override void Configurate(IServiceBuilder builder)
        {
            builder.RegisterInstantiate(_gameOptions);

            //grid
            builder.RegisterInstantiate(_gridOptions);
            _gridMap = builder.RegisteNewGameobject<GridMap>();
            _unitCreator = builder.RegisteNewGameobject<GridUnitCreator>();
            builder.RegisteNewGameobject<GridUnitInteractor>();
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