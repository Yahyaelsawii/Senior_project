# VR_Lab_2026 – Project index

Unity 2022.3 VR project for Oculus Quest. Main folders and key files.

---

## Root

- **Assets/** – Project content (scenes, models, XR config, samples)
- **Packages/** – Unity packages (manifest.json, packages-lock.json)
- **ProjectSettings/** – Build, player, XR, input, etc.
- **Library/** – Generated (cache, build artifacts; usually excluded from version control)
- **Logs/** – Unity editor logs
- **Temp/** – Temporary build/import files

---

## Assets

### Scenes
- `Assets/Scenes/SampleScene.unity` – Main scene (in build)

### XR configuration (Oculus / OpenXR)
- `Assets/XR/XRGeneralSettingsPerBuildTarget.asset` – XR loaders (Standalone + Android), automatic load/run
- `Assets/XR/Loaders/OpenXRLoader.asset` – OpenXR loader
- `Assets/XR/Settings/OpenXR Package Settings.asset` – OpenXR features (Meta Quest Support, profiles)
- `Assets/XR/Settings/OpenXR Editor Settings.asset`

### XRI (XR Interaction Toolkit)
- `Assets/XRI/Settings/` – Interaction layers, device simulator, editor settings

### Models (environment)
- `Assets/Models/operating_room.fbx` – Main environment model
- `Assets/Models/Materials/` – m_floor, m_wall_tile, m_metal, m_glass, m_details, m_furniture, m_cloth, m_red, m_black, m_white, m_glow, m_xrays
- `Assets/Models/Textures/` – Base, normal, roughness, emissive textures

### Samples (XR Interaction Toolkit 3.3.1 Starter Assets)
- `Assets/Samples/XR Interaction Toolkit/3.3.1/Starter Assets/`
  - **Prefabs:** XR Origin (XR Rig), Ray/Poke/Direct/Teleport/Gaze/NearFar interactors, controllers, teleport reticles, Permissions Manager
  - **DemoScene.unity** – XR demo scene
  - **DemoSceneAssets/** – Models, audio, scripts (IncrementUIText, MultiAnchorTeleportReticle), sprites, settings
  - **Materials, Shaders, Presets, AffordanceThemes, Filters, Animations, Editor**

### Old project files
- `Assets/OldProjectFiles/` – Start_menu.prefab, PreFab (ButtonSound, FadeFilter), Sounds, Scripts
  - **Scripts/UI:** LabMenuManager, SceneManager, UserInfoForm, ScreenFader, FadeScreen, VRKeyboard, UIButtonClickSound
  - **Scripts/XR:** TeleportOnAButton, SnapXROriginOnStart, KeyboardVRSimulator, KeyboardVRSimulatorSimple
  - **Scripts/System:** DontDestroyUISound

---

## ProjectSettings (key files)

- `ProjectSettings/ProjectSettings.asset` – Player, Android (package name, min SDK 29), graphics
- `ProjectSettings/EditorBuildSettings.asset` – Scenes in build (SampleScene)
- `ProjectSettings/XRSettings.asset` – VR device settings
- `ProjectSettings/InputManager.asset` – Input axes
- `ProjectSettings/GraphicsSettings.asset`, `QualitySettings.asset`, etc.

---

## Packages (manifest.json)

- **com.unity.xr.interaction.toolkit** 3.3.1
- **com.unity.xr.management** 4.5.4
- **com.unity.xr.openxr** 1.9.1
- **com.unity.inputsystem** (pulled by XRI)
- **com.unity.shadergraph**, **com.unity.textmeshpro**, **com.unity.timeline**, **com.unity.ugui**, **com.unity.visualscripting**, **com.unity.feature.development**, **com.unity.collab-proxy**
- **com.unity.modules.*** (core engine modules)

---

## Build

- **Platform:** Android (Oculus Quest 3)
- **Scene in build:** Assets/Scenes/SampleScene.unity
- **Package name (Android):** com.DefaultCompany.VR_Lab_2026

---

*Generated index of VR_Lab_2026 project folder.*
