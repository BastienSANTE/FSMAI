using UnityEngine;

namespace AdvancedAI
{
    public class Attack : State
    {
        public override void Enter()
        {
        }

        public override void Execute(AIEntity actor)
        {
            Debug.Log("Attacking");
        }

        public override void Exit()
        {
        }
    }
}