namespace Element
{
    using System;
    using Model;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Gate : MonoBehaviour
    {
        [SerializeField] private float stayTime = 2f;

        private float stayTimer;
        private bool  isTriggered;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (this.isTriggered) return;

            if (this.stayTimer < this.stayTime)
            {
                this.stayTimer += Time.deltaTime;
            }
            else
            {
                this.OnOpenGate();
                this.isTriggered = true;
            }
        }

        private void OnOpenGate()
        {
            PlayerLocalData.Instance.CurrentPlayerLevel++;
            PlayerLocalData.Instance.Save();
            Debug.Log($"Open Gate: {PlayerLocalData.Instance.CurrentPlayerLevel}");
            SceneManager.LoadScene("Game");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            this.stayTimer = 0f;
        }
    }
}