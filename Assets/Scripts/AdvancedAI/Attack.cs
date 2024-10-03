using UnityEngine;

namespace AdvancedAI
{
    public class Attack : State
    {
        public Attack(AIEntity entity) : base(entity) { }

        public override void Execute()
        {
            Debug.Log("Attacking");
        }

        public override State NextState()
        {
            throw new System.NotImplementedException();
        }
    }
}