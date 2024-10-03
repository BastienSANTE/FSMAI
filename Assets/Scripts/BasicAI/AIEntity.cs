using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedAI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace BasicAI
{
    public class AIEntity : MonoBehaviour
    {
        public VisionComponent vision;
        public List<Transform> wanderPoints;                //List of visitable points
        public int selectedPoint;                           //Index of the selected point to go to
        public float wanderNearPoint;                       //Margin between points and AI, to narrow/expand area
        // private bool isSelecting;                        //Prevents from selecting in a loop and freezing, UNUSED
        public float attackRadius;                          //Radius under which attack state is enabled
        public float checkRadius;                           //Radius of sphere for floor collision
        public Player.Player playerReference;               //Reference to communicate with player
        public float fleeMultiplier;                        //Speed multiplier in Flee state
        public float attackCooldown;
    
        private GameObject _physicalBody;                   //Actual model of the AI Enemy
        private Rigidbody _rb;                              //RB Component, used for the jump movement
        
        [SerializeField] private AIState currentAIState;   //Current AI State
        public NavMeshAgent navMeshAgent;                   //Component to use for NavMesh
        private Transform _target;                          //Player position, if in detection radius
        private bool _isGrounded;                           //Check to prevent jumping to infinity
        private float _baseSpeed;
        private float _attackTimer;
    
        private void Awake()
        {
            currentAIState = AIState.Wandering;
            navMeshAgent = GetComponent<NavMeshAgent>();
            _physicalBody = transform.GetChild(0).gameObject;
            _rb = _physicalBody.GetComponent<Rigidbody>();
            playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
            _baseSpeed = navMeshAgent.speed;
        }   
    
    
        private void Update()
        {
            _isGrounded = Physics.CheckSphere(new Vector3(_physicalBody.transform.position.x, 
                _physicalBody.transform.position.y - 0.5f,
                _physicalBody.transform.position.z), checkRadius);
        
            switch (currentAIState)
            {
                case AIState.Wandering:
                    Wandering();
                    if (vision.inVision.Count > 0)
                    {
                        currentAIState = AIState.Chasing;
                        _target = vision.inVision.First().transform;
                    }
                    if (playerReference.attacking)
                    {
                        currentAIState = AIState.Fleeing;
                    }
                    break;
                case AIState.Chasing:
                    Chasing();
                    if (vision.inVision.Count == 0)
                    {
                        currentAIState = AIState.Wandering;
                    }
                    if (Vector3.Distance(transform.position, _target.position) <= attackRadius)
                    {
                        currentAIState = AIState.Attacking;
                    }
                    if (playerReference.attacking)
                    {
                        currentAIState = AIState.Fleeing;
                    }
                    break;
                case AIState.Attacking:
                    Attacking();
                    if (Vector3.Distance(transform.position, _target.position) > attackRadius)
                    {
                        currentAIState = AIState.Chasing;
                    }
                    break;
                case AIState.Fleeing:
                    Fleeing();
                    if (vision.inVision.Count == 0)
                    {
                        currentAIState = AIState.Wandering;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Wandering()
        {
            //Goes to selected point
            if (Vector3.Distance(transform.position, wanderPoints[selectedPoint].position) > wanderNearPoint) {
                navMeshAgent.SetDestination(wanderPoints[selectedPoint].position);
            } else {
                //Selects a random point among the list specified in the editor
                selectedPoint = Random.Range(0, wanderPoints.Count);
            }
        }
        private void Chasing()
        {
            navMeshAgent.SetDestination(_target.position);
        }
        private void Attacking()
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > attackCooldown)
            {
                _attackTimer = 0;
                Debug.Log("Attacking");
            }

        }
    
        private void Fleeing()
        {
            Debug.Log("Fleeing");
            //Almost a copy of the Wandering State, but speed tweaked
            navMeshAgent.speed = _baseSpeed * fleeMultiplier;
            // if (Vector3.Distance(transform.position, wanderPoints[selectedPoint].position) > wanderNearPoint) {
            //     _navMeshAgent.SetDestination(wanderPoints[selectedPoint].position);
            // } else {
            //     selectedPoint = Random.Range(0, wanderPoints.Count);
            // }
            navMeshAgent.SetDestination(-_target.position);

        }
    }
}