namespace ScreenManager.Popups
{
    using global::ScreenManager.Components;
    using Model;
    using TMPro;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class ShopPopup : BasePopup
    {
        [SerializeField]
        private ShopItem[] shopItems;

        [SerializeField]
        private TMP_Text currentCoin;

        public override void Init()
        {
            base.Init();
            this.shopItems[0].OnBuy.AddListener(this.OnBuyHand);
            this.shopItems[1].OnBuy.AddListener(this.OnBuySword);
            this.shopItems[2].OnBuy.AddListener(this.OnBuyGun);
        }

        private void UpdateCurrentCoin()
        {
            this.currentCoin.text = PlayerLocalData.Instance.CurrentCoin.ToString();
        }

        private void OnBuyGun()
        {
            var currentCoin = PlayerLocalData.Instance.CurrentCoin;
            if (currentCoin >= this.shopItems[2].Price)
            {
                PlayerLocalData.Instance.BuffGunDamage += 0.03f;
                PlayerLocalData.Instance.CurrentCoin   -= this.shopItems[2].Price;
                this.shopItems[2].SliderValue          =  PlayerLocalData.Instance.BuffGunDamage;
                this.UpdateCurrentCoin();
            }
        }

        private void OnBuySword()
        {
            var currentCoin = PlayerLocalData.Instance.CurrentCoin;
            if (currentCoin >= this.shopItems[1].Price)
            {
                PlayerLocalData.Instance.BuffSwordDamage += 0.03f;
                PlayerLocalData.Instance.CurrentCoin     -= this.shopItems[1].Price;
                this.shopItems[1].SliderValue            =  PlayerLocalData.Instance.BuffSwordDamage;
                this.UpdateCurrentCoin();
            }
        }

        private void OnBuyHand()
        {
            var currentCoin = PlayerLocalData.Instance.CurrentCoin;
            if (currentCoin >= this.shopItems[0].Price)
            {
                PlayerLocalData.Instance.BuffHandDamage += 0.015f;
                PlayerLocalData.Instance.CurrentCoin    -= this.shopItems[0].Price;
                this.shopItems[0].SliderValue           =  PlayerLocalData.Instance.BuffHandDamage;
                this.UpdateCurrentCoin();
            }
        }

        public override void OnShow()
        {
            base.OnShow();
            Time.timeScale = 0;

            this.BindData();
        }

        public override void OnHide()
        {
            base.OnHide();
            Time.timeScale = 1;

            SceneManager.LoadScene("Game");
        }

        private void BindData()
        {
            this.shopItems[0].Price = 1;
            this.shopItems[1].Price = 2;
            this.shopItems[2].Price = 3;

            this.shopItems[0].SliderValue = PlayerLocalData.Instance.BuffHandDamage;
            this.shopItems[1].SliderValue = PlayerLocalData.Instance.BuffSwordDamage;
            this.shopItems[2].SliderValue = PlayerLocalData.Instance.BuffGunDamage;

            this.UpdateCurrentCoin();
        }
    }
}