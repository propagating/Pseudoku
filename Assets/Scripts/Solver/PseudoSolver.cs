using System;
using System.Collections.Generic;
using System.Linq;
using Solver.ConstraintValidators;
using Solver.Enums;
using Solver.Interfaces;
using Solver.SolveMethods;

namespace Solver
{
    public class PseudoSolver
    {
        public List<IConstraintValidator> BoardValidators { get; set; } = new List<IConstraintValidator>();
        public List<ISolveMethod> SolverMethods { get; set; } = new List<ISolveMethod>();

        public PseudoSolver(List<PuzzleConstraint> constraints)
        {
            SolverMethods.Add(new HiddenSingle());
            SolverMethods.Add(new IntersectionRemoval());


            foreach (var constraint in constraints)
            {
                switch (constraint)
                {
                    case PuzzleConstraint.BoxUnique:
                        BoardValidators.Add(new BoxUnique());
                        break;
                    case PuzzleConstraint.ColumnUnique:
                        BoardValidators.Add( new ColumnUnique());
                        break;
                    case PuzzleConstraint.RowUnique:
                        BoardValidators.Add(new RowUnique());
                        break;
                    case PuzzleConstraint.KnightUnique:
                        BoardValidators.Add(new KnightUnique());
                        break;
                    case PuzzleConstraint.KingUnique:
                        BoardValidators.Add(new KingUnique());
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        public void Solve(PseudoBoard board)
        {
            var currentFailures = 0;
            var totalFailures   = 0;
            var totalSteps      = 0;
            var methodSteps     = 0;
            var validatorSteps  = 0;
            while (!board.PuzzleSolved)
            {
                var solvableCells = board.BoardCells.Where(x => !x.SolvedCell).ToList();
                foreach (var cell in solvableCells)
                {

                    var validatorSuccess = false;
                    var methodSuccess    = false;
                    foreach (var validator in BoardValidators.OrderBy(x=> x.ValidatorDifficulty))
                    {
                        validatorSuccess = validator.ValidatePotentialCellValues(cell, board);
                        if (!validatorSuccess)
                        {
                            currentFailures++;
                            totalFailures++;
                        }
                        else
                        {
                            currentFailures = 0;
                        }

                        validatorSteps++;
                        totalSteps++;
                    }

                    if (!validatorSuccess)
                    {
                        foreach (var method in SolverMethods.OrderBy(x=> x.MethodDifficulty))
                        {
                            methodSuccess = method.ApplyMethod(cell, board);
                            if (!methodSuccess)
                            {
                                currentFailures++;
                                totalFailures++;
                            }
                            else
                            {
                                currentFailures = 0;
                            }
                            methodSteps++;
                            totalSteps++;
                        }
                    }
                }

                board.PuzzleSolved = solvableCells.All(x => x.SolvedCell);

                //TODO: Store state if update occurs
                //TODO: Add some sort of pause to check for next step
            }
        }
    }
}
