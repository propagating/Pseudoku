namespace Solver.Interfaces
{
    public interface ISolveMethod
    {
        int MethodDifficulty { get; set; }
        bool ApplyMethod(PseudoCell cell, PseudoBoard board, out string solveMessage);
    }
}
