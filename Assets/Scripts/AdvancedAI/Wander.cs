using UnityEngine;

namespace AdvancedAI
{
    public class Wander : State
    {
        public override void Enter()
        {

        }

        public override void Execute(BasicAI.AIEntity actor)
        {
            //Goes to selected point
            if (Vector3.Distance(actor.transform.position, actor.wanderPoints[actor.selectedPoint].position) > actor.wanderNearPoint) {
                actor.navMeshAgent.SetDestination(actor.wanderPoints[actor.selectedPoint].position);
            } else {
                //Selects a random point among the list specified in the editor
                actor.selectedPoint = Random.Range(0, actor.wanderPoints.Count);
            }
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}
