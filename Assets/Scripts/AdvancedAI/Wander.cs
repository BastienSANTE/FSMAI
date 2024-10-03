using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdvancedAI
{
    public class Wander : State
    {
        public Wander(AIEntity entity) : base(entity) { }

        public override void Execute()
        {
            //Goes to selected point
            if (Vector3.Distance(Entity.transform.position, Entity.wanderPoints[Entity.selectedPoint].transform.position) > Entity.wanderNearPoint) {
                Entity.navAgent.SetDestination(Entity.wanderPoints[Entity.selectedPoint].transform.position);
            } else {
                //Selects a random point among the list specified in the editor
                Entity.selectedPoint = Random.Range(0, Entity.wanderPoints.Length);
            }
        }

        public override State NextState()
        {
            if (Entity.vision.inVision.Count != 0)
            {
                return new Chase(Entity);
            }

            if (Entity.playerReference.attacking)
            {
                return new Flee(Entity);
            }

            return null;
        }
    }
}
