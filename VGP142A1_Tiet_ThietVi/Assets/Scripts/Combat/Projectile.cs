using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float damage = 0f;
        [SerializeField] float speed = 1f;
        [SerializeField] float maxLifeTime = 3f;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] GameObject[] destroyOnImpact = null;
        [SerializeField] float lifeAfterImpact = 2f;
        [SerializeField] bool homing = false;

        Health target = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
            Destroy(gameObject, maxLifeTime);
        }

        private void Update()
        {
            if (!target) return;

            if (homing && !target.IsDead)
            {
                transform.LookAt(GetAimLocation());
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage += damage;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

            if (targetCapsule == null)
            {
                return target.transform.position;
            }

            return target.transform.position + Vector3.up * targetCapsule.height / 1.5f;
        }

        private void OnTriggerEnter(Collider c)
        {
            if (c.GetComponent<Health>() != target) return;

            if (target.IsDead) return;

            target.TakeDamage(damage);

            speed = 0.0f;

            if (hitEffect)
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
            }

            for (int i = 0; i < destroyOnImpact.Length; i++)
            {
                Destroy(destroyOnImpact[i]);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }
}
