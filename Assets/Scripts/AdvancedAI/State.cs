namespace AdvancedAI
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Execute(AIEntity actor);
        public abstract void Exit();
    }
}