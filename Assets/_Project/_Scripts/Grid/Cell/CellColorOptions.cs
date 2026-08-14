using UnityEngine;

namespace BattleGridGame
{
    [CreateAssetMenu(fileName = nameof(CellColorOptions))]
    public class CellColorOptions : ScriptableObject
    {
        [field: SerializeField, Header("Cell Select color")] public Color Select { get; private set; } = Color.green;
        [field: SerializeField] public Color Attack { get; private set; } = Color.red;
        [field: SerializeField] public Color Move { get; private set; } = Color.blue;

        [field: SerializeField, Header("Fraction Color")] public Color Player { get; private set; } = Color.skyBlue;
        [field: SerializeField] public Color AI_1 { get; private set; } = Color.softRed;
        [field: SerializeField] public Color AI_2 { get; private set; } = Color.violetRed;
        [field: SerializeField] public Color AI_3 { get; private set; } = Color.orangeRed;

        public Color Default = Color.white;

        public Color GetInteractionColor(CellInteractionType cellInteractionType)
        {
            return cellInteractionType switch
            {
                CellInteractionType.Select => Select,
                CellInteractionType.Attack => Attack,
                CellInteractionType.Move => Move,
                CellInteractionType.Default or _ => Default,
            };
        }

        public Color GetTeamCcolor(TeamType team)
        {
            return team switch
            {
                TeamType.Player => Player,
                TeamType.AI_1 => AI_1,
                TeamType.AI_2 => AI_2,
                TeamType.None or _ => Default,
            };
        }
    }
}
