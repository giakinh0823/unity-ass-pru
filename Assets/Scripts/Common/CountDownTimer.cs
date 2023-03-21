namespace Common
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    public class CountDownTimer : MonoBehaviour
    {
        [SerializeField] private float initTime = 0f;

        [Space(20)] [SerializeField] private UnityEvent<bool, float> onUpdate;

        [Space(20)] [SerializeField] private UnityEvent onTimeout;

        public float InitTime
        {
            get => this.initTime;
            set => this.initTime = value;
        }

        public float TimeLeft { get; set; }

        private bool IsRunning { get; set; }

        public void Reset()
        {
            this.IsRunning = false;
            this.TimeLeft  = this.InitTime;
        }

        public void Start()
        {
            this.Reset();
            this.IsRunning = true;
        }

        public void Update()
        {
            this.onUpdate?.Invoke(this.IsRunning, this.TimeLeft);
            
            if (this.TimeLeft <= 0 || !this.IsRunning) return;
            this.TimeLeft -= Time.deltaTime;
            
            if (this.TimeLeft <= 0)
            {
                this.IsRunning = false;
                this.onTimeout?.Invoke();
            }
        }

    }
}