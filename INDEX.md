# Project Index

**Project Type:** Unity VR Project  
**Date Indexed:** February 13, 2026

## Project Structure

### Assets/
Main project assets directory containing all game resources.

#### Root Assets
- `Allen_brain_final.fbx` - 3D brain model (main asset)
- `DefaultVolumeProfile.asset` - Volume profile settings

#### Scenes/
- `BasicScene/` - Basic scene setup
  - `BasicScene.unity` - Main scene file
  - `Grid_Light_512x512.png` - Grid texture
  - `Grid.mat` - Grid material
- `SampleScene/` - Sample scene with lighting data
  - `SampleScene.unity` - Sample scene file
  - Lighting data files (lightmaps, reflection probes)

#### VRTemplateAssets/
VR template assets and resources.

##### Audio/
- Audio files for VR interactions

##### Fonts/
- `Inter/` - Inter font family

##### Graphics/
- Graphics assets and lighting data

##### Materials/
- 44 material files
- 31 texture files (PNG)

##### Models/
- `Anchor/` - Anchor models (3 FBX files)
- `Blink/` - Blink animation model
- `Controllers/` - VR controller models
- `Cursors/` - Cursor models
- `Environment/` - Environment models (6 FBX files)
- `Marks/` - Mark models
- `Poke/` - Poke interaction model
- `Primitives/` - Primitive shapes (5 FBX files)
- `UI/` - UI models

##### Prefabs/
- `Affordance/` - Affordance prefabs
- `Blaster/` - Blaster prefabs
- `Blink/` - Blink prefabs
- `Controller/` - Controller prefabs (3 prefabs)
- `Cursors/` - Cursor prefabs
- `Interactables/` - Interactable objects (10 prefabs)
- `Setup/` - Setup prefabs (5 prefabs)
- `Teleport/` - Teleport prefabs
- `TutorialPlayer/` - Tutorial player prefab
- `UI/` - UI prefabs (6 prefabs)

##### Scripts/
- 15 C# scripts for VR functionality

##### Shaders/
- 3 shader files
- Shader includes

##### Sprites/
- 29 PNG sprite files

##### Themes/
- 6 theme asset files

##### Tutorial/
- Tutorial assets and images
- `VRTutorialContainer.asset`
- `VRTutorialProjectSettings.asset`
- `VRTutorialStyle.asset`
- `VRTutorialWelcomePage.asset`
- Tutorial images (welcome screen, Unity logo, project header)

##### Videos/
- `OnboardingVideoVRT.webm` - VR onboarding video

#### Samples/
- Sample assets and code (588 files total)
  - 82 prefabs
  - 37 C# scripts

#### Settings/
- Project settings files
- Scene templates

#### TextMesh Pro/
- TextMesh Pro resources
  - `Fonts/` - Font files
  - `Resources/` - Resource assets
  - `Shaders/` - TextMesh Pro shaders (14 shader files)

#### XR/
Extended Reality (XR) configuration and settings.

##### AndroidXR/
- Android XR settings initializer

##### Loaders/
- `OpenXRLoader.asset` - OpenXR loader configuration
- `SimulationLoader.asset` - Simulation loader configuration

##### Resources/
- `XRSimulationRuntimeSettings.asset` - XR simulation runtime settings

##### Settings/
- `OpenXR Editor Settings.asset` - OpenXR editor settings
- `OpenXR Package Settings.asset` - OpenXR package settings
- `XRSimulationSettings.asset` - XR simulation settings

##### UserSimulationSettings/
- User simulation preferences and environment assets manager

##### XRGeneralSettingsPerBuildTarget.asset
- XR general settings per build target

#### XRI/
XR Interaction Toolkit settings.

##### Settings/
- `Resources/`
  - `InteractionLayerSettings.asset` - Interaction layer settings
  - `XRDeviceSimulatorSettings.asset` - XR device simulator settings
  - `XRInteractionRuntimeSettings.asset` - XR interaction runtime settings
- `XRInteractionEditorSettings.asset` - XR interaction editor settings

### ProjectSettings/
Unity project configuration files.

- `ProjectSettings.asset` - Main project settings
- `ProjectVersion.txt` - Unity version information
- `Physics2DSettings.asset` - 2D physics settings
- `QualitySettings.asset` - Quality settings
- `TagManager.asset` - Tag and layer manager
- `TimeManager.asset` - Time manager settings
- `URPProjectSettings.asset` - Universal Render Pipeline settings
- `VFXManager.asset` - Visual Effects manager
- `XRPackageSettings.asset` - XR package settings
- `XRSettings.asset` - XR settings
- Package-specific settings (dedicated-server, learn.iet-framework, testtools.codecoverage)

### Packages/
Package management files.

- `manifest.json` - Package manifest
- `packages-lock.json` - Package lock file

### UserSettings/
User-specific editor settings.

- `EditorUserSettings.asset` - Editor user settings
- `PlayModeUserSettings.asset` - Play mode user settings
- `Layouts/` - Editor layout files
- `Search.index` - Search index
- `Search.settings` - Search settings

### Library/
Unity library folder (auto-generated, contains compiled assets and cache).

### Temp/
Temporary files (auto-generated).

### Logs/
Build and import logs.

## File Statistics

### Assets Folder
- **Total files:** ~1145 files
  - 627 `.meta` files (Unity metadata)
  - 112 `.prefab` files
  - 78 `.mat` files (materials)
  - Additional asset files (textures, models, scripts, etc.)

### Key Asset Types
- **3D Models:** FBX files (brain model, VR controllers, environment, primitives)
- **Materials:** 78+ material files
- **Prefabs:** 112+ prefab files
- **Scripts:** C# scripts in VRTemplateAssets/Scripts/ and Samples/
- **Shaders:** TextMesh Pro shaders and custom shaders
- **Textures:** PNG files for sprites and materials
- **Scenes:** Unity scene files (BasicScene, SampleScene)

## Technology Stack

- **Engine:** Unity (version in ProjectSettings/ProjectVersion.txt)
- **Render Pipeline:** Universal Render Pipeline (URP)
- **XR Framework:** OpenXR
- **XR Toolkit:** XR Interaction Toolkit (XRI)
- **Text Rendering:** TextMesh Pro
- **Platform Support:** Android XR, OpenXR

## Notes

- This is a VR project using Unity's XR Interaction Toolkit
- Main asset: `Allen_brain_final.fbx` (brain model)
- Project includes VR template assets for controllers, interactions, and UI
- Supports OpenXR and Android XR platforms
- Uses Universal Render Pipeline for rendering
