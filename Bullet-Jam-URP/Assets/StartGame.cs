using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    Button startBtn;
    // Start is called before the first frame update
    void Start()
    {
        startBtn = GetComponent<Button>();
        startBtn.onClick.AddListener(StartTheGame);
    }

    private void StartTheGame()
    {
        SceneManager.LoadScene(1);
    }
}
