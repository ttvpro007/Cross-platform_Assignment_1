using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] string defaultWeaponName = "Unarmed";

        Health target;

        Weapon currentWeapon = null;

        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (!target) return;

            if (target.IsDead) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1.0f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (!weapon || currentWeapon == weapon) return;
            currentWeapon = weapon;

            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetHealth = combatTarget.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead && !GetComponent<Health>().IsDead;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack >= currentWeapon.WeaponAttackRate)
            {
                // Trigger animation Hit() event
                TriggerAttackAnimation();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("cancelAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.WeaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttackAnimation();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("cancelAttack");
            GetComponent<Animator>().SetTrigger("cancelAttack");
        }

        // Animation Event
        void Hit()
        {
            if (target == null) return;

            if (currentWeapon.HasProjectile)
            {
                currentWeapon.ShootProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.WeaponDamage);
            }
        }

        // Animation Event
        void Shoot()
        {
            Hit();
        }

        public object CaptureState()
        {
            return (currentWeapon) ? currentWeapon.name : "Unarmed";
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}