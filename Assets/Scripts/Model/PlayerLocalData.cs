namespace Model
{
    using Common;

    public class PlayerLocalData : BaseLocalData<PlayerLocalData>
    {
        public int CurrentPlayerLevel      { get; set; }
        public int CurrentPlayerReviveTime { get; set; }

        public override void Init()
        {
            this.CurrentPlayerLevel      = 1;
            this.CurrentPlayerReviveTime = 5;
            base.Init();
        }
    }
}