# Fix Falling + Room Moving With Head

## Issue 1: Still Falling

The Mesh Collider might not be working. Try this:

### Check Your Floor Model:

1. Select your floor model (`room_05`, `sceneupdated`, or `Sci Fi Corridor`)
2. In Inspector, check if **Mesh Collider** is there
3. Make sure it's **enabled** (checkbox checked)
4. **Important:** If the model is complex, make sure **"Convex" is UNCHECKED**
5. Try **"Box Collider"** instead of Mesh Collider:
   - Remove Mesh Collider (if it's not working)
   - Add Component → **"Box Collider"**
   - Adjust the size to cover your floor

### OR: Disable Gravity Temporarily

1. Select **"Camera Offset"** (under XR Origin)
2. Find **"Dynamic Move Provider"** component
3. Uncheck **"Use Gravity"**
4. This stops falling immediately

---

## Issue 2: Room Moves With Head (CRITICAL FIX)

**This means your room models are accidentally children of XR Origin!**

### Fix This:

1. **Check Hierarchy:**
   - Look at "XR Origin (XR Rig)" in Hierarchy
   - **Expand it** (click the arrow)
   - Check if `room_05`, `sceneupdated`, or `Sci Fi Corridor` are **inside** XR Origin
   
2. **If room models are inside XR Origin:**
   - **Drag them OUT** of XR Origin
   - Drag them to the **root level** of Hierarchy (same level as XR Origin)
   - They should be **siblings** of XR Origin, not children

3. **Correct Hierarchy Should Look Like:**
   ```
   SampleScene
   ├── XR Origin (XR Rig)
   │   ├── Camera Offset
   │   │   ├── Main Camera
   │   │   ├── Left Controller
   │   │   └── Right Controller
   │   └── Locomotion System
   ├── room_05          ← Should be here (root level)
   ├── sceneupdated     ← Should be here (root level)
   ├── Sci Fi Corridor  ← Should be here (root level)
   ├── Canvas
   └── SceneManager
   ```

4. **If room models are NOT inside XR Origin:**
   - Check if XR Origin's position/rotation is wrong
   - Select **"XR Origin (XR Rig)"**
   - Reset Transform: Position (0, 0, 0), Rotation (0, 0, 0), Scale (1, 1, 1)

---

## Complete Fix Steps:

### Step 1: Fix Room Moving Issue

1. In Hierarchy, **expand "XR Origin (XR Rig)"**
2. If you see `room_05`, `sceneupdated`, or `Sci Fi Corridor` inside it:
   - **Drag them out** to root level
3. Verify XR Origin Transform:
   - Position: (0, 0, 0)
   - Rotation: (0, 0, 0)
   - Scale: (1, 1, 1)

### Step 2: Fix Falling Issue

**Option A: Use Box Collider (Easier)**

1. Select your floor model
2. Remove Mesh Collider if it exists
3. Add Component → **"Box Collider"**
4. In Inspector, adjust **Size**:
   - X: 50 (or however wide your floor is)
   - Y: 1
   - Z: 50 (or however long your floor is)
5. Adjust **Center** Y to match floor height

**Option B: Disable Gravity**

1. Select **"Camera Offset"**
2. Find **"Dynamic Move Provider"**
3. Uncheck **"Use Gravity"**

### Step 3: Verify XR Origin Position

1. Select **"XR Origin (XR Rig)"**
2. Set Position Y to match your floor height (usually 0 or 1.5)
3. The CharacterController should snap to ground

---

## Test:

1. Save scene
2. Press Play
3. **Room should stay still** when you move your head
4. **You should not fall** through the floor

---

## If Still Having Issues:

1. **Check CharacterController:**
   - Select "XR Origin (XR Rig)"
   - Find **CharacterController** component
   - Make sure it's **enabled** (checkbox checked)
   - Height should be around 1.36
   - Radius should be 0.1

2. **Check Ground Detection:**
   - The CharacterController needs to detect the collider
   - Make sure your floor collider is **not** set as **"Is Trigger"**
   - The collider should be on the **Default layer** (or a layer that CharacterController can collide with)
