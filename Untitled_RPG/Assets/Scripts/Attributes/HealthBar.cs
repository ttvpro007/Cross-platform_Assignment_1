using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health health;
        [SerializeField] RectTransform foreGround;
        [SerializeField] Canvas rootCanvas;

        private void Update()
        {
            if (Mathf.Approximately(health.GetFraction(), 0) || health.GetFraction() == 1)
            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;

            foreGround.localScale = new Vector3(health.GetFraction(), 1, 1);
        }
    }
}
