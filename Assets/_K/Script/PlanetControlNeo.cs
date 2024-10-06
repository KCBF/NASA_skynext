using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlanetControlNeo : MonoBehaviour
{
    [System.Serializable]
    public class Planet
    {
        public Transform planetTransform;     // Drag the planet object here
        public float orbitSpeed = 1f;         // Orbit speed in degrees per second
        public float selfRotationSpeed = 15f; // Speed of the planet's self-rotation
        public float tiltAngle = 23.5f;       // Axial tilt angle of the planet
        public float planetScale;             // Scale of the planet based on NASA data
        public float orbitRadius;             // Average distance from the sun (scaled appropriately)
        public float eccentricity = 0f;       // Eccentricity of the orbit, for elliptical orbits
        public Color orbitRingColor = Color.white; // Color of the orbit ring
    }

    // Planets
    public Planet mercury;
    public Planet venus;
    public Planet earth;
    public Planet mars;
    public Planet jupiter;
    public Planet saturn;
    public Planet uranus;
    public Planet neptune;

    public Transform sun; // Drag the Sun object here
    public Button toggleOrbitVisibilityButton; // Drag and drop the TextMeshPro button here

    // Global offset for orbit distance
    public float globalOrbitOffset = 0f;  // Adjustable in the inspector to push planets further from the sun

    // Sun scaling factor (10900 for the Sun)
    private const float sunScaleFactor = 10900f;

    private bool orbitsVisible = true;
    private List<GameObject> orbitRings = new List<GameObject>();

    void Start()
    {
        // Set the Sun's size
        sun.localScale = Vector3.one * sunScaleFactor;

        // Initialize planet data based on real values
        InitializePlanets();

        // Set initial positions, rotations, and draw orbit rings for planets
        SetInitialPositionsAndDrawOrbits(mercury, 38.31f, 0.034f);
        SetInitialPositionsAndDrawOrbits(venus, 95.23f, 177.4f);
        SetInitialPositionsAndDrawOrbits(earth, 100f, 23.5f);
        SetInitialPositionsAndDrawOrbits(mars, 53.18f, 25.19f);
        SetInitialPositionsAndDrawOrbits(jupiter, 1091.07f, 3.13f);
        SetInitialPositionsAndDrawOrbits(saturn, 915.02f, 26.73f);
        SetInitialPositionsAndDrawOrbits(uranus, 397.19f, 97.77f);
        SetInitialPositionsAndDrawOrbits(neptune, 394.62f, 28.32f);

        // Set up button listener for toggling orbit visibility
        if (toggleOrbitVisibilityButton != null)
        {
            toggleOrbitVisibilityButton.onClick.AddListener(ToggleOrbitVisibility);
        }
    }

    void Update()
    {
        // Update orbit and self-rotation for all planets
        UpdatePlanetOrbitAndRotation(mercury);
        UpdatePlanetOrbitAndRotation(venus);
        UpdatePlanetOrbitAndRotation(earth);
        UpdatePlanetOrbitAndRotation(mars);
        UpdatePlanetOrbitAndRotation(jupiter);
        UpdatePlanetOrbitAndRotation(saturn);
        UpdatePlanetOrbitAndRotation(uranus);
        UpdatePlanetOrbitAndRotation(neptune);
    }

    void InitializePlanets()
    {
        // Sizes based on NASA data, relative to Earth (which is set to 100 units)
        mercury.planetScale = 38.31f;
        venus.planetScale = 95.23f;
        earth.planetScale = 100f;
        mars.planetScale = 53.18f;
        jupiter.planetScale = 1091.07f;
        saturn.planetScale = 915.02f;
        uranus.planetScale = 397.19f;
        neptune.planetScale = 394.62f;

        // Orbit radii based on NASA distances from the Sun (AU) scaled to Unity units, plus the global offset
        mercury.orbitRadius = (0.387f * sunScaleFactor) + globalOrbitOffset;
        venus.orbitRadius = (0.723f * sunScaleFactor) + globalOrbitOffset;
        earth.orbitRadius = (1.0f * sunScaleFactor) + globalOrbitOffset;
        mars.orbitRadius = (1.524f * sunScaleFactor) + globalOrbitOffset;
        jupiter.orbitRadius = (5.203f * sunScaleFactor) + globalOrbitOffset;
        saturn.orbitRadius = (9.537f * sunScaleFactor) + globalOrbitOffset;
        uranus.orbitRadius = (19.191f * sunScaleFactor) + globalOrbitOffset;
        neptune.orbitRadius = (30.068f * sunScaleFactor) + globalOrbitOffset;

        // Update orbit and self-rotation speeds
        UpdateOrbitAndRotationSpeeds();
    }

    void UpdateOrbitAndRotationSpeeds()
    {
        // Assign scientifically accurate orbit speeds and self-rotation speeds
        mercury.orbitSpeed = 360f / 88f;        // Completes one orbit in 88 Earth days
        mercury.selfRotationSpeed = 360f / 58.65f;

        venus.orbitSpeed = 360f / 225f;         // Completes one orbit in 225 Earth days
        venus.selfRotationSpeed = 360f / -243f; // Retrograde rotation

        earth.orbitSpeed = 360f / 365.25f;      // Completes one orbit in 365.25 Earth days
        earth.selfRotationSpeed = 360f / 1f;

        mars.orbitSpeed = 360f / 687f;          // Completes one orbit in 687 Earth days
        mars.selfRotationSpeed = 360f / 1.03f;

        jupiter.orbitSpeed = 360f / 4333f;      // Completes one orbit in 4333 Earth days
        jupiter.selfRotationSpeed = 360f / 0.41f;

        saturn.orbitSpeed = 360f / 10759f;      // Completes one orbit in 10759 Earth days
        saturn.selfRotationSpeed = 360f / 0.45f;

        uranus.orbitSpeed = 360f / 30687f;      // Completes one orbit in 30687 Earth days
        uranus.selfRotationSpeed = 360f / -0.72f; // Retrograde rotation

        neptune.orbitSpeed = 360f / 60190f;     // Completes one orbit in 60190 Earth days
        neptune.selfRotationSpeed = 360f / 0.67f;
    }

    void SetInitialPositionsAndDrawOrbits(Planet planet, float scale, float tilt)
    {
        // Set the initial position for the planet based on its orbit radius
        float orbitRadiusWithOffset = planet.orbitRadius + globalOrbitOffset;
        planet.planetTransform.position = new Vector3(orbitRadiusWithOffset, 0, 0);

        // Set axial tilt
        planet.planetTransform.rotation = Quaternion.Euler(tilt, 0, 0);

        // Set the scale of the planet
        planet.planetTransform.localScale = Vector3.one * scale;

        // Draw the orbit ring
        DrawOrbitRing(planet);
    }

    void DrawOrbitRing(Planet planet)
    {
        // Create an empty GameObject for the orbit ring
        GameObject orbitRing = new GameObject(planet.planetTransform.name + "_OrbitRing");
        orbitRing.transform.position = sun.position;

        // Add LineRenderer component
        LineRenderer lineRenderer = orbitRing.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = true;
        lineRenderer.positionCount = 100;
        lineRenderer.startWidth = 50f;
        lineRenderer.endWidth = 50f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = planet.orbitRingColor };
        lineRenderer.startColor = planet.orbitRingColor;
        lineRenderer.endColor = planet.orbitRingColor;

        // Draw the orbit ring considering eccentricity for elliptical orbits
        float angle = 0f;
        Vector3[] positions = new Vector3[100];
        float semiMajorAxis = planet.orbitRadius + globalOrbitOffset;
        float semiMinorAxis = semiMajorAxis * (1f - planet.eccentricity);

        for (int i = 0; i < 100; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * semiMajorAxis;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * semiMinorAxis;

            positions[i] = new Vector3(x, 0, z);
            angle += 360f / 100;
        }

        lineRenderer.SetPositions(positions);

        // Add the orbit ring to the list for toggling visibility later
        orbitRings.Add(orbitRing);
    }

    void UpdatePlanetOrbitAndRotation(Planet planet)
    {
        // Calculate orbit speed per frame
        float orbitSpeedPerFrame = planet.orbitSpeed * Time.timeScale * Time.deltaTime;

        // Orbit around the Sun (assuming the Sun is at the origin)
        planet.planetTransform.RotateAround(sun.position, Vector3.up, orbitSpeedPerFrame);

        // Self-rotation around its own axis
        float rotationSpeedPerFrame = planet.selfRotationSpeed * Time.deltaTime * Time.timeScale;
        planet.planetTransform.Rotate(Vector3.up, rotationSpeedPerFrame);
    }

    void ToggleOrbitVisibility()
    {
        orbitsVisible = !orbitsVisible;

        // Toggle visibility of all orbit rings
        foreach (GameObject orbitRing in orbitRings)
        {
            orbitRing.SetActive(orbitsVisible);
        }
    }
}