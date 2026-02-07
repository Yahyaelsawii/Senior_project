# Fix Left Controller & Pink Objects - Quick Fix

## Issues:
1. ❌ Left Controller missing XR Controller component
2. ❌ Pink objects/lines in scene (missing materials)
3. ❌ Error in console

---

## Fix 1: Add XR Controller to Left Controller

The warning says: **"This component requires the GameObject to have an XR Controller component."**

### Step 1: Select Left Controller

1. In Hierarchy, expand: `XR Interaction Setup` → `XR Origin (XR Rig)` → `Camera Offset`
2. Select **"Left Controller"**

### Step 2: Add XR Controller Component

1. In Inspector, click **"Add Component"**
2. Search for: **"XR Controller"** (or **"Action Based Controller"**)
3. Add it
4. Configure:
   - **Controller Node**: `Left Hand`
   - **Update Tracking Type**: `Update and Before Render`
   - **Enable Input Tracking**: ✅ Checked

### Step 3: Verify Components

Left Controller should now have:
- ✅ **XR Controller** (or Action Based Controller)
- ✅ **Action Based Controller Manager**
- ✅ **XRRayInteractor** (for ray)
- ✅ **XR Interactor Line Visual** (for ray visualization)

---

## Fix 2: Fix Pink Objects (Missing Materials)

Pink = Unity's way of showing missing materials/shaders.

### Option A: Find and Fix Missing Materials

1. **Select the pink object** in Scene view
2. In Inspector, check the **Mesh Renderer** component
3. Look for **Materials** section
4. If it says **"None (Material)"** or shows pink:
   - Click the circle icon next to the material slot
   - Assign a material, OR
   - Create a new material:
     - Right-click in Project → Create → Material
     - Name it (e.g., "DefaultMaterial")
     - Drag it into the material slot

### Option B: Hide Pink Objects Temporarily

If you don't need these objects:

1. Select the pink object
2. In Inspector, **uncheck** the GameObject (top-left checkbox)
3. This hides it until you fix the material

### Option C: Fix Pink Lines (Ray Visuals)

If the pink lines are from controller rays:

1. Select **"Left Controller"** or **"Right Controller"**
2. Find **"XR Interactor Line Visual"** component
3. Check **"Line Material"** field
4. If it's empty or pink:
   - Click the circle icon
   - Search for: **"Default-Line"** or **"XRLineRenderer"**
   - Select it
   - OR create a new material:
     - Right-click in Project → Create → Material
     - Shader: **"Unlit/Color"** or **"Sprites/Default"**
     - Set color to white or your preferred color
     - Assign to Line Material

---

## Fix 3: Check Console Error

1. **Open Console** (Window → General → Console)
2. Look for **red error messages**
3. **Click on the error** to see details
4. Common errors and fixes:

### Error: "XR Controller component missing"
- **Fix**: Follow Fix 1 above

### Error: "Material/Material is missing"
- **Fix**: Follow Fix 2 above

### Error: "NullReferenceException"
- **Fix**: Check that all required components are assigned
- Make sure XR Interaction Manager exists in scene

### Error: "Input Action Asset not assigned"
- **Fix**: 
  1. Select Left/Right Controller
  2. Find "Action Based Controller Manager"
  3. Assign "XRI Default Input Actions" to "Action Asset" field

---

## Quick Checklist

- [ ] Added XR Controller component to Left Controller
- [ ] Verified Left Controller has all required components
- [ ] Fixed pink objects (assigned materials)
- [ ] Fixed pink lines (assigned line material to ray visuals)
- [ ] Checked Console for errors
- [ ] Fixed any red errors

---

## Expected Result

After fixing:
- ✅ Left Controller works (same as Right Controller)
- ✅ No pink objects (all have materials)
- ✅ No pink lines (ray visuals have materials)
- ✅ No errors in Console
- ✅ Both controllers show rays

---

## If Left Controller Still Doesn't Work

1. **Compare with Right Controller:**
   - Select Right Controller
   - Check what components it has
   - Make sure Left Controller has the same components

2. **Copy Right Controller Setup:**
   - Select Right Controller
   - In Inspector, note all components
   - Select Left Controller
   - Add the same components with same settings

3. **Use Prefab Instead:**
   - Delete Left Controller from scene
   - Go to: `Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Prefabs/Controllers/`
   - Drag **"XR Controller Left.prefab"** into scene
   - Place it under Camera Offset (same as Right Controller)
