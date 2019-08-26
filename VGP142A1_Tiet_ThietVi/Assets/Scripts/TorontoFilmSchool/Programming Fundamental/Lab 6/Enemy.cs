﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Control;

namespace ObserverPattern
{
    public class Enemy : Observer
    {
        GameObject self;
        AIController aiController;
        Attack attack;

        public Enemy(GameObject self)
        {
            this.self = self;
            aiController = self.GetComponent<AIController>();
            attack = new Attack();
        }

        public override void OnNotify()
        {
            aiController.isNotified = true;
            //attack.Execute(self);
        }
    }
}