using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG/Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController attackAnimationOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] Projectile projectile = null;
        [SerializeField] float weaponRange = 1.0f;
        [SerializeField] float weaponDamage = 3.0f;
        [SerializeField] bool isRightHanded = true;

        public float WeaponRange { get { return weaponRange; } }
        public float WeaponDamage { get { return weaponDamage; } }
        public bool HasProjectile { get { return projectile != null; } }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab)
            {
                Transform equipHand = GetEquipHand(rightHand, leftHand);

                Instantiate(equippedPrefab, equipHand);
            }

            if (attackAnimationOverride)
            {
                animator.runtimeAnimatorController = attackAnimationOverride;
            }
        }

        private Transform GetEquipHand(Transform rightHand, Transform leftHand)
        {
            Transform equipHand;

            if (isRightHanded) equipHand = rightHand;
            else equipHand = leftHand;

            return equipHand;
        }

        public void ShootProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetEquipHand(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }
    }
}