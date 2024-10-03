using System;
using System.Linq;

namespace AdvancedAI
{
    public class Chase : State
    {

        private Player.Player _player;
        
        public Chase(AIEntity entity) : base(entity)
        {
            if (Entity.vision.inVision.Count == 0)
                throw new Exception("Started in Chase, but with nobody in vision");
            Entity.target = Entity.vision.inVision[0].transform;
            _player = Entity.target.GetComponent<Player.Player>();
        }

        public override void Execute()
        {

            Entity.navAgent.SetDestination(Entity.target.position);
        }

        public override State NextState()
        {
            if (Entity.vision.inVision.Count > 0)
            {
                return new Chase(Entity);
            }
            
            if (_player.attacking)
            {
                return new Flee(Entity);
            }
            
            return null;
        }
    }
}