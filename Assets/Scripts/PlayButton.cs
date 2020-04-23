using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private GameManager gameManager;
    private Button playButton;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        playButton = GetComponent<Button>();

        playButton.onClick.AddListener(gameManager.LoadGameLevel);
    }
}
