using System;
using UnityEngine;

namespace Utilities.Timers {
    public abstract class Timer : IDisposable {
        public float RemainingTime { get; protected set; }
        public bool IsRunning { get; private set; }
        
        protected float InitialTime;
        
        public float Progress => Mathf.Clamp(RemainingTime / InitialTime, 0, 1);
        
        public event Action OnTimerStart = delegate { };
        public event Action OnTimerStop = delegate { };
        public event Action OnTimerTick = delegate { };
        
        private bool _disposed;
        
        protected Timer(float value) {
            InitialTime = value;
        }
        
        public void Start() {
            RemainingTime = InitialTime;
            if (!IsRunning) {
                IsRunning = true;
                TimerManager.RegisterTimer(this);
                OnTimerStart.Invoke();
            }
        }
        
        public void Stop() {
            if (!IsRunning) return;
            IsRunning = false;
            TimerManager.DeregisterTimer(this);
            OnTimerStop.Invoke();
        }
        
        public void StopAndDispose() {
            Stop();
            Dispose();
        }
        
        /// <summary>
        /// NB! Call base.Tick(); to invoke the OnTimerTick delegate
        /// </summary>
        public virtual void Tick() {
            OnTimerTick.Invoke();
        }
        
        public abstract bool IsFinished { get; }
        
        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;
        
        public virtual void Reset() => RemainingTime = InitialTime;
        public virtual void Reset(float newTime) {
            InitialTime = newTime;
            Reset();
        }
        
        ~Timer() {
            Dispose(false);
        }
        
        public void Dispose() {
            Debug.Log("*** Disposed Timer ***");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (_disposed) return;

            if (disposing) {
                TimerManager.DeregisterTimer(this);
            }
            
            _disposed = true;
        }
    }
}