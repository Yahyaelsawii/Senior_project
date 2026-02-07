# Fix Missing Scripts on Controllers - Quick Fix

## Problem
You have **2 missing scripts** on Left Controller (and likely Right Controller too) showing as "None (Mono Script)". These are preventing the ray from showing.

---

## Solution: Remove Broken Scripts & Re-add Components

### Step 1: Remove Missing Scripts from Left Controller

1. **Select "Left Controller"** in Hierarchy
   - Expand: `XR Interaction Setup` → `XR Origin (XR Rig)` → `Camera Offset` → `Left Controller`

2. In Inspector, scroll down to the **two "(Script)" components** showing warnings

3. For each missing script:
   - Click the **3 dots (⋮)** next to the script name
   - Select **"Remove Component"**
   - Repeat for the second missing script

---

### Step 2: Re-add XRRayInteractor Component

1. Still on **"Left Controller"**
2. Click **"Add Component"** button
3. Search for: **"XRRayInteractor"**
4. Click to add it
5. Configure these settings:
   - ✅ **Enable UI Interaction**: Checked
   - **Max Raycast Distance**: `10`
   - **Line Type**: `Straight Line`
   - **Raycast Mask**: `Everything` (or your desired layers)

---

### Step 3: Re-add XR Interactor Line Visual Component

1. Still on **"Left Controller"**
2. Click **"Add Component"** button
3. Search for: **"XR Interactor Line Visual"**
4. Click to add it
5. Configure these settings:
   - **Line Width**: `0.005` (or `0.01` if you want it thicker)
   - ✅ **Override Interactor Line Length**: Checked
   - **Line Length**: `10`
   - **Line Material**: Leave default (or assign a material if you want custom color)

---

### Step 4: Wire Up the Ray Interactor

1. Still on **"Left Controller"**
2. Find the **"Action Based Controller Manager"** component
3. Look for **"Ray Interactor"** field in the **Interactors** section
4. If it's empty or shows "None":
   - Click the circle icon next to the field
   - In the Hierarchy, expand `Left Controller` to see its children
   - You should see a child GameObject named **"Ray Interactor"**
   - Drag that child GameObject into the **"Ray Interactor"** field
   - OR: If there's no child, leave it pointing to the Left Controller itself (it will auto-detect)

---

### Step 5: Repeat for Right Controller

1. Select **"Right Controller"** (under Camera Offset)
2. Follow **Steps 1-4** above for Right Controller

---

### Step 6: Verify Action Asset is Assigned

1. Select **"Left Controller"**
2. Find **"Action Based Controller Manager"** component
3. Check **"Action Asset"** field
4. It should show: **"XRI Default Input Actions"**
5. If it's empty:
   - Click the circle icon
   - Navigate to: `Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/XRI Default Input Actions.inputactions`
   - Select it
6. Repeat for **"Right Controller"**

---

### Step 7: Fix XR Origin Tracking (For Tilting & Below Ground Issues)

1. Select **"XR Origin (XR Rig)"** (under XR Interaction Setup)
2. In Inspector, find **"XROrigin"** component
3. Change **"Requested Tracking Origin Mode"** from **"Device"** to **"Floor"** ✅
4. Set **"Camera Y Offset"** to `0`
5. Check **Transform → Position**:
   - Set **Y = 0** (or match your floor height)

---

### Step 8: Test

1. **Save Scene** (Ctrl+S / Cmd+S)
2. Press **Play** in Unity Editor
3. You should now see:
   - ✅ Controller rays visible
   - ✅ Can look 360° smoothly (no tilting)
   - ✅ Not below ground
   - ✅ Controllers visible

---

## Quick Checklist

- [ ] Removed 2 missing scripts from Left Controller
- [ ] Added XRRayInteractor to Left Controller
- [ ] Added XR Interactor Line Visual to Left Controller
- [ ] Wired Ray Interactor in Action Based Controller Manager
- [ ] Repeated for Right Controller
- [ ] Verified Action Asset is assigned on both controllers
- [ ] Changed XR Origin Tracking Mode to "Floor"
- [ ] Set Camera Y Offset to 0
- [ ] Set XR Origin Position Y to 0

---

## If Rays Still Don't Show

1. Check Console for errors
2. Verify **XR Interaction Manager** exists in scene
3. Make sure **OpenXR** is enabled in Project Settings → XR Plug-in Management → Android
4. Make sure **Oculus Touch Controller Profile** is enabled in OpenXR settings

---

## Alternative: Use Prefab Instead

If fixing manually is too complicated:

1. **Delete** "XR Interaction Setup" from scene
2. Go to: `Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Prefabs/`
3. Drag **"XR Origin (XR Rig).prefab"** into scene
4. This prefab has everything pre-configured correctly
5. Then add **"XR Interaction Manager"** if needed (right-click → Create Empty → Add Component → XR Interaction Manager)
