using System.Collections;
using UnityEngine;

namespace BattleGridGame
{
    public class UnitGridMapEvents : MonoBehaviour
    {
        public IEnumerator UnitMove(ICell currentCell, ICell targetCell)
        {
            //play anim move

            currentCell.Unit.Movement(targetCell.MovePos);
            targetCell.SetUnit(currentCell.Unit);
            targetCell.SetTeam(currentCell.Team);
            currentCell.SetUnit(null);
            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator UnitBattle(ICell attackCell, ICell defenceCell)
        {
            // play anim attacks

            attackCell.Unit.TakeDamage(defenceCell.Unit.Damage);
            defenceCell.Unit.TakeDamage(attackCell.Unit.Damage);

            DestroyIsDead(attackCell);
            DestroyIsDead(defenceCell);
            yield return new WaitForSeconds(0.5f);
        }

        private void DestroyIsDead(ICell cell)
        {
            if(cell.Unit.IsDead)
            {
                //play Anim Dead

                //Заглушка
                Destroy(cell.Unit.gameObject);
                cell.SetUnit(null);
            }
        }
    }
}
