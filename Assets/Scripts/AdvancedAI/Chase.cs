namespace AdvancedAI
{
    public class Chase : State
    {
        public override void Enter()
        {
            
        }

        public override void Execute(AIEntity actor)
        {
            actor.navAgent.SetDestination(actor.target.position);
        }

        public override void Exit()
        {
            
        }
    }
}