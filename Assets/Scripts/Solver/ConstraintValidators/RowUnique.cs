using System.Collections.Generic;
using System.Linq;
using Solver.Interfaces;

namespace Solver.ConstraintValidators
{
    public class RowUnique : IConstraintValidator
    {
        public int ValidatorDifficulty { get; set; } = 1;
        public bool ValidatePotentialCellValues(PseudoCell cell, PseudoBoard board)
        {
            var existingValues = board.BoardCells.Where(x => x.CellRow == cell.CellRow
                                                             && x.SolvedCell
                                                             && cell.PossibleValues.Contains(x.CurrentValue)).Select(x=> x.CurrentValue).ToList();
            var startCount = cell.PossibleValues.Count;
            foreach (var value in existingValues)
            {
                cell.PossibleValues.Remove(value);
            }

            if (cell.PossibleValues.Count == 1)
            {
                cell.CurrentValue   = cell.PossibleValues.First(); //only 1 value remains.
                cell.PossibleValues = new List<int>();
                cell.SolvedCell     = true;
            }

            return cell.PossibleValues.Count != startCount;
        }
    }
}
