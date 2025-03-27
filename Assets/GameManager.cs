using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int stage = 1; // Track the current stage

    private void Awake()
    {
        Debug.Log($"Stage {stage}");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void NextStage()
    {
        stage++;
        string nextScene = SceneManager.GetActiveScene().name == "SceneA" ? "SceneB" : "SceneA";
        SceneManager.LoadScene(nextScene);
    }

}
