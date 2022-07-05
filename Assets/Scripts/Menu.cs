using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class Menu : MonoBehaviour
    {
        public void Replay()
        {
            FindObjectOfType<GameManager>().Reset();
        }

        public void QuitGame()
        {
            // Mematikan Game saat dalam editor
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif

            // Mematikan Game setelah game di build
            Application.Quit();
        }
    }
}