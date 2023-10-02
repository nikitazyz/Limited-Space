using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _levelSelection;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _homeButton;

    private void Awake()
    {
        _mainMenu.SetActive(true);
        _levelSelection.SetActive(false);
        
        _playButton.onClick.AddListener(() =>
        {
            _mainMenu.SetActive(false);
            _levelSelection.SetActive(true);
        });
        
        _homeButton.onClick.AddListener(() =>
        {
            _mainMenu.SetActive(true);
            _levelSelection.SetActive(false);
        });
    }
}
