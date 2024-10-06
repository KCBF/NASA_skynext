// SolarSystemManager.cs - Attach this script to an empty GameObject in the SolarSystem scene
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SolarSystemManager : MonoBehaviour
{
    public Button sunButton;
    public Button mercuryButton;
    public Button venusButton;
    public Button earthButton;
    public Button marsButton;
    public Button jupiterButton;
    public Button saturnButton;
    public Button uranusButton;
    public Button neptuneButton;
    public TMP_Text progressText;  // Assign TextMeshPro Text to display progress
    public Button resetButton;     // Assign Reset Experience button in inspector

    public string sunSceneName;
    public string mercurySceneName;
    public string venusSceneName;
    public string earthSceneName;
    public string marsSceneName;
    public string jupiterSceneName;
    public string saturnSceneName;
    public string uranusSceneName;
    public string neptuneSceneName;

    private bool[] celestialBodiesVisited = new bool[9];
    private int totalCelestialBodies = 9;
    private int celestialBodiesVisitedCount = 0;

    void Start()
    {
        LoadVisitedStatus();
        UpdateProgressText();
        AssignButtonListeners();
        resetButton.onClick.AddListener(ResetExperience);
    }

    void AssignButtonListeners()
    {
        sunButton.onClick.AddListener(() => OnCelestialButtonClick(0, sunSceneName));
        mercuryButton.onClick.AddListener(() => OnCelestialButtonClick(1, mercurySceneName));
        venusButton.onClick.AddListener(() => OnCelestialButtonClick(2, venusSceneName));
        earthButton.onClick.AddListener(() => OnCelestialButtonClick(3, earthSceneName));
        marsButton.onClick.AddListener(() => OnCelestialButtonClick(4, marsSceneName));
        jupiterButton.onClick.AddListener(() => OnCelestialButtonClick(5, jupiterSceneName));
        saturnButton.onClick.AddListener(() => OnCelestialButtonClick(6, saturnSceneName));
        uranusButton.onClick.AddListener(() => OnCelestialButtonClick(7, uranusSceneName));
        neptuneButton.onClick.AddListener(() => OnCelestialButtonClick(8, neptuneSceneName));
    }

    void OnCelestialButtonClick(int index, string sceneName)
    {
        if (!celestialBodiesVisited[index])
        {
            celestialBodiesVisited[index] = true;
            celestialBodiesVisitedCount++;
            GetButtonByIndex(index).GetComponent<Image>().color = Color.green;
            UpdateProgressText();
            SaveVisitedStatus();
        }
        SceneManager.LoadScene(sceneName);
    }

    Button GetButtonByIndex(int index)
    {
        switch (index)
        {
            case 0: return sunButton;
            case 1: return mercuryButton;
            case 2: return venusButton;
            case 3: return earthButton;
            case 4: return marsButton;
            case 5: return jupiterButton;
            case 6: return saturnButton;
            case 7: return uranusButton;
            case 8: return neptuneButton;
            default: return null;
        }
    }

    void UpdateProgressText()
    {
        progressText.text = celestialBodiesVisitedCount + "/" + totalCelestialBodies;
    }

    void ResetExperience()
    {
        celestialBodiesVisitedCount = 0;
        for (int i = 0; i < celestialBodiesVisited.Length; i++)
        {
            celestialBodiesVisited[i] = false;
            GetButtonByIndex(i).GetComponent<Image>().color = Color.red;
        }
        UpdateProgressText();
        SaveVisitedStatus();
    }

    void SaveVisitedStatus()
    {
        for (int i = 0; i < celestialBodiesVisited.Length; i++)
        {
            PlayerPrefs.SetInt("CelestialVisited" + i, celestialBodiesVisited[i] ? 1 : 0);
        }
        PlayerPrefs.SetInt("CelestialVisitedCount", celestialBodiesVisitedCount);
        PlayerPrefs.Save();
    }

    void LoadVisitedStatus()
    {
        celestialBodiesVisitedCount = PlayerPrefs.GetInt("CelestialVisitedCount", 0);
        for (int i = 0; i < celestialBodiesVisited.Length; i++)
        {
            celestialBodiesVisited[i] = PlayerPrefs.GetInt("CelestialVisited" + i, 0) == 1;
            GetButtonByIndex(i).GetComponent<Image>().color = celestialBodiesVisited[i] ? Color.green : Color.red;
        }
    }
}