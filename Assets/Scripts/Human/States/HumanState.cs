using System;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public abstract class HumanState
    {
        protected Human _human;
        protected HumanStateManager _stateManager;

        public HumanState(Human human, HumanStateManager stateManager)
        {
            _human = human;
            _stateManager = stateManager;
        }

        public virtual void Start() {}

        public virtual void Exit() {}
    }
}
