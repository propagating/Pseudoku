using System.Collections.Generic;

namespace Solver
{
    public class SolverState
    {
        public List<PseudoCell> BoardCells { get; set; }
        public bool SolvedState { get; set; }
    }
}
