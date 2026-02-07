# Fix Controllers Using Prefabs (No Component Search Needed)

## Problem
The XR components aren't showing up in the "Add Component" search. This usually means there are compilation errors or the package needs to reimport.

## Solution: Use Prefabs Instead!

Instead of adding components manually, we'll use the **pre-configured prefabs** that already have everything set up.

---

## Step 1: Check for Compilation Errors First

1. **Open Console** (Window → General → Console)
2. Look for **red error messages**
3. If you see errors, Unity won't show components until they're fixed
4. **Fix any errors** first, then continue

---

## Step 2: Remove Missing Scripts

1. Select **"Left Controller"** in Hierarchy
2. In Inspector, find the **two "(Script)" components** with warnings
3. For each:
   - Click **3 dots (⋮)** → **Remove Component**
4. Repeat for **"Right Controller"**

---

## Step 3: Add Ray Interactor Prefab to Left Controller

1. In **Project** window, navigate to:
   ```
   Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Prefabs/Interactors/
   ```
2. Find **"Ray Interactor.prefab"**
3. **Drag it** into Hierarchy as a **child** of **"Left Controller"**
   - It should appear under: `Left Controller → Ray Interactor`
4. The prefab already has:
   - ✅ XRRayInteractor component
   - ✅ XR Interactor Line Visual component
   - ✅ All settings configured correctly

---

## Step 4: Wire Up the Ray Interactor

1. Select **"Left Controller"**
2. Find **"Action Based Controller Manager"** component
3. In **Interactors** section, find **"Ray Interactor"** field
4. Drag the **"Ray Interactor"** child GameObject (from Step 3) into this field
   - OR: Click the circle icon and select it from the list

---

## Step 5: Repeat for Right Controller

1. Drag **"Ray Interactor.prefab"** again
2. Drop it as a child of **"Right Controller"**
3. Wire it up in the **Action Based Controller Manager** (same as Step 4)

---

## Step 6: Fix XR Origin Tracking (For Tilting & Below Ground)

1. Select **"XR Origin (XR Rig)"** (under XR Interaction Setup)
2. In Inspector, find **"XROrigin"** component
3. Change **"Requested Tracking Origin Mode"** from **"Device"** to **"Floor"** ✅
4. Set **"Camera Y Offset"** to `0`
5. Check **Transform → Position**:
   - Set **Y = 0** (or match your floor height)

---

## Step 7: Verify Everything

1. **Left Controller** should have:
   - ✅ "Ray Interactor" child GameObject
   - ✅ Action Based Controller Manager → Ray Interactor field filled

2. **Right Controller** should have:
   - ✅ "Ray Interactor" child GameObject  
   - ✅ Action Based Controller Manager → Ray Interactor field filled

3. **XR Origin** should have:
   - ✅ Tracking Origin Mode = "Floor"
   - ✅ Camera Y Offset = 0

---

## Step 8: Test

1. **Save Scene** (Ctrl+S / Cmd+S)
2. Press **Play** in Unity Editor
3. You should now see:
   - ✅ Controller rays visible
   - ✅ Can look 360° smoothly (no tilting)
   - ✅ Not below ground
   - ✅ Controllers visible

---

## Alternative: If Prefabs Don't Work Either

If dragging prefabs also doesn't work, there's likely a compilation error. Do this:

1. **Close Unity**
2. **Delete** these folders (they'll regenerate):
   - `Library/ScriptAssemblies/`
   - `Library/ScriptCache/`
3. **Reopen Unity**
4. Wait for Unity to reimport everything
5. Check Console for errors
6. Try the prefab method again

---

## Quick Checklist

- [ ] Checked Console for compilation errors
- [ ] Removed missing scripts from Left Controller
- [ ] Removed missing scripts from Right Controller
- [ ] Added "Ray Interactor.prefab" as child of Left Controller
- [ ] Wired Ray Interactor in Left Controller's Action Based Controller Manager
- [ ] Added "Ray Interactor.prefab" as child of Right Controller
- [ ] Wired Ray Interactor in Right Controller's Action Based Controller Manager
- [ ] Changed XR Origin Tracking Mode to "Floor"
- [ ] Set Camera Y Offset to 0

---

## Why This Works

The prefabs are pre-configured with all the correct components and settings. By using prefabs instead of searching for components, we bypass any issues with Unity's component search system.
