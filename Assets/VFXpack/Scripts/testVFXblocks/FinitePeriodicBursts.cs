using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class FinitePeriodicBursts : VFXSpawnerCallbacks
{
    public class InputProperties
    {
        //[Min(0), Tooltip("Sets the number of particles to spawn per second.")]
        public int BurstCount = 10;
        public int particlesPerBurst = 10;
        public float DelayBetweenBursts = .2f;
    }

    static private readonly int BurstCountID = Shader.PropertyToID("BurstCount");
    static private readonly int particlesPerBurstID = Shader.PropertyToID("particlesPerBurst");
    static private readonly int DelayBetweenBurstsID = Shader.PropertyToID("DelayBetweenBursts");

    public sealed override void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
    {
        state.delayAfterLoop = vfxValues.GetFloat(DelayBetweenBurstsID);
        state.loopCount = vfxValues.GetInt(BurstCountID);
        state.loopIndex = 0;
    }



    public sealed override void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
    {
        Debug.Log(state.loopState);
        if(state.loopState == VFXSpawnerLoopState.Looping)
        {
            state.spawnCount+= vfxValues.GetInt(particlesPerBurstID);

            state.delayAfterLoop = vfxValues.GetFloat(DelayBetweenBurstsID);
            state.loopState = VFXSpawnerLoopState.DelayingAfterLoop;
        }
    }

    public sealed override void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
    {
        state.loopState = VFXSpawnerLoopState.Finished;
    }
}
