using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver
{
    public class PseudoBoard
    {
        public List<PseudoCell> BoardCells { get; set; } = new List<PseudoCell>();
        public int MaxRows { get; set; }
        public int MaxColumns { get; set; }
        public List<int> AllowedValues { get; set; }
        public bool ValidState { get; set; } = true;
        public bool PuzzleSolved { get; set; }
        public string PuzzleString { get; set; }
        public int RowBoxes { get; set; }
        public int ColumnBoxes { get; set; }
        public int TotalBoxes { get; set; }
        public string SolutionString { get; set; }

        public PseudoBoard(int rows, int cols, string inputValues)
        {
            MaxRows           = rows;
            MaxColumns        = cols;
            AllowedValues     = Enumerable.Range(1, Math.Max(MaxRows, MaxColumns)).Select(x => x).ToList();
            PuzzleString      = inputValues;

            var currentRow    = 1;
            var currentColumn = 1;
            var currentIndex  = 0;

            var values = inputValues.ToList();

            while (currentRow <= MaxRows)
            {
                while (currentColumn <= MaxColumns)
                {
                    var cellValue = Convert.ToInt32(values[currentIndex] - 48); //char of 0 enters as 48
                    var pseudoCell = new PseudoCell
                    {
                        CellRow        = currentRow,
                        CellColumn     = currentColumn,
                        CurrentValue   = cellValue,
                        PossibleValues = cellValue == 0 ? Enumerable.Range(1, Math.Max(MaxRows, MaxColumns)).Select(x=> x).ToList() : new List<int>(),
                        SolvedCell     = cellValue == 0 ? false : true
                    };

                    pseudoCell.FindBox();
                    BoardCells.Add(pseudoCell);
                    currentIndex++;
                    currentColumn++;
                }

                currentColumn = 1;
                currentRow++;
            }
        }

    }
}
