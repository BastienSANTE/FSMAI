using UnityEngine;
using UnityEngine.AI;

namespace AdvancedAI
{ 
    public class AIEntity : MonoBehaviour
    {
        public VisionComponent vision;        //Viewing frustum, OpenGL style
        [HideInInspector] public GameObject[] wanderPoints;  //List of visitable points
        public Player.Player playerReference; //Reference to communicate with player
        public NavMeshAgent navAgent;        //NavMesh utility
        public Transform target;              //Player position, if in detection radius

        public int selectedPoint;     //Index of the selected point to go to
        public float wanderNearPoint; //Margin between points and AI, to narrow/expand area

        public float attackRadius;   //Radius under which attack state is enabled
        public float checkRadius;    //Radius of sphere for floor collision
        public float attackCooldown; //Minimum time between attacks, prevents abuse
        public float attackTimer;    //Flash timer for attack effect

        public float baseSpeed;      //Wander state speed, multiplied by fleeMultiplier
        public float fleeMultiplier; //Speed multiplier in Flee state

        private GameObject _physicalBody; //Actual model of the AI Enemy
        private bool _isGrounded;         //Check to prevent jumping to infinity

        private State _state;
        
        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>(); baseSpeed = navAgent.speed;
            _physicalBody = transform.GetChild(0).gameObject;
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
            if (playerGameObject) playerReference = playerGameObject.GetComponent<Player.Player>();
            _state = new Wander(this);
        }

        private void Start()
        {
            wanderPoints = GameObject.FindGameObjectsWithTag("Waypoint");
            Debug.Log(wanderPoints.Length);
        }

        private void Update()
        {
            _state.Execute();
            State nextState = _state.NextState();
            if (nextState != null) _state = nextState;
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 30, 100, 20), $"AI State: {_state}", new GUIStyle {fontSize = 20});
        }
    }
}