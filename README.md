# Space Apps Challenge: Create an Orrery Web App 🌌🚀

## Project Overview

This project, developed by **Team SkyNext**, is part of the **2024 NASA Space Apps Challenge**. Our mission was to create an interactive orrery web application that visually represents celestial bodies such as planets, Near-Earth Asteroids (NEAs), Near-Earth Comets (NECs), and Potentially Hazardous Asteroids (PHAs). The goal is to offer an engaging and educational tool that helps users explore the solar system and learn more about the fascinating objects around us.

## Features 🌟

- **Interactive 3D Orrery**: A visually stunning, interactive model of the solar system showcasing the orbits of planets, asteroids, and comets.
- **NEO Visualization**: Real-time or pre-set data for Near-Earth Objects like NEAs, NECs, and PHAs, using NASA’s public datasets.
- **Orbital Paths**: Accurate visualization of celestial bodies' orbits using **Keplerian parameters**.
- **User-Friendly Controls**: 
  - Toggle labels and orbital paths for different celestial bodies.
  - Zoom and pan through the solar system at will.
  - Speed and time control for simulating orbits in motion (dynamic orrery).
- **Multiple Views**: Switch between first-person views of flying through the orrery and an exterior, wide-lens view of the system.
- **NASA Data Integration**: Leverages NASA’s small-body and planetary data to ensure real-world accuracy of orbits and object positions.

## Technologies Used 🔧

- **Unity**: For rendering the orrery, 3D interactions, and orbit animations.
- **C#**: To handle the logic and control functionalities within the Unity engine.
- **NASA APIs & Data**: Real-time data for planets and NEOs, using NASA's publicly available databases.
- **WebGL**: Deploys the Unity app directly on the web for cross-platform accessibility.

## Project Structure 📂

- `/Assets`: Unity project assets.
  - `/Scripts`: C# scripts managing celestial object behaviors, UI, and user interactions.
  - `/Prefabs`: 3D models of planets, asteroids, and comets.
  - `/Shaders`: Custom shaders for orbit rendering and visual effects.
  
- `/Resources`: NASA datasets for real-world orbital data.

- `/Scenes`: Core scenes in Unity that display the orrery and user interface.

- `/WebBuild`: WebGL build files ready for web deployment.

## How to Run 🚀

1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   ```
2. **Open in Unity**:
   - Open the project through Unity Hub and launch it.

3. **Build the Web App**:
   - In Unity, go to `File -> Build Settings`.
   - Select **WebGL** as the platform and click **Build** to generate web deployment files.

4. **Use NASA Data**:
   - Download datasets from NASA’s [Small-Body Database](https://ssd.jpl.nasa.gov/tools/sbdb_query.html).
   - Load them into the Unity app to display current or historical orbits.

## Our Team 🌠

**SkyNext** is a passionate team of developers and space enthusiasts driven by our love for astrophysics and interactive technology. We joined the **2024 NASA Space Apps Challenge** to push the boundaries of what’s possible with interactive space simulations. This project allowed us to explore real-time data visualization, orbital mechanics, and educational tools that can inspire future generations.

### Team Members:
- **[Your Name]**: Project Lead, Developer
- **[Your Name]**: UI/UX Designer, 3D Modeler
- **[Your Name]**: Data Integration Specialist, NASA APIs Guru
- **[Your Name]**: Orbital Mechanics Expert, C# Developer

Together, we’ve built this project to not only meet the challenge but to provide an intuitive and fun way for anyone to explore our cosmic neighborhood.

## Contributing 🤝

Want to help improve this project? Follow these steps:
1. Fork the repository.
2. Create a new branch for your feature or fix.
3. Submit a pull request with a detailed description of your contribution.

## License 📜

This project is licensed under the MIT License. NASA data and APIs used are publicly available under their respective terms.

---

Join us as we continue exploring the vastness of space, one celestial body at a time! 🌍🌙✨
