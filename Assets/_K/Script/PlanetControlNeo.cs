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

    // Planets and moons
    public Planet mercury;
    public Planet venus;
    public Planet earth;
    public Planet moon;
    public Planet mars;
    public Planet phobos;
    public Planet deimos;
    public Planet jupiter;
    public Planet io;
    public Planet europa;
    public Planet ganymede;
    public Planet callisto;
    public Planet saturn;
    public Planet titan;
    public Planet rhea;
    public Planet iapetus;
    public Planet dione;
    public Planet tethys;
    public Planet uranus;
    public Planet miranda;
    public Planet ariel;
    public Planet umbriel;
    public Planet neptune;
    public Planet triton;

    public Transform sun; // Drag the Sun object here

    // Global offset for orbit distance
    public float globalOrbitOffset = 0f;  // Adjustable in the inspector to push planets and moons further from the sun

    // Adjustable initial orbit speed for all planets
    public float initialOrbitSpeed = 1f;

    // Orbit speed control via key input (multiplier)
    private float currentOrbitSpeedMultiplier = 1f;

    // Orbit ring appearance control
    public Color orbitRingColor = Color.white;
    public float orbitRingThickness = 0.05f;

    // Sun scaling factor (10900 for the Sun)
    private const float sunScaleFactor = 10900f;
    
    // Scale ratio based on Earth
    private const float earthScale = 100f;

    void Start()
    {
        // Set the Sun's size
        sun.localScale = Vector3.one * sunScaleFactor;

        // Initialize planet data based on real values
        InitializePlanets();

        // Set initial positions, rotations, and draw orbit rings for planets and moons
        SetInitialPositionsAndDrawOrbits(mercury, 38.31f, 0.034f);
        SetInitialPositionsAndDrawOrbits(venus, 95.23f, 177.4f);
        SetInitialPositionsAndDrawOrbits(earth, 100f, 23.5f);
        SetInitialPositionsAndDrawOrbits(moon, 27.27f, 27.43f);
        SetInitialPositionsAndDrawOrbits(mars, 53.18f, 25.19f);
        SetInitialPositionsAndDrawOrbits(phobos, 0.17f, 0.01325f);
        SetInitialPositionsAndDrawOrbits(deimos, 0.097f, 0.05259f);
        SetInitialPositionsAndDrawOrbits(jupiter, 1091.07f, 3.13f);
        SetInitialPositionsAndDrawOrbits(io, 28.62f, 0.07396f);
        SetInitialPositionsAndDrawOrbits(europa, 24.49f, 0.14879f);
        SetInitialPositionsAndDrawOrbits(ganymede, 41.47f, 0.29896f);
        SetInitialPositionsAndDrawOrbits(callisto, 37.87f, 0.69371f);
        SetInitialPositionsAndDrawOrbits(saturn, 915.02f, 26.73f);
        SetInitialPositionsAndDrawOrbits(titan, 40.43f, 0.66375f);
        SetInitialPositionsAndDrawOrbits(rhea, 8f, 0.18825f);
        SetInitialPositionsAndDrawOrbits(iapetus, 7.42f, 3.30375f);
        SetInitialPositionsAndDrawOrbits(dione, 8.83f, 0.11325f);
        SetInitialPositionsAndDrawOrbits(tethys, 8.37f, 0.07837f);
        SetInitialPositionsAndDrawOrbits(uranus, 397.19f, 97.77f);
        SetInitialPositionsAndDrawOrbits(miranda, 3.69f, 0.05875f);
        SetInitialPositionsAndDrawOrbits(ariel, 9.09f, 0.105f);
        SetInitialPositionsAndDrawOrbits(umbriel, 9.18f, 0.172f);
        SetInitialPositionsAndDrawOrbits(neptune, 394.62f, 28.32f);
        SetInitialPositionsAndDrawOrbits(triton, 21.23f, 0.244f);
    }

    void Update()
    {
        // Handle orbit speed changes with X and Z keys
        HandleOrbitSpeedChange();

        // Update orbit and self-rotation for all planets and moons
        UpdatePlanetOrbitAndRotation(mercury);
        UpdatePlanetOrbitAndRotation(venus);
        UpdatePlanetOrbitAndRotation(earth);
        UpdatePlanetOrbitAndRotation(moon);
        UpdatePlanetOrbitAndRotation(mars);
        UpdatePlanetOrbitAndRotation(phobos);
        UpdatePlanetOrbitAndRotation(deimos);
        UpdatePlanetOrbitAndRotation(jupiter);
        UpdatePlanetOrbitAndRotation(io);
        UpdatePlanetOrbitAndRotation(europa);
        UpdatePlanetOrbitAndRotation(ganymede);
        UpdatePlanetOrbitAndRotation(callisto);
        UpdatePlanetOrbitAndRotation(saturn);
        UpdatePlanetOrbitAndRotation(titan);
        UpdatePlanetOrbitAndRotation(rhea);
        UpdatePlanetOrbitAndRotation(iapetus);
        UpdatePlanetOrbitAndRotation(dione);
        UpdatePlanetOrbitAndRotation(tethys);
        UpdatePlanetOrbitAndRotation(uranus);
        UpdatePlanetOrbitAndRotation(miranda);
        UpdatePlanetOrbitAndRotation(ariel);
        UpdatePlanetOrbitAndRotation(umbriel);
        UpdatePlanetOrbitAndRotation(neptune);
        UpdatePlanetOrbitAndRotation(triton);
    }

    void InitializePlanets()
    {
        // Sizes based on NASA data, relative to Earth (which is set to 100 units)
        mercury.planetScale = 38.31f;
        venus.planetScale = 95.23f;
        earth.planetScale = 100f;
        moon.planetScale = 27.27f;
        mars.planetScale = 53.18f;
        phobos.planetScale = 0.17f;
        deimos.planetScale = 0.097f;
        jupiter.planetScale = 1091.07f;
        io.planetScale = 28.62f;
        europa.planetScale = 24.49f;
        ganymede.planetScale = 41.47f;
        callisto.planetScale = 37.87f;
        saturn.planetScale = 915.02f;
        titan.planetScale = 40.43f;
        rhea.planetScale = 8f;
        iapetus.planetScale = 7.42f;
        dione.planetScale = 8.83f;
        tethys.planetScale = 8.37f;
        uranus.planetScale = 397.19f;
        miranda.planetScale = 3.69f;
        ariel.planetScale = 9.09f;
        umbriel.planetScale = 9.18f;
        neptune.planetScale = 394.62f;
        triton.planetScale = 21.23f;

        // Orbit radii based on NASA distances from the Sun (AU) scaled to Unity units, plus the global offset
        mercury.orbitRadius = (0.387f * sunScaleFactor) + globalOrbitOffset;
        venus.orbitRadius = (0.723f * sunScaleFactor) + globalOrbitOffset;
        earth.orbitRadius = (1.0f * sunScaleFactor) + globalOrbitOffset;
        moon.orbitRadius = (0.00257f * sunScaleFactor) + globalOrbitOffset;
        mars.orbitRadius = (1.524f * sunScaleFactor) + globalOrbitOffset;
        phobos.orbitRadius = (0.00006f * sunScaleFactor) + globalOrbitOffset;
        deimos.orbitRadius = (0.00015f * sunScaleFactor) + globalOrbitOffset;
        jupiter.orbitRadius = (5.203f * sunScaleFactor) + globalOrbitOffset;
        io.orbitRadius = (0.00282f * sunScaleFactor) + globalOrbitOffset;
        europa.orbitRadius = (0.00448f * sunScaleFactor) + globalOrbitOffset;
        ganymede.orbitRadius = (0.00714f * sunScaleFactor) + globalOrbitOffset;
        callisto.orbitRadius = (0.01244f * sunScaleFactor) + globalOrbitOffset;
        saturn.orbitRadius = (9.537f * sunScaleFactor) + globalOrbitOffset;
        titan.orbitRadius = (0.00854f * sunScaleFactor) + globalOrbitOffset;
        rhea.orbitRadius = (0.00856f * sunScaleFactor) + globalOrbitOffset;
        iapetus.orbitRadius = (0.01982f * sunScaleFactor) + globalOrbitOffset;
        dione.orbitRadius = (0.00858f * sunScaleFactor) + globalOrbitOffset;
        tethys.orbitRadius = (0.00855f * sunScaleFactor) + globalOrbitOffset;
        uranus.orbitRadius = (19.191f * sunScaleFactor) + globalOrbitOffset;
        miranda.orbitRadius = (0.00009f * sunScaleFactor) + globalOrbitOffset;
        ariel.orbitRadius = (0.00129f * sunScaleFactor) + globalOrbitOffset;
        umbriel.orbitRadius = (0.00257f * sunScaleFactor) + globalOrbitOffset;
        neptune.orbitRadius = (30.068f * sunScaleFactor) + globalOrbitOffset;
        triton.orbitRadius = (0.00355f * sunScaleFactor) + globalOrbitOffset;

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

        moon.orbitSpeed = 360f / 27.32f;        // Moon orbits Earth in 27.32 days
        moon.selfRotationSpeed = 360f / 27.32f; // Tidal lock with Earth

        mars.orbitSpeed = 360f / 687f;          // Completes one orbit in 687 Earth days
        mars.selfRotationSpeed = 360f / 1.03f;

        phobos.orbitSpeed = 360f / 0.31891f;    // Phobos orbits Mars in 0.31891 Earth days
        deimos.orbitSpeed = 360f / 1.263f;      // Deimos orbits Mars in 1.263 Earth days

        jupiter.orbitSpeed = 360f / 4333f;      // Completes one orbit in 4333 Earth days
        jupiter.selfRotationSpeed = 360f / 0.41f; // Rotates every 10 hours

        io.orbitSpeed = 360f / 1.769f;
        europa.orbitSpeed = 360f / 3.551f;
        ganymede.orbitSpeed = 360f / 7.154f;
        callisto.orbitSpeed = 360f / 16.689f;

        saturn.orbitSpeed = 360f / 10759f;      // Completes one orbit in 10759 Earth days
        saturn.selfRotationSpeed = 360f / 0.45f;

        titan.orbitSpeed = 360f / 15.945f;
        rhea.orbitSpeed = 360f / 4.518f;
        iapetus.orbitSpeed = 360f / 79.3215f;
        dione.orbitSpeed = 360f / 2.737f;
        tethys.orbitSpeed = 360f / 1.888f;

        uranus.orbitSpeed = 360f / 30687f;      // Completes one orbit in 30687 Earth days
        uranus.selfRotationSpeed = 360f / -0.72f; // Retrograde rotation

        miranda.orbitSpeed = 360f / 1.413f;
        ariel.orbitSpeed = 360f / 2.52f;
        umbriel.orbitSpeed = 360f / 4.144f;

        neptune.orbitSpeed = 360f / 60190f;     // Completes one orbit in 60190 Earth days
        neptune.selfRotationSpeed = 360f / 0.67f;

        triton.orbitSpeed = 360f / 5.877f;
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
        // Instantiate the orbit ring as a prefab or create dynamically
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
        float orbitSpeedPerFrame = planet.orbitSpeed * initialOrbitSpeed * currentOrbitSpeedMultiplier * Time.deltaTime;

        // Orbit around the Sun (assuming the Sun is at the origin)
        planet.planetTransform.RotateAround(sun.position, Vector3.up, orbitSpeedPerFrame);

        // Self-rotation around its own axis
        float rotationSpeedPerFrame = planet.selfRotationSpeed * Time.deltaTime;
        planet.planetTransform.Rotate(Vector3.up, rotationSpeedPerFrame);
    }

    void HandleOrbitSpeedChange()
    {
        // Increase orbit speed with the X key
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentOrbitSpeedMultiplier *= 2f; // Double the speed
        }

        // Decrease orbit speed with the Z key
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentOrbitSpeedMultiplier /= 2f; // Halve the speed
        }
    }
}
