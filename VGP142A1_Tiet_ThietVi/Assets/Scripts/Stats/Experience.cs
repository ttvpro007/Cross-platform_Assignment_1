using RPG.Saving;
using System;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float xp = 0;

        public float Value { get { return xp; } }

        //public delegate void XPGainedDelegate();
        //public event XPGainedDelegate onXPGained; OR Action - void delegate with no return type
        
        public event Action onXPGained;

        public void GainXP(float xp)
        {
            this.xp += xp;
            onXPGained();
        }

        public object CaptureState()
        {
            return xp;
        }

        public void RestoreState(object state)
        {
            xp = (float)state;
        }
    }
}
