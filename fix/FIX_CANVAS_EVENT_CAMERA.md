# Fix Canvas Event Camera & Double Rays

## Issue 1: Canvas Event Camera Warning

Your Canvas shows a **critical warning**: 
> "A World Space Canvas with no specified Event Camera may not register UI events correctly."

**This is why buttons aren't working!**

---

## Fix: Assign Event Camera to Canvas

1. Select **"Canvas"** in Hierarchy
2. In Inspector, find **"Canvas"** component
3. Look for **"Event Camera"** field
4. It currently shows: **"None (Camera)"**
5. **Assign the Main Camera:**
   - Click the circle icon next to "Event Camera"
   - In the search, type: **"Main Camera"**
   - Select **"Main Camera"** from the list
   - OR drag **"Main Camera"** from Hierarchy into the field

**After assigning the camera, the warning should disappear and buttons should work!**

---

## Issue 2: Double Rays (Pink/White)

Since Line Renderer is required by XR Interactor Line Visual, the double rays must be from something else.

### Check for Child Objects with Ray Visuals:

1. **Expand "Left Controller"** in Hierarchy
   - Look for child objects like:
     - "Ray Interactor"
     - "Line Visual"
     - Any child with "Line" in the name
2. **If you find child objects:**
   - Select each child
   - Check if it has "XR Interactor Line Visual" or "Line Renderer" components
   - If it does, and it's drawing a duplicate ray, you can:
     - **Disable the GameObject** (uncheck the checkbox at top of Inspector)
     - OR **Delete it** if it's not needed

### Check for Duplicate XR Interactor Line Visual:

Even though you only see one in Inspector, there might be two:

1. Select **"Left Controller"**
2. In Inspector, scroll through ALL components
3. Count how many **"XR Interactor Line Visual"** components you see
4. If you see **TWO**, remove one:
   - Keep the one with proper material assigned
   - Remove the one with missing/pink material

### Check Line Material:

The pink line might be from missing material:

1. Select **"Left Controller"**
2. Find **"XR Interactor Line Visual"** component
3. Expand it
4. Check **"Line Material"** field
5. If it's empty or shows "None":
   - Click the circle icon
   - Search for: **"Default-Line"** or **"XRLineRenderer"**
   - Select it
   - OR create a material:
     - Right-click in Project → Create → Material
     - Name: "RayMaterial"
     - Shader: **"Unlit/Color"**
     - Color: White
     - Assign to Line Material

6. Repeat for **"Right Controller"**

---

## Quick Checklist

### Fix Buttons:
- [ ] Canvas → Canvas component → Event Camera = **Main Camera** (not None!)
- [ ] Warning about Event Camera is gone
- [ ] Test buttons in Play Mode

### Fix Double Rays:
- [ ] Checked child objects under controllers for duplicate ray visuals
- [ ] Disabled/deleted duplicate child objects if found
- [ ] Verified only ONE XR Interactor Line Visual per controller
- [ ] Assigned Line Material to XR Interactor Line Visual (not pink)

---

## Expected Result

After fixes:
- ✅ Canvas warning is gone
- ✅ Buttons work when you point ray and press trigger
- ✅ Only ONE white ray per controller (no pink, no double lines)

---

## Why Event Camera is Required

For **World Space Canvas**, Unity needs to know which camera to use for UI event detection. Without it:
- UI events (button clicks) won't register correctly
- Raycasts from controllers won't hit UI elements properly
- Buttons won't respond to XR controller input

**Solution:** Assign Main Camera to Event Camera field.

---

## Most Important Fix

**Assign Main Camera to Canvas Event Camera** - this is the critical fix for buttons not working!

The double rays might be from child objects or missing materials - check those after fixing the Event Camera.
