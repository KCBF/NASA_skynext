// PlanetSceneManager.cs - Attach this script to an empty GameObject in each planet scene (Sun, Mars, Mercury, etc.)
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlanetSceneManager : MonoBehaviour
{
    public Button backButton; // Assign the Back button in inspector

    void Start()
    {
        backButton.onClick.AddListener(GoBackToSolarSystem);
    }

    void GoBackToSolarSystem()
    {
        SceneManager.LoadScene("001_SolarSystem");
    }
}