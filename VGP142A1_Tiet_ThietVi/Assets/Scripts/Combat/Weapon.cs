using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG/Weapon/New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController attackAnimationOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] Projectile projectile = null;
        [SerializeField] float range = 1.0f;
        [SerializeField] float damage = 3.0f;
        [SerializeField] float damagePercentageBonus = 0f;
        [SerializeField] float attackPerSecond = 0.5f;
        [SerializeField] bool isRightHanded = true;

        const string weaponName = "Weapon";

        public bool HasProjectile { get { return projectile != null; } }
        public float Range { get { return range; } }
        public float Damage { get { return damage; } }
        public float DamageBuffPercentage { get { return damagePercentageBonus; } }
        public float AttackRate { get { return 1 / attackPerSecond; } }

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

        public void ShootProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetEquipHand(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(instigator, target, calculatedDamage);
        }
    }
}