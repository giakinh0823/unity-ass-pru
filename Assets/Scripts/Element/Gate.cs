namespace Element
{
    using System;
    using Model;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Gate : MonoBehaviour
    {
        private bool isGateOpen;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                var questController = FindObjectOfType<QuestPlayerController>();
                if (questController is { IsReadyToUse: true, IsQuestCompleted: true })
                {
                    this.OnOpenGate();
                }
            }
        }

        private void OnOpenGate()
        {
            if (this.isGateOpen)
            {
                return;
            }

            this.isGateOpen = true;

            PlayerLocalData.Instance.CurrentPlayerLevel++;
            PlayerLocalData.Instance.Save();
            Debug.Log($"Open Gate: {PlayerLocalData.Instance.CurrentPlayerLevel}");
            SceneManager.LoadScene("Game");
        }
    }
}