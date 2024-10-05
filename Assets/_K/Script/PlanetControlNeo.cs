using UnityEngine;

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
        public float orbitRadius;             // Distance from the sun (scaled appropriately)
        public GameObject orbitRingPrefab;    // Orbit ring prefab for visual orbit path
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

    // Global offset for orbit distance
    public float globalOrbitOffset = 0f;  // Adjustable in the inspector to push planets further from the sun

    // Time scale adjustment for the whole scene
    public float timeScaleIncrement = 0.1f;
    public float minTimeScale = 0.1f;     // Lower limit for time scale
    public float maxTimeScale = 10f;      // Upper limit for time scale

    // Orbit ring appearance control
    public Color orbitRingColor = Color.white;
    public float orbitRingThickness = 0.05f;

    // Sun scaling factor (10900 for the Sun)
    private const float sunScaleFactor = 10900f;

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
    }

    void Update()
    {
        // Handle time scale changes with X and Z keys
        HandleTimeScaleChange();

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
        planet.planetTransform.position = new Vector3(planet.orbitRadius + (sun.localScale.x / 2), 0, 0);

        // Set axial tilt
        planet.planetTransform.rotation = Quaternion.Euler(tilt, 0, 0);

        // Set the scale of the planet
        planet.planetTransform.localScale = Vector3.one * scale;

        // Draw the orbit ring
        DrawOrbitRing(planet);
    }

    void DrawOrbitRing(Planet planet)
    {
        // Instantiate the orbit ring as a prefab
        if (planet.orbitRingPrefab != null)
        {
            GameObject orbitRing = Instantiate(planet.orbitRingPrefab, sun.position, Quaternion.identity);
            orbitRing.transform.localScale = new Vector3(planet.orbitRadius * 2, orbitRingThickness, planet.orbitRadius * 2);

            // Adjust the color and thickness of the orbit ring
            Renderer ringRenderer = orbitRing.GetComponent<Renderer>();
            if (ringRenderer != null)
            {
                ringRenderer.material.color = orbitRingColor;
            }
        }
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

    void HandleTimeScaleChange()
    {
        // Increase time scale with the X key
        if (Input.GetKeyDown(KeyCode.X) && Time.timeScale < maxTimeScale)
        {
            Time.timeScale += timeScaleIncrement;  // Increase the time scale
        }

        // Decrease time scale with the Z key, but ensure it doesn't go below minTimeScale
        if (Input.GetKeyDown(KeyCode.Z) && Time.timeScale > minTimeScale)
        {
            Time.timeScale -= timeScaleIncrement;  // Decrease the time scale
        }
    }
}
