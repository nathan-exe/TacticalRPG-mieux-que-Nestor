using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

namespace SimpleVFXs
{


    public class StylisedEffect : MonoBehaviour
    {
        [SerializeField] bool TriggerMainEventOnAwake = true;

        [SerializeField] UnityEvent MainEvent;



        private void Start()
        {
            //prevents the vfx from always playing on awake
            if (TryGetComponent<VisualEffect>(out VisualEffect vfx))
            {
                vfx.Stop();
            }
            
            //plays the whole event on awake
            if(TriggerMainEventOnAwake)
            {
                TriggerMainEvent();
            }
        }

        void OnPulledFromPool()
        {
            //prevents the vfx from always playing on awake
            if (TryGetComponent<VisualEffect>(out VisualEffect vfx))
            {
                vfx.Stop();
            }

            //plays the whole event on awake
            if (TriggerMainEventOnAwake)
            {
                TriggerMainEvent();
            }
        }

        public void TriggerMainEvent()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                MainEvent.Invoke();
            }
            else //In edit mode, tries to play every monobehaviour method listening for the event anyways
            {
                for (int i = 0; i < MainEvent.GetPersistentEventCount(); i++)
                {
                    if ((MonoBehaviour)MainEvent.GetPersistentTarget(i) == null) continue;

                    ((MonoBehaviour)MainEvent.GetPersistentTarget(i)).SendMessage(MainEvent.GetPersistentMethodName(i),SendMessageOptions.DontRequireReceiver);

                }
            }
#else
            MainEvent.Invoke();
#endif
        }


        public void playVFX()
        {
            GetComponent<VisualEffect>().Play();
        }


    }




}