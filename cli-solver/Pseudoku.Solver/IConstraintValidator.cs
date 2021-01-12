namespace Pseudoku.Solver
{
    public interface IConstraintValidator
    {
        public int ValidatorDifficulty { get; set; }
        public bool ValidatePotentialCellValues(PseudoCell cell, PseudoBoard board);
    }
}
