namespace Solver
{
    public class SolverStep
    {
        public uint SolverStepId { get; set; }
        public string StepComment { get; set; }
        public SolverState BoardState { get; set; }
    }
}
