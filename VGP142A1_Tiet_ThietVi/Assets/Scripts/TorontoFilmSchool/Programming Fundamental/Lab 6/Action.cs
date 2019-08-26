using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace ObserverPattern
{
    public abstract class Action
    {
        public abstract void Execute();
        public abstract void Execute(GameObject gameObject);
    }

    public class Attack : Action
    {
        public override void Execute()
        {
        }

        public override void Execute(GameObject gameObject)
        {
            gameObject.GetComponent<AIController>().AttackBehaviour();
        }
    }
}