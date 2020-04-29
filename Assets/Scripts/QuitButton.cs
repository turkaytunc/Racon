using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private GameManager gameManager;
    private Button playButton;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playButton = GetComponent<Button>();
        playButton.onClick.AddListener(gameManager.QuitGame);
    }

}
