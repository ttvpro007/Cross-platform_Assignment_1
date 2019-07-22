using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2.0f;

        Transform target;

        private void Update()
        {
            if (!target) return;

            if (!GetIsInRange())
            {
                    GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combat_target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combat_target.transform;
            switch (Mathf.FloorToInt(Random.Range(0.1f, 2.9f)))
            {
                case 0:
                    print("Take that!!! " + combat_target.name + "!!!");
                    return;
                case 1:
                    print("Heyyya!!! " + combat_target.name + "!!!");
                    return;
                case 2:
                    print("Haaaaa!!! " + combat_target.name + "!!!");
                    return;
            }
        }

        public void Cancel()
        {
            target = null;
        }
    }
}