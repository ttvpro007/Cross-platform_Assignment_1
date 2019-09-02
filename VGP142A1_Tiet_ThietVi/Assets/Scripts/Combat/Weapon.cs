﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using System;

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
        [SerializeField] float attackPerSecond = 0.5f;
        [SerializeField] bool isRightHanded = true;

        const string weaponName = "Weapon";

        public bool HasProjectile { get { return projectile != null; } }
        public float WeaponRange { get { return weaponRange; } }
        public float WeaponDamage { get { return weaponDamage; } }
        public float WeaponAttackRate { get { return 1 / attackPerSecond; } }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (equippedPrefab)
            {
                Transform equipHand = GetEquipHand(rightHand, leftHand);

                GameObject weapon = Instantiate(equippedPrefab, equipHand);
                weapon.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            if (attackAnimationOverride)
            {
                animator.runtimeAnimatorController = attackAnimationOverride;
            }
            else if (overrideController)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);

            if (!oldWeapon)
                oldWeapon = leftHand.Find(weaponName);

            if (!oldWeapon) return;

            oldWeapon.name = "Destroying";
            Destroy(oldWeapon.gameObject);
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