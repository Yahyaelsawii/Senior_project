# Fix DemoScene Issues - Step by Step

## Issues:
1. ❌ Can't look 360° - weird tilting
2. ❌ Below the ground
3. ❌ Controllers and ray tracing not showing

---

## Root Cause

The **XR Origin** has the wrong **Tracking Origin Mode**. It's set to "Device" but should be "Floor" for Quest.

---

## Fix All 3 Issues in Unity Editor

### Step 1: Fix Tracking Origin (Fixes Issues 1 & 2)

1. **Open DemoScene** (or your scene)
2. In Hierarchy, select **"XR Interaction Setup"**
3. Expand it to see: **"XR Origin (XR Rig)"**
4. Select **"XR Origin (XR Rig)"**
5. In Inspector, find the **"XROrigin"** component
6. Look for **"Requested Tracking Origin Mode"**
7. Change it from **"Device"** to **"Floor"** ✅
8. Set **"Camera Y Offset"** to **0** (if it's not already)

**This fixes:**
- ✅ Weird tilting / can't look 360°
- ✅ Being below the ground

---

### Step 2: Fix Controller Position (If Still Below Ground)

1. Still on **"XR Origin (XR Rig)"**
2. Check **Transform → Position**
3. Set **Y = 0** (or match your floor height)
4. Set **Rotation = (0, 0, 0)**
5. Set **Scale = (1, 1, 1)**

---

### Step 3: Verify Controllers Are Enabled (Fixes Issue 3)

1. Expand **"XR Origin (XR Rig)"** → **"Camera Offset"**
2. You should see:
   - **"Left Controller"**
   - **"Right Controller"**
3. Select **"Left Controller"**
4. In Inspector, check:
   - **"XR Controller"** component exists ✅
   - **"Action Based Controller Manager"** component exists ✅
   - **"XRRayInteractor"** component exists ✅ (for ray)
   - **"XR Interactor Line Visual"** component exists ✅ (for ray visualization)
5. Repeat for **"Right Controller"**

---

### Step 4: Check Ray Interactor Settings

If rays still don't show:

1. Select **"Left Controller"**
2. Find **"XRRayInteractor"** component
3. Check:
   - **"Enable UI Interaction"** = ✅ Checked
   - **"Max Raycast Distance"** = 10 (or higher)
   - **"Line Type"** = Straight Line
4. Find **"XR Interactor Line Visual"** component
5. Check:
   - **"Line Width"** = 0.005 (or visible value)
   - **"Override Interactor Line Length"** = ✅ Checked
   - **"Line Length"** = 10 (or higher)
6. Repeat for **"Right Controller"**

---

### Step 5: Verify XR Interaction Manager

1. In Hierarchy, look for **"XR Interaction Manager"**
2. If missing:
   - Right-click in Hierarchy → **Create Empty**
   - Name it: **"XR Interaction Manager"**
   - Select it → **Add Component** → **"XR Interaction Manager"**

---

### Step 6: Check Input Actions Asset

1. Select **"Left Controller"**
2. Find **"Action Based Controller Manager"** component
3. Check **"Action Asset"** field
4. It should reference: **"XRI Default Input Actions"**
5. If empty:
   - Click the circle icon next to the field
   - Navigate to: `Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/XRI Default Input Actions.inputactions`
   - Select it
6. Repeat for **"Right Controller"**

---

### Step 7: Verify OpenXR Settings

1. **Edit → Project Settings → XR Plug-in Management → Android**
2. Check:
   - ✅ **OpenXR** is checked/enabled
3. **Edit → Project Settings → XR Plug-in Management → OpenXR → Android**
4. Check:
   - ✅ **Meta Quest Support** is enabled
   - ✅ **Oculus Touch Controller Profile** is enabled

---

### Step 8: Test in Editor First

1. Press **Play** in Unity Editor
2. You should see:
   - Controllers visible (or at least rays)
   - Can look around 360° smoothly
   - Not falling through ground
3. If it works in Editor, build to Quest

---

### Step 9: Recenter in Quest

After building to Quest:

1. Put on headset
2. **Long-press Meta button** to recenter view
3. Or use **Quest menu → Settings → Guardian → Recenter**

---

## Quick Checklist

- [ ] XR Origin → Tracking Origin Mode = **Floor**
- [ ] XR Origin → Camera Y Offset = **0**
- [ ] XR Origin → Position Y = **0** (or floor height)
- [ ] Left Controller → XRRayInteractor exists
- [ ] Left Controller → XR Interactor Line Visual exists
- [ ] Right Controller → XRRayInteractor exists
- [ ] Right Controller → XR Interactor Line Visual exists
- [ ] Both Controllers → Action Asset = "XRI Default Input Actions"
- [ ] XR Interaction Manager exists in scene
- [ ] OpenXR → Android → Meta Quest Support enabled
- [ ] OpenXR → Android → Oculus Touch Controller Profile enabled

---

## If Still Not Working

Tell me:
1. Does it work in Unity Editor Play mode?
2. What happens when you build to Quest?
3. Are controllers detected at all (do you see hand models)?
4. Screenshot of XR Origin Inspector (XROrigin component)
