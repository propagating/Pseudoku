﻿using System.Collections.Generic;
using System.Linq;

namespace Pseudoku.Solver.Validators
{
    public class KingUnique : IValidator
    {
        public int ValidatorDifficulty { get; set; } = 1;
        public static readonly List<(int, int)> KingMoves = new List<(int, int)> {(1, 1), (1, -1), (-1, 1), (-1, -1)};
        public bool ValidatePotentialCellValues(PseudoCell cell, PseudoBoard board)
        {
            var startCount = cell.PossibleValues.Count;
            foreach (var move in KingMoves.ToList())
            {
                var moveVertical   = cell.CellRow + move.Item1;
                var moveHorizontal = cell.CellColumn + move.Item2;

                var existingValues = board.BoardCells.Where(x => x.CellRow == moveVertical
                                                                 && x.CellColumn == moveHorizontal
                                                                 && x.SolvedCell
                                                                 && cell.PossibleValues.Contains(x.CurrentValue)).Select(x => x.CurrentValue).ToList();
                foreach (var value in existingValues)
                {
                    cell.PossibleValues.Remove(value);
                }

                if (cell.PossibleValues.Count == 1)
                {
                    cell.CurrentValue   = cell.PossibleValues.First(); //only 1 value remains.
                    cell.PossibleValues = new List<int>();
                    cell.SolvedCell     = true;
                    return true;
                }
            }

            return cell.PossibleValues.Count != startCount;
        }
    }
}
