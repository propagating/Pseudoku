﻿using System.Collections.Generic;
using System.Linq;
using Solver.Interfaces;

namespace Solver.ConstraintValidators
{
    public class BoxUnique : IConstraintValidator
    {
        public int ValidatorDifficulty { get; set; } = 1;

        public bool ValidatePotentialCellValues(PseudoCell cell, PseudoBoard board, out string solveMessage)
        {
            solveMessage = "";
            var existingValues = board.BoardCells.Where(x => x.CellBox == cell.CellBox
                                                             && x.SolvedCell
                                                             && cell.PossibleValues.Contains(x.CurrentValue)).ToList();
            var startCount = cell.PossibleValues.Count;
            foreach (var eCell in existingValues)
            {
                solveMessage = $"{solveMessage}\nRemoved {eCell.CurrentValue} from R{cell.CellRow} C{cell.CellColumn} for conflict with R{eCell.CellRow} C{eCell.CellColumn} : Box Constraint";
                cell.PossibleValues.Remove(eCell.CurrentValue);
            }

            if (cell.PossibleValues.Count == 1)
            {
                cell.CurrentValue   = cell.PossibleValues.First(); //only 1 value remains.
                solveMessage = $"{solveMessage}\nSolved for {cell.CurrentValue} in R{cell.CellRow} C{cell.CellColumn} : Naked Single Box Constraint";
                cell.PossibleValues = new List<int>();
                cell.SolvedCell     = true;
            }

            return cell.PossibleValues.Count != startCount;
        }
    }
}
