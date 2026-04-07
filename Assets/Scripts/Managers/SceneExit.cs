using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class SceneExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Name of the scene in Build Settings

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the player (tag your player as "Player")
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
///this script made with AI help (Gemini)
