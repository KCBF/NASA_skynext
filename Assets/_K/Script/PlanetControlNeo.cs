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
    public float globalOrbitOffset = 400f;  // Adjustable in the inspector to push planets further from the sun

    // Sun scaling factor (adjusted to 1090 for the Sun)
    private const float sunScaleFactor = 1090f;

    private bool orbitsVisible = true;

    void Start()
    {
        // Set the Sun's size
        sun.localScale = Vector3.one * sunScaleFactor;

        // Initialize planet data based on real values
        InitializePlanets();

        // Set initial positions, rotations for planets
        SetInitialPositions(mercury, 3.83f, 0.034f);
        SetInitialPositions(venus, 9.52f, 177.4f);
        SetInitialPositions(earth, 10f, 23.5f);
        SetInitialPositions(mars, 5.32f, 25.19f);
        SetInitialPositions(jupiter, 109.11f, 3.13f);
        SetInitialPositions(saturn, 91.5f, 26.73f);
        SetInitialPositions(uranus, 39.72f, 97.77f);
        SetInitialPositions(neptune, 39.46f, 28.32f);

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
        // Sizes based on NASA data, relative to Earth (which is set to 10 units)
        mercury.planetScale = 3.83f;
        venus.planetScale = 9.52f;
        earth.planetScale = 10f;
        mars.planetScale = 5.32f;
        jupiter.planetScale = 109.11f;
        saturn.planetScale = 91.5f;
        uranus.planetScale = 39.72f;
        neptune.planetScale = 39.46f;

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

    void SetInitialPositions(Planet planet, float scale, float tilt)
    {
        // Set the initial position for the planet based on its orbit radius
        float orbitRadiusWithOffset = planet.orbitRadius;
        planet.planetTransform.position = new Vector3(orbitRadiusWithOffset, 0, 0);

        // Set axial tilt
        planet.planetTransform.rotation = Quaternion.Euler(tilt, 0, 0);

        // Set the scale of the planet
        planet.planetTransform.localScale = Vector3.one * scale;
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

        // Update visibility of all planet orbits (visual feedback can be implemented here if needed)
    }
}