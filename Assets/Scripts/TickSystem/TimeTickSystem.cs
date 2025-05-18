using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeTickSystem
{
    public class OnTickEventArgs : EventArgs
    {
        public int tick;
        public float tickDeltaTime;
    }

    public static event EventHandler<OnTickEventArgs> OnTick;
    public static event EventHandler<OnTickEventArgs> OnTick_4;

    private const float TICK_TIMER_MAX = .2f;

    private static GameObject timeTickSystemGameObject;
    private static int tick;

    public static void Create()
    {
        if (timeTickSystemGameObject == null)
        {
            timeTickSystemGameObject = new GameObject("TimeTickSystem");
            timeTickSystemGameObject.AddComponent<TimeTickSystemObject>();
        }
    }

    private class TimeTickSystemObject : MonoBehaviour
    {
        private float tickTimer;
        private float previousTickTime;

        private void Awake()
        {
            tick = 0;
            previousTickTime = Time.time;
        }

        private void Update()
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= TICK_TIMER_MAX)
            {
                tickTimer -= TICK_TIMER_MAX;
                tick++;

                float currentTime = Time.time;
                float tickDeltaTime = currentTime - previousTickTime;
                previousTickTime = currentTime;

                OnTick?.Invoke(this, new OnTickEventArgs { tick = tick, tickDeltaTime = tickDeltaTime });

                if (tick % 4 == 0)
                {
                    OnTick_4?.Invoke(this, new OnTickEventArgs { tick = tick, tickDeltaTime = tickDeltaTime });
                }
            }
        }
    }
}
