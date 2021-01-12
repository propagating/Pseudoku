﻿namespace Solver.Interfaces
{
    public interface IConstraintValidator
    {
        int ValidatorDifficulty { get; set; }
        bool ValidatePotentialCellValues(PseudoCell cell, PseudoBoard board);
    }
}