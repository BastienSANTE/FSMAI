using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public Rigidbody rigidBody;
        public float movementSpeed;
        public bool attacking;
        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update()
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            rigidBody.AddForce(movement, ForceMode.Force);
        
            if (Input.GetMouseButtonDown(0))
            {
                attacking = !attacking;
            }
        }
        
    }
}



