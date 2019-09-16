using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3.0f);
            yield return new WaitForSeconds(0.5f);
            yield return FadeIn(3.0f);
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1.0f;
        }

        public IEnumerator FadeOut(float time)
        {
            //while (canvasGroup.alpha < 1)
            while (canvasGroup.alpha < 0.1)
            {
                canvasGroup.alpha += Mathf.Clamp01(Time.deltaTime / time);
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Mathf.Clamp01(Time.deltaTime / time);
                yield return null;
            }
        }
    }
}
