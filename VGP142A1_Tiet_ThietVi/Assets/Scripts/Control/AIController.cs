using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField] float chaseDistance = 5.0f;
        [SerializeField] float suspicionTime = 5.0f;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        [SerializeField] float waypointDwellTime = 0.0f;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        [SerializeField] bool randomPatrol;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] PatrolPathGraph patrolPathGraph;
        [SerializeField] float waypointTolerance = 1.0f;
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.5f;

        Fighter fighter;
        Mover mover;
        Health health;
        GameObject player;

        Vector3 guardPosition;
        int currentWaypointIndex = 0;
        #endregion

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            player = GameObject.FindGameObjectWithTag("Player");

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead) return;

            if (TargetInAttackRange(player) && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour(randomPatrol);
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        #region ATTACK
        private void AttackBehaviour()
        {
            fighter.Attack(player);
            timeSinceLastSawPlayer = 0;
        }

        private bool TargetInAttackRange(GameObject target)
        {
            return Vector3.Distance(transform.position, target.transform.position) <= chaseDistance;
        }
        #endregion ATTACK

        #region SUSPICION
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        #endregion SUSPICION

        #region PATROL
        private void PatrolBehaviour(bool random)
        {
            Vector3 nextPosition = guardPosition;

            if (!random)
            {
                if (patrolPath != null)
                {
                    if (AtWaypoint())
                    {
                        timeSinceArrivedAtWaypoint = 0;
                        CycleWaypoint();
                    }

                    nextPosition = GetCurrentWaypoint();
                }

                if (timeSinceArrivedAtWaypoint >= waypointDwellTime)
                {
                    mover.StartMoveAction(nextPosition, patrolSpeedFraction);
                }
            }
            else
            {
                if (patrolPathGraph != null)
                {
                    if (AtWaypointRandom())
                    {
                        timeSinceArrivedAtWaypoint = 0;
                        CycleWaypointRandom();
                    }

                    nextPosition = GetCurrentWaypointRandom();
                }

                if (timeSinceArrivedAtWaypoint >= waypointDwellTime)
                {
                    mover.StartMoveAction(nextPosition, patrolSpeedFraction);
                }
            }
        }

        private bool AtWaypoint()
        {
            return Vector3.Distance(transform.position, GetCurrentWaypoint()) <= waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextWaypointIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private bool AtWaypointRandom()
        {
            return Vector3.Distance(transform.position, GetCurrentWaypointRandom()) <= waypointTolerance;
        }

        private void CycleWaypointRandom()
        {
            currentWaypointIndex = patrolPathGraph.GetNextWaypointIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypointRandom()
        {
            return patrolPathGraph.GetWaypoint(currentWaypointIndex);
        }

        #endregion PATROL

        #region GIZMOS
        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
        #endregion
    }
}
