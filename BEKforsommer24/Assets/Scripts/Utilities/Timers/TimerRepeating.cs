using UnityEngine;

namespace Utilities.Timers {
    public class TimerRepeating : Timer {
        public TimerRepeating(float value) : base(value) { }

        public override void Tick() {
            if (!IsRunning) return;
            base.Tick();
            
            if (RemainingTime - Time.deltaTime > 0) {
                RemainingTime -= Time.deltaTime;
            }
            else {
                Stop();
                Start();
            }
        }
        
        public override bool IsFinished => RemainingTime <= 0;
    }
}