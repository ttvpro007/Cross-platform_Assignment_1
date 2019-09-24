using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [Range(0, 2)]
        [SerializeField] public float moveSpeedFraction = 1.415f;

        Health health;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjDistance = 1f;
        [SerializeField] float maxNavPathLength = 30f;

        // Cross-platform edit
        public Transform targetTransform;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (InteractWithUI()) return;
            //if (PauseMenu.GameIsPaused) return;
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }

            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;

            SetCursor(CursorType.None);
        }
        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            else
                return false;
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSotred();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSotred()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];

            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits;
        }

        private bool InteractWithMovement()
        {

            Vector3 target;

            bool hasHit = RaycastNavMesh(out target);

            if (hasHit)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, moveSpeedFraction);
                }

                SetCursor(CursorType.Movement);

                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hitNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjDistance, NavMesh.AllAreas);

            if (!hitNavMesh) return false;
            target = navMeshHit.position;

            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);

            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;

            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float totalLength = 0;

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                totalLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return totalLength;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping cursorMapping = GetCursorMapping(type);
            Cursor.SetCursor(cursorMapping.texture, cursorMapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping cursorMapping in cursorMappings)
            {
                if (cursorMapping.type == type)
                    return cursorMapping;
            }

            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}