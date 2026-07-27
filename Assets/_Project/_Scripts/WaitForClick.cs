using UnityEngine;

namespace GameCore.UnityServiceLocator
{
    public partial class GameContext
    {
        public class WaitForClickCell : CustomYieldInstruction
        {
            public bool Cell { get; private set; } = false;

            public override bool keepWaiting
            {
                get
                {
                    Cell = true;
                    return !Input.GetMouseButtonDown(0);
                }
            }
        }
    }
}