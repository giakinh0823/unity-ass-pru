namespace ScreenManager.Components
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShopItem : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text name;

        [SerializeField]
        private Slider slider;

        [SerializeField]
        private Button upgradeButton;

        [SerializeField]
        private TMP_Text price;


        public string Name
        {
            set => this.name.text = value;
        }

        public float SliderValue
        {
            set => this.slider.value = value;
        }

        public int Price
        {
            set => this.price.text = value.ToString();
            get => int.Parse(this.price.text);
        }

        public Button.ButtonClickedEvent OnBuy => this.upgradeButton.onClick;
    }
}