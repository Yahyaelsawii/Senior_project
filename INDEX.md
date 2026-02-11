# Project Index

**Project Name:** Senior Design 2_VR  
**Unity Version:** 2022.3.62f3  
**Project Type:** Unity VR Application with XR Interaction Toolkit

---

## 📁 Project Structure

### Core Scripts

#### Custom Gameplay / UI Scripts (`Assets/Scripts/`)
- **`UI/SceneManager.cs`** - Main menu logic
  - Handles Lab load, brightness and contrast sliders, and state toggles.
- **`UI/ScreenFader.cs`** - Full‑screen fade in/out and scene transitions.
- **`UI/UserInfoForm.cs`** - Name/age form
  - Locks movement and menu buttons until submitted, stores values in `PlayerPrefs`.
- **`UI/UIButtonClickSound.cs`** - Plays UI click sound via shared `AudioSource`.
- **`UI/VRKeyboard.cs`** - Simple custom on‑screen keyboard support (kept as backup; primary keyboard now uses XRIT Spatial Keyboard sample).
- **`UI/LabMenuManager.cs`** - In‑Lab pause/start menu
  - Toggles world‑space UI with left controller menu button and handles Resume/Main Menu.
- **`XR/TeleportOnAButton.cs`** - Teleport XR Origin to a target when A button is pressed.
- **`XR/KeyboardVRSimulator.cs`** - Editor‑only keyboard/mouse movement + XR button simulation.
- **`XR/SnapXROriginOnStart.cs`** - Optional helper that snaps XR Origin to a spawn point for a short time at scene start.
- **`System/DontDestroyUISound.cs`** - Makes the global UI sound object persist across scenes.

#### Tutorial Scripts
- **`Assets/TutorialInfo/Scripts/Readme.cs`** - Tutorial readme script
- **`Assets/TutorialInfo/Scripts/Editor/ReadmeEditor.cs`** - Editor script for readme

#### XR Toolkit Sample Scripts (Starter Assets)
- **`Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Scripts/`** — ActionBasedControllerManager, DynamicMoveProvider, GazeInputManager, ObjectSpawner, RotationAxisLockGrabTransformer, ToggleComponentZone, XRPokeFollowAffordance, ClimbTeleportDestinationIndicator, DestroySelf
- **`Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/DemoSceneAssets/Scripts/`** — MultiAnchorTeleportReticle, TeleportVolumeAnchorAffordanceStateLink, IncrementUIText
- **`Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Editor/Scripts/`** — StarterAssetsSampleProjectValidation

---

### Scenes

- **`Assets/Scenes/Lab.unity`** - Lab scene
- **`Assets/Scenes/Main_screen.unity`** - Main screen scene
- **`Assets/Scenes/test.unity`** - Test scene
- **`Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/DemoScene.unity`** - XR Toolkit demo scene (in Samples)

---

### 3D Models & Assets

#### Lab Models
- **`Assets/3D Models/FaridaProvidedLab/`** - Lab environment assets (192 files: 96 meta, 55 PNG textures, 23 materials)
- **`Assets/3D Models/LabSceneTextures/`** - Lab scene textures (190 files: 95 meta, 55 PNG, 23 materials)
- **`Assets/3D Models/TwoCabinMindVRInterior.fbx`** - VR interior model

#### Waiting Room Models
- **`Assets/3D Models/WaitingRoom/room/`**
  - `elevator_01.fbx` - Elevator model
  - `room_05.fbx` - Room model
  - `Materials/` - Material assets (40 files: 20 materials, 20 meta)
  - `NewTextures/` - Updated texture materials
  - `RoomWithMountainMaterials/` - Mountain-themed room materials
  - `texture/` - Texture images (PNG files)

#### Root-level Model
- **`Assets/sceneupdated.fbx`** - Scene model at Assets root

#### General Models
- **`Assets/Models/`**
  - `3d-model.obj` - 3D model object
  - `Brain.fbx` - Brain model
  - `Sci Fi Corridor.fbx` - Sci-fi corridor model
  - `Materials/` - Material assets (Blue Lab.mat, etc.)

#### Textures
- **`Assets/3D Models/textures/`** - General texture collection (44 files: 22 meta, 17 JPG, 5 PNG)

---

### XR Interaction Toolkit Assets

#### Starter Assets Scripts (v2.6.5)
- **`Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Scripts/`**
  - `ActionBasedControllerManager.cs` - Controller management
  - `DynamicMoveProvider.cs` - Dynamic movement provider
  - `GazeInputManager.cs` - Gaze input handling
  - `ObjectSpawner.cs` - Object spawning system
  - `RotationAxisLockGrabTransformer.cs` - Grab transformer
  - `ToggleComponentZone.cs` - Component toggle zone
  - `XRPokeFollowAffordance.cs` - Poke interaction affordance
  - `ClimbTeleportDestinationIndicator.cs` - Climbing teleport indicator
  - `DestroySelf.cs` - Self-destruction utility
  - `MultiAnchorTeleportReticle.cs` - Multi-anchor teleport reticle
  - `TeleportVolumeAnchorAffordanceStateLink.cs` - Teleport volume anchor
  - `IncrementUIText.cs` - UI text increment utility

#### XR Prefabs
- **Controllers**
  - `XR Controller Right.prefab`
  - `XR Controller Left.prefab`

- **XR Origin**
  - `XR Origin (XR Rig).prefab` - Main XR rig setup

- **Interactors**
  - `Direct Interactor.prefab` - Direct hand interaction
  - `Ray Interactor.prefab` - Ray-based interaction
  - `Teleport Interactor.prefab` - Teleportation interaction
  - `Gaze Interactor.prefab` - Gaze-based interaction
  - `Poke Interactor.prefab` - Poke/touch interaction

- **Interactables**
  - `Interactable Simple Cube.prefab`
  - `Interactable Instant Pyramid.prefab`
  - `Interactable Kinematic Torus.prefab`
  - `Interactable Velocity Tracked Wedge.prefab`
  - `Push Button.prefab`
  - `Interaction Affordance.prefab`

- **Teleportation**
  - `Teleport Area.prefab`
  - `Teleport Anchor.prefab`
  - `Snapping Teleport Anchor.prefab`
  - `Blocking Teleport Reticle.prefab`
  - `Directional Teleport Reticle.prefab`
  - `Climb Teleport Arrow.prefab`

- **Climbing**
  - `Climb Sample.prefab`
  - `Climbing Wall.prefab`
  - `Ladder.prefab`
  - `Single Floor Ladder.prefab`
  - `Multi Floor Ladder.prefab`
  - `Multi-Anchor Teleport Reticle.prefab`

- **UI Prefabs**
  - `Icon Button.prefab`
  - `Icon Toggle.prefab`
  - `TextButton.prefab`
  - `Text Toggle.prefab`
  - `Dropdown.prefab`
  - `MinMaxSlider.prefab`
  - `ModalSingleButton.prefab`
  - `Scroll UI Sample.prefab`
  - `Interactive Controls.prefab`
  - `UI Sample.prefab`

- **Other**
  - `TunnelingVignette.prefab` - Comfort vignette effect
  - `XR Interaction Setup.prefab` - Complete XR setup
  - `Teleportation Environment.prefab` - Teleport demo environment
  - **Spatial Keyboard (Samples)**:
    - `XRI Global Keyboard Manager.prefab` - Global XR keyboard manager used for name/age input.
    - `XRI Keyboard.prefab`, `XRI Spatial Keyboard.prefab` and key prefabs - backing assets for the on‑screen VR keyboard.

---

### Settings & Configuration

#### XR Settings
- **`Assets/XR/Loaders/OpenXRLoader.asset`** - OpenXR loader configuration
- **`Assets/XR/Settings/`** - XR general settings
- **`Assets/XR/XRGeneralSettingsPerBuildTarget.asset`** - Build target settings

#### XRI Settings
- **`Assets/XRI/Settings/`** - XR Interaction Toolkit settings (7 files: 4 meta, 3 assets)

#### Scene Settings
- **`Assets/Settings/SampleSceneProfile.asset`** - Scene profile settings

---

### TextMesh Pro Assets

- **`Assets/TextMesh Pro/`**
  - `Fonts/LiberationSans.ttf` - Default font
  - `Shaders/` - TextMesh Pro shaders (34 files: 17 meta, 13 shaders, 4 cginc)
  - `Resources/` - Resource assets (21 files: 12 meta, 5 assets, 2 materials)
  - `Sprites/EmojiOne.json` & `EmojiOne.png` - Emoji support

---

### Project Configuration Files

#### Unity Project Files
- **`Assembly-CSharp.csproj`** - C# project file
- **`Assembly-CSharp-Editor.csproj`** - Editor project file
- **`Senior Design 2_VR.sln`** - Visual Studio solution file

#### Package Management
- **`Packages/manifest.json`** - Package dependencies:
  - Unity XR Interaction Toolkit 2.6.5
  - Unity XR Management 4.4.0
  - Unity OpenXR 1.10.0
  - Meta XR All‑in‑One SDK 83.0.4 (for Quest platform features; OpenXR remains active loader)
  - Unity Input System 1.7.0
  - TextMesh Pro 3.0.9
  - Unity UI (uGUI) 2.0.0
  - Unity Timeline 1.8.10
  - Unity Visual Scripting 1.9.9
  - Unity Collaborate 2.11.3

#### Project Settings
- **`ProjectSettings/`** - Unity project settings (25 files)
  - `ProjectVersion.txt` - Unity version info
  - `XRSettings.asset` - XR configuration
  - `InputManager.asset` - Input settings
  - `GraphicsSettings.asset` - Graphics configuration
  - `QualitySettings.asset` - Quality presets
  - And more...

---

## 🔧 Key Features

1. **Scene Management** - Camera teleportation and state management
2. **Brightness Control** - Dynamic light intensity adjustment
3. **Contrast Control** - Material color adjustment for mesh renderers
4. **VR Interaction** - Full XR Interaction Toolkit integration
5. **Lab Environment** - Custom lab scene with 3D models
6. **Waiting Room** - Separate waiting room environment

---

## 📋 Current Project Status

### ✅ Fixed Issues
- Input Handling set to "Input System Package (New)" (was causing Android crashes)
- XR Interaction Toolkit updated to 2.6.5 (matches sample scripts)
- Oculus package removed (using OpenXR instead)

### ⚠️ Known Issues & Solutions
- **Scene Corruption:** If scene file is corrupted, delete broken XR Origin and re-add from prefab
- **Missing Scripts:** Remove GameObjects with missing scripts and recreate from prefabs
- **Manual Setup Required:** XR components must be added manually (see fix/MANUAL_XR_SETUP.md)

### 🎯 Setup Requirements
- XR Origin prefab must be added from: `Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Prefabs/XR Origin (XR Rig).prefab`
- XR Interaction Manager must be created manually
- Controllers need XRRayInteractor and XRInteractorLineVisual components
- Camera Offset needs ActionBasedContinuousMoveProvider and ActionBasedContinuousTurnProvider
- Canvas needs TrackedDeviceGraphicRaycaster for UI interaction

---

## 📝 Technical Notes

- All Unity assets have corresponding `.meta` files for Unity's asset management
- The project uses **OpenXR** as the XR provider (not Oculus package)
- TextMesh Pro is integrated for UI text rendering
- The project includes both custom scripts and XR Toolkit sample assets
- Build target: **Android** for Oculus Quest devices
- Graphics API: **Vulkan** (recommended for Quest)
- Minimum Android API: **29** (Android 10)

---

## 📚 Documentation Files

- **`INDEX.md`** - This file - Project index and structure overview
- **`fix/`** - Fix and setup instruction guides:
  - **`fix/MANUAL_XR_SETUP.md`** - Step-by-step manual XR setup instructions
  - **`fix/CRASH_FIX_INSTRUCTIONS.md`** - Troubleshooting guide for crashes and missing scripts
  - **`fix/XR_SETUP_INSTRUCTIONS.md`** - XR component setup guide (legacy - see MANUAL_XR_SETUP.md)
  - **Other fix guides:** FIX_ALL_CONTROLLER_ISSUES.md, FIX_BUTTONS_XR.md, FIX_CANVAS_EVENT_CAMERA.md, FIX_DEMOSCENE_ISSUES.md, FIX_DOUBLE_RAYS_AND_BUTTONS.md, FIX_EXISTING_SETUP.md, FIX_FALLING.md, FIX_FALLING_AND_ROOM_MOVING.md, FIX_LEFT_CONTROLLER_AND_PINK.md, FIX_MISSING_CONTROLLER_SCRIPTS.md, FIX_MISSING_SCRIPTS.md, FIX_PACKAGE_COMPILATION.md, FIX_PINK_RAY_AND_BUTTONS_FINAL.md, FIX_USING_PREFABS.md, FIND_BUTTON_ONCLICK.md

---

## 🗂️ File Count Summary

- **C# Scripts:** 16 files (custom + XR Toolkit samples + TutorialInfo)
- **Unity Scenes:** 4 (Lab, Main_screen, test, DemoScene in Samples)
- **Prefabs:** 40+ files (XR Toolkit Starter Assets)
- **3D Models:** Multiple FBX and OBJ (Assets root, Models/, 3D Models/, WaitingRoom/)
- **Materials:** 100+ material files
- **Textures:** 200+ image files (PNG, JPG)
- **Documentation:** 19 markdown files (index, setup, fix guides)

---

## ⚠️ Important Notes

- **XR Setup:** Use `fix/MANUAL_XR_SETUP.md` for step-by-step XR component configuration
- **Crash Issues:** See `fix/CRASH_FIX_INSTRUCTIONS.md` for troubleshooting
- **Input Handling:** Set to "Input System Package (New)" for Android/Quest builds
- **Scene Corruption:** If scene is corrupted, delete broken XR Origin and re-add from prefab
- **Missing Scripts:** Remove broken GameObjects and recreate from prefabs

---

*Index last updated: Feb 11, 2026*
