# Manual XR Setup - Step by Step

## ⚠️ IMPORTANT: Do This First

1. **Delete the broken XR Origin from your scene:**
   - In Hierarchy, find "XR Origin (XR Rig)"
   - Right-click → Delete
   - Also delete "XR Interaction Manager" if it has missing scripts

2. **Add the XR Origin Prefab:**
   - Go to: `Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Prefabs/`
   - Drag **"XR Origin (XR Rig).prefab"** into your scene
   - Position it where you want the player to start (usually at 0,0,0)

3. **Add XR Interaction Manager:**
   - Right-click in Hierarchy → Create Empty
   - Name it: **"XR Interaction Manager"**
   - Select it
   - In Inspector, click **Add Component**
   - Search for: **"XR Interaction Manager"**
   - Add it

## Components You Need to Add Manually

### 1. Left Controller - Add These Components:

Select **"Left Controller"** (under XR Origin → Camera Offset):

1. **XRRayInteractor** (for pointer)
   - Add Component → Search "XRRayInteractor"
   - Settings:
     - Enable UI Interaction: ✅ Checked
     - Max Raycast Distance: 10
     - Line Type: Straight Line

2. **XRInteractorLineVisual** (for pointer visualization)
   - Add Component → Search "XR Interactor Line Visual"
   - Settings:
     - Line Width: 0.005
     - Override Interactor Line Length: ✅ Checked
     - Line Length: 10

### 2. Right Controller - Add These Components:

Select **"Right Controller"** (under XR Origin → Camera Offset):

1. **XRRayInteractor** (same as Left Controller)
   - Add Component → Search "XRRayInteractor"
   - Settings:
     - Enable UI Interaction: ✅ Checked
     - Max Raycast Distance: 10
     - Line Type: Straight Line

2. **XRInteractorLineVisual** (same as Left Controller)
   - Add Component → Search "XR Interactor Line Visual"
   - Settings:
     - Line Width: 0.005
     - Override Interactor Line Length: ✅ Checked
     - Line Length: 10

### 3. Camera Offset - Add These Components:

Select **"Camera Offset"** (under XR Origin):

1. **ActionBasedContinuousMoveProvider** (for movement)
   - Add Component → Search "Action Based Continuous Move Provider"
   - Settings:
     - Enable Strafe: ✅ Checked
     - Use Gravity: ✅ Checked
     - Gravity Application Mode: Attempting Move

2. **ActionBasedContinuousTurnProvider** (for turning)
   - Add Component → Search "Action Based Continuous Turn Provider"
   - Settings:
     - Turn Speed: 60

### 4. Canvas - Add This Component:

Select your **"Canvas"** GameObject:

1. **TrackedDeviceGraphicRaycaster** (for XR UI interaction)
   - Add Component → Search "Tracked Device Graphic Raycaster"
   - This allows controllers to interact with UI buttons

## Verify Everything is Set Up

After adding all components, check:

- ✅ XR Origin exists in scene
- ✅ XR Interaction Manager exists in scene
- ✅ Left Controller has XRRayInteractor and XRInteractorLineVisual
- ✅ Right Controller has XRRayInteractor and XRInteractorLineVisual
- ✅ Camera Offset has ActionBasedContinuousMoveProvider and ActionBasedContinuousTurnProvider
- ✅ Canvas has TrackedDeviceGraphicRaycaster
- ✅ No missing script warnings in Console

## Controls

- **Left Thumbstick**: Move forward/back/left/right
- **Right Thumbstick**: Turn left/right
- **Trigger**: Click/Select buttons or objects
- **Pointer**: Ray from controller shows where you're pointing

## Save and Build

1. Save the scene (Ctrl+S / Cmd+S)
2. Build and test

Your buttons (PlayButton, OptionsButton, TutorialButton) should now work with the controllers!
