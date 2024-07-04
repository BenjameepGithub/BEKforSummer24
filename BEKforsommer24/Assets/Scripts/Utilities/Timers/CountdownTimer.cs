using UnityEngine;

namespace Utilities.Timers {
    public class CountdownTimer : Timer {
        public CountdownTimer(float value) : base(value) { }
        
        public override void Tick() {
            base.Tick();
            if (IsRunning && RemainingTime > 0) {
                RemainingTime -= Time.deltaTime;
            }
            
            if (IsRunning && RemainingTime <= 0) {
                StopAndDispose();
            }
        }
        
        public override bool IsFinished => RemainingTime <= 0;
    }
}