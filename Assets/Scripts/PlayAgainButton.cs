using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAgainButton : MonoBehaviour
{
    private GameManager gameManager;
    private Button playAgainButton;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        playAgainButton = GetComponent<Button>();

        playAgainButton.onClick.AddListener(gameManager.LoadMainMenu);
    }
}
