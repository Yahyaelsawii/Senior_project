# XR Setup Instructions

## Issues Fixed

1. **Controller Pointer Not Showing** - Added Ray Interactors to controllers
2. **Can't Move** - Added locomotion providers (Continuous Move & Turn)
3. **Buttons Not Selectable** - Configured UI for XR interaction

## How to Run the Setup

### Option 1: Using Unity Menu (Recommended)
1. Open your scene in Unity
2. Go to **XR → Setup XR Components** in the menu bar
3. The script will automatically configure everything
4. Check the Console for confirmation messages

### Option 2: Using Context Menu
1. In the Hierarchy, right-click on any GameObject
2. Select **XR Setup Helper** component (if added to a GameObject)
3. Click **Setup XR Components** in the Inspector

## What the Setup Does

### 1. Ray Interactors (Controller Pointers)
- Adds `XRRayInteractor` to Left and Right controllers
- Adds `XRInteractorLineVisual` for pointer visualization
- Enables UI interaction
- Configures raycast distance (10 meters)

### 2. Locomotion (Movement)
- Adds `ActionBasedContinuousMoveProvider` for smooth movement
- Adds `ActionBasedContinuousTurnProvider` for smooth turning
- Creates `LocomotionSystem` if it doesn't exist
- Configures gravity and strafe movement

### 3. UI Button Interaction
- Ensures Canvas has `TrackedDeviceGraphicRaycaster` for XR UI interaction
- All existing buttons will work with XR controllers automatically

## Controls (After Setup)

### Movement
- **Left Thumbstick**: Move forward/backward and strafe left/right
- **Right Thumbstick**: Turn left/right

### Interaction
- **Trigger**: Select/Click (on buttons or objects)
- **Pointer**: Ray from controller shows where you're pointing

## Your Button Scripts

Your three buttons (PlayButton, OptionsButton, TutorialButton) are already set up with:
- `OnPlayButtonClick()` - Teleports camera to lab point
- `OnOptionsButtonClick()` - Opens brightness panel
- `CloseBrightnessPanel()` - Closes brightness panel

These will work automatically once XR setup is complete!

## Troubleshooting

### If pointer still doesn't show:
1. Check that Ray Interactors were added to controllers
2. Verify `XRInteractorLineVisual` component is present
3. Check Console for any errors

### If movement doesn't work:
1. Verify `ActionBasedContinuousMoveProvider` is on Camera Offset
2. Check that Input Actions are assigned (should be automatic)
3. Make sure you're using the thumbstick, not buttons

### If buttons don't respond:
1. Ensure Canvas has `TrackedDeviceGraphicRaycaster`
2. Check that Ray Interactors have `enableUIInteraction = true`
3. Verify buttons are on a Canvas with proper layer settings

## Manual Setup (If Needed)

If the automatic setup doesn't work, you can manually:

1. **Add Ray Interactor to Controllers:**
   - Select Left/Right Controller in Hierarchy
   - Add Component → XR Ray Interactor
   - Add Component → XR Interactor Line Visual

2. **Add Locomotion:**
   - Select Camera Offset under XR Origin
   - Add Component → Action Based Continuous Move Provider
   - Add Component → Action Based Continuous Turn Provider

3. **Setup UI:**
   - Select your Canvas
   - Add Component → Tracked Device Graphic Raycaster

## Notes

- The setup script is safe to run multiple times (it checks for existing components)
- All changes are made at runtime/editor time, not to prefabs
- Your existing button scripts will continue to work as before
