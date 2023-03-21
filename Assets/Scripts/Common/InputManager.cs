namespace Common
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; set; }
        
        public Action jump;

        public Action attack;

        public Action changeWeapon;

        [SerializeField]
        private Button jumpButton;

        [SerializeField]
        private Button attackButton;

        [SerializeField]
        private Button changeWeaponButton;

        private void Awake()
        {
            Instance = this;
            this.jumpButton.onClick.AddListener(() => this.jump?.Invoke());
            this.attackButton.onClick.AddListener(() => this.attack?.Invoke());
            this.changeWeaponButton.onClick.AddListener(() => this.changeWeapon?.Invoke());
        }
    }
}