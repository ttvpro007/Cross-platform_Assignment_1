using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Player")
            {
                if (!weapon) return;

                c.GetComponent<Fighter>().EquipWeapon(weapon);

                Destroy(gameObject);
            }
        }
    }
}
