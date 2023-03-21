namespace Model
{
    using Common;
    using ScreenManager.Screens;
    using UnityEngine;

    public class PlayerLocalData : BaseLocalData<PlayerLocalData>
    {
        public int CurrentPlayerLevel      { get; set; }
        public int CurrentPlayerReviveTime { get; set; }

        private int _currentCoin;

        public int CurrentCoin
        {
            get => this._currentCoin;
            set
            {
                this._currentCoin = value;
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