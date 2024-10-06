using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using System.IO; // Only for file-saving in the Editor
#endif

public class WebCSVClick : MonoBehaviour
{
    public Button downloadButton;

    private List<CelestialObject> celestialObjects;

    void Start()
    {
        if (downloadButton != null)
        {
            downloadButton.onClick.AddListener(DownloadCSV);
        }
        else
        {
            Debug.LogError("Download button not assigned in the Inspector!");
        }
    }

    public void DownloadCSV()
    {
        GetResults();

        if (celestialObjects == null || celestialObjects.Count == 0)
        {
            Debug.LogWarning("No celestial objects to export.");
            return;
        }

        string csv = GenerateCSV();

        #if UNITY_WEBGL
        // Use data URL download in WebGL
        DownloadInWebGL(csv, "keplerian_parameters.csv");
        #else
        // Use file-saving in Editor (or other platforms) for testing
        SaveCSVInEditor(csv, "keplerian_parameters.csv");
        #endif
    }

    void GetResults()
    {
        celestialObjects = new List<CelestialObject>();

        CelestialObject obj1 = new CelestialObject
        {
            Eccentricity = 0.223,
            SemiMajorAxis = 1.458, 
            Inclination = 10.83, 
            ArgumentOfPeriapsis = 178.9,
            MeanAnomaly = 73.3,
            LongitudeOfAscendingNode = 304.4
        };

        celestialObjects.Add(obj1);
        Debug.Log("Generated Keplerian Parameters for Celestial Object(s).");
    }

    string GenerateCSV()
    {
        StringBuilder csv = new StringBuilder();
        csv.AppendLine("Eccentricity,SemiMajorAxis,Inclination,ArgumentOfPeriapsis,MeanAnomaly,LongitudeOfAscendingNode");

        foreach (var obj in celestialObjects)
        {
            csv.AppendLine($"{obj.Eccentricity},{obj.SemiMajorAxis},{obj.Inclination},{obj.ArgumentOfPeriapsis},{obj.MeanAnomaly},{obj.LongitudeOfAscendingNode}");
        }

        return csv.ToString();
    }

    void DownloadInWebGL(string csvContent, string fileName)
    {
        byte[] csvBytes = Encoding.UTF8.GetBytes(csvContent);
        string base64String = System.Convert.ToBase64String(csvBytes);
        string dataURI = "data:text/csv;base64," + base64String;
        Application.OpenURL(dataURI);
        Debug.Log("Download triggered for: " + fileName);
    }

    #if !UNITY_WEBGL && UNITY_EDITOR
    // For testing in the Unity Editor, save the file locally
    void SaveCSVInEditor(string csvContent, string fileName)
    {
        string path = Path.Combine(Application.dataPath, fileName);
        File.WriteAllText(path, csvContent);
        Debug.Log($"CSV saved to: {path}");
    }
    #endif
}

public class CelestialObject
{
    public double Eccentricity { get; set; }
    public double SemiMajorAxis { get; set; }
    public double Inclination { get; set; }
    public double ArgumentOfPeriapsis { get; set; }
    public double MeanAnomaly { get; set; }
    public double LongitudeOfAscendingNode { get; set; }
}