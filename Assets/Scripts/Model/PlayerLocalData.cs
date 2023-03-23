namespace Model
{
    using Common;
    using ScreenManager.Screens;
    using UnityEngine;

    public class PlayerLocalData : BaseLocalData<PlayerLocalData>
    {
        public int HighestLevel { get; set; }

        private int currentPlayerLevel;

        public int CurrentPlayerLevel
        {
            get => this.currentPlayerLevel;
            set
            {
                this.currentPlayerLevel = value;
                if (this.currentPlayerLevel > this.HighestLevel)
                {
                    this.HighestLevel = this.currentPlayerLevel;
                }
            }
        }

        public int CurrentPlayerReviveTime { get; set; }

        private int currentCoin;

        public int CurrentCoin
        {
            get => this.currentCoin;
            set
            {
                this.currentCoin = value;
                Object.FindObjectOfType<GameplayScreen>()?.CoinUpdate();
            }
        }

        public override void Init()
        {
            this.CurrentPlayerLevel      = 1;
            this.CurrentCoin             = 0;
            this.CurrentPlayerReviveTime = 5;
            base.Init();
        }
    }
}