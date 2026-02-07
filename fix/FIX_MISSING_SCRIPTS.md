# Fix Missing Scripts - Step by Step

## Current Issues:
- Missing scripts on Left Controller (3 instances)
- Missing scripts on Right Controller (3 instances)  
- Missing script on XR Interaction Manager
- Missing script on InteractionLayerSettings

## Solution: Replace with Prefab

The XR Origin from GameObjects menu is missing components. Use the prefab instead.

---

## Step 1: Delete Broken XR Origin

1. In Hierarchy, find **"XR Origin (XR Rig)"**
2. Right-click → **Delete**
3. Also delete **"XR Interaction Manager"** if it exists

---

## Step 2: Add XR Origin from Prefab

1. In Project window, navigate to:
   ```
   Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Prefabs/
   ```
2. Find **"XR Origin (XR Rig).prefab"**
3. **Drag it into the Hierarchy** (not Scene view)
4. Position it at (0, 0, 0) or where you want player to start

This prefab has ALL the correct components already set up!

---

## Step 3: Verify XR Interaction Manager

The prefab should automatically create XR Interaction Manager. Check:

1. Look in Hierarchy for **"XR Interaction Manager"**
2. If it's missing:
   - Right-click in Hierarchy → Create Empty
   - Name it: **"XR Interaction Manager"**
   - Select it → Add Component → Search **"XR Interaction Manager"** → Add

---

## Step 4: Add Movement Components

The prefab might not have movement enabled. Add these:

### On "Camera Offset" (under XR Origin):

1. Select **"Camera Offset"** in Hierarchy
2. **Add Component** → Search **"Action Based Continuous Move Provider"**
   - Enable Strafe: ✅ Checked
   - Use Gravity: ✅ Checked
   - Gravity Application Mode: **Attempting Move**
3. **Add Component** → Search **"Action Based Continuous Turn Provider"**
   - Turn Speed: **60**

---

## Step 5: Verify Controller Ray (Pointer)

The prefab should have Ray Interactors, but verify:

### Left Controller:
1. Select **"Left Controller"** (under XR Origin → Camera Offset)
2. Check Inspector for these components:
   - ✅ **XRRayInteractor** (should exist)
   - ✅ **XRInteractorLineVisual** (should exist)
3. If missing, add:
   - Add Component → **XRRayInteractor**
     - Enable UI Interaction: ✅ Checked
     - Max Raycast Distance: 10
   - Add Component → **XR Interactor Line Visual**
     - Line Width: 0.005

### Right Controller:
1. Select **"Right Controller"** (under XR Origin → Camera Offset)
2. Same check as Left Controller
3. Add same components if missing

---

## Step 6: Setup Canvas for UI

1. Select your **"Canvas"** GameObject
2. **Add Component** → Search **"Tracked Device Graphic Raycaster"**
3. This allows controllers to click buttons

---

## Step 7: Check for Missing Scripts

1. Look at Console
2. If you still see missing script warnings:
   - Select the GameObject with missing script
   - In Inspector, you'll see "(Script)" with empty field
   - Click the **3 dots** → **Remove Component**
   - Repeat for all missing scripts

---

## Step 8: Save and Test

1. **Save Scene** (Ctrl+S / Cmd+S)
2. Press **Play** to test in Editor
3. You should see:
   - Controller rays/pointers
   - Ability to move with thumbsticks
   - Ability to click buttons

---

## Controls:

- **Left Thumbstick**: Move forward/back/left/right
- **Right Thumbstick**: Turn left/right  
- **Trigger**: Click buttons or select objects
- **Ray**: Visible line from controller showing where you're pointing

---

## If Still Having Issues:

1. **Delete the entire XR Origin** from scene
2. **Drag the prefab again** from:
   `Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Prefabs/XR Origin (XR Rig).prefab`
3. Make sure you're using the **prefab**, not creating from GameObjects menu

The prefab has everything pre-configured correctly!
