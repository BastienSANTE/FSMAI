namespace AdvancedAI
{
    public class Flee : State
    {
        public Flee(AIEntity entity) : base(entity) { }

        public override void Execute()
        {
            Entity.navAgent.speed = Entity.baseSpeed * Entity.fleeMultiplier;
            Entity.navAgent.SetDestination(-Entity.target.position);
        }

        public override State NextState()
        {
            if (Entity.playerReference.attacking)
            {
                return null;
            }
            
            return new Wander(Entity);
            
        }
    }
}