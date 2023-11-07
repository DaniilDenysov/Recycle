using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TrainingGrounds
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance;

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] private List<Scenarium> scenariums;
        private int currentState = 0;

        public void NextScenarium()
        {
            scenariums[currentState].OnScenariumDone();
            scenariums[currentState++]?.OnStart();
        }

        [System.Serializable]
        class Scenarium
        {
            [SerializeField] private bool done;
            [SerializeField] private UnityEvent startEvent;

            public void OnStart ()
            {
                startEvent.Invoke();
            }

            public void OnScenariumDone ()
            {
                done = true;
            }
        }
    }
}
