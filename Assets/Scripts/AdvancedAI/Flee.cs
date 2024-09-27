namespace AdvancedAI
{
    public class Flee : State
    {
        public override void Enter()
        {
        }

        public override void Execute(AIEntity actor)
        {
            actor.navAgent.speed = actor.baseSpeed * actor.fleeMultiplier;
            actor.navAgent.SetDestination(-actor.target.position);
        }

        public override void Exit()
        {
        }
    }
}