using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5f;

        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Player")
            {
                if (!weapon) return;

                c.GetComponent<Fighter>().EquipWeapon(weapon);

                StartCoroutine(DisableForSeconds(respawnTime));
            }
        }

        private IEnumerator DisableForSeconds(float timer)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(timer);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }
    }
}
