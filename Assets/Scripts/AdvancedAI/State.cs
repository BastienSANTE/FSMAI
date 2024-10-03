namespace AdvancedAI
{
    public abstract class State
    {
        protected AIEntity Entity;

        protected State(AIEntity entity)
        {
            Entity = entity;
        }

        public abstract void Execute();

        public abstract State NextState();
    }
}