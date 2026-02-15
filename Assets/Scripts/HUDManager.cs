using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    public static HUDManager instance;

    private void Awake()
    {
        Time.timeScale = 0;

        instance = this;
    }

    public void OnStartClicked()
    {
        Time.timeScale = 1f;
        mainMenu.SetActive(false);

    }
    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        // Stop playing the scene (stops Play Mode)
        EditorApplication.isPlaying = false;
        // If running in a built game
#else
            // Quit the application
            Application.Quit();
#endif

    }

    public void OnShipDestroyed()
    {
        StartCoroutine(RestartLevelRoutine());
    }
    private IEnumerator RestartLevelRoutine()
    {
        while (true) // Infinite loop to keep spawning
        {
            
            yield return new WaitForSeconds(3);
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }


}
