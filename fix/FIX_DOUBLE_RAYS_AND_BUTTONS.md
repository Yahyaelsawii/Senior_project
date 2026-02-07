# Fix Double Rays (Pink/White) & Make Buttons Work

## Issues:
1. ❌ Two lines per controller (pink and white) = 4 total lines
2. ❌ Buttons not pressing/working

---

## Fix 1: Remove Duplicate Ray Lines

You have **two ray visual components** per controller, causing double lines. One has a missing material (pink), one works (white).

### Step 1: Check Left Controller

1. Select **"Left Controller"** in Hierarchy
2. In Inspector, look for **"XR Interactor Line Visual"** components
3. You should see **TWO** of them
4. **Remove the one with pink/missing material:**
   - Find the one where "Line Material" is empty or shows pink
   - Click **3 dots (⋮)** → **Remove Component**
5. **Keep the one with white material** (working one)

### Step 2: Check Right Controller

1. Select **"Right Controller"**
2. Same process - remove duplicate "XR Interactor Line Visual" component
3. Keep only the one with proper material

### Step 3: Check for Duplicate Ray Interactors

You might also have duplicate "XRRayInteractor" components:

1. Select **"Left Controller"**
2. In Inspector, count how many **"XRRayInteractor"** components you have
3. If you have **TWO**, remove one:
   - Keep the one that has "Enable UI Interaction" checked
   - Remove the duplicate
4. Repeat for **"Right Controller"**

---

## Fix 2: Make Buttons Work

Buttons need **XR UI Input Module** on EventSystem, not just regular EventSystem.

### Step 1: Check EventSystem

1. In Hierarchy, look for **"EventSystem"** GameObject
2. Select it
3. In Inspector, check what components it has:
   - Should have **"Event System"** component ✅
   - Should have **"XR UI Input Module"** component ✅ (this is what you need!)

### Step 2: Add XR UI Input Module

If EventSystem doesn't have "XR UI Input Module":

1. Select **"EventSystem"** GameObject
2. Click **"Add Component"**
3. Search for: **"XR UI Input Module"**
4. Add it
5. Leave all settings as default

**This is CRITICAL** - without this, buttons won't respond to XR controllers!

### Step 3: Add TrackedDeviceGraphicRaycaster to Canvas

1. Select **"Canvas"** GameObject
2. Click **"Add Component"**
3. Search for: **"Tracked Device Graphic Raycaster"**
4. Add it
5. Leave all settings as default

### Step 4: Verify Controllers Have UI Interaction

1. Select **"Left Controller"**
2. Find **"XRRayInteractor"** component
3. Check: **"Enable UI Interaction"** = ✅ **Checked**
4. Repeat for **"Right Controller"**

---

## Fix 3: Verify Button Setup

Your buttons are already wired up, but let's verify:

1. Select **"PlayButton"**
2. In Inspector, find **"Button"** component
3. Check:
   - **"Interactable"** = ✅ Checked
   - **OnClick ()** section should show `SceneManager.OnPlayButtonClick`
4. Repeat for **"OptionsButton"** and **"TutorialButton"**

---

## Quick Checklist

### Fix Double Rays:
- [ ] Removed duplicate "XR Interactor Line Visual" from Left Controller
- [ ] Removed duplicate "XR Interactor Line Visual" from Right Controller
- [ ] Removed duplicate "XRRayInteractor" if present
- [ ] Only ONE ray line per controller now (white, not pink)

### Fix Buttons:
- [ ] EventSystem has "XR UI Input Module" component
- [ ] Canvas has "Tracked Device Graphic Raycaster" component
- [ ] Left Controller → XRRayInteractor → "Enable UI Interaction" = ✅ Checked
- [ ] Right Controller → XRRayInteractor → "Enable UI Interaction" = ✅ Checked
- [ ] All buttons have "Interactable" = ✅ Checked

---

## Expected Result

After fixing:
- ✅ Only ONE white ray line per controller (2 total, not 4)
- ✅ No pink lines
- ✅ Point controller ray at button → Ray changes color/highlights button
- ✅ Press Trigger → Button clicks and works!

---

## Troubleshooting

### Still See Double Rays:

1. **Check child GameObjects:**
   - Expand "Left Controller" in Hierarchy
   - Look for child objects like "Ray Interactor" or "Line Visual"
   - If you see duplicates, delete the extra ones

2. **Check for multiple XR Interactor Line Visual components:**
   - Select controller → Inspector
   - Count how many "XR Interactor Line Visual" components
   - Should be only ONE

### Buttons Still Don't Work:

1. **Check EventSystem:**
   - Must have "XR UI Input Module" (not just "Event System")
   - If missing, add it (Fix 2, Step 2)

2. **Check Canvas:**
   - Must have "Tracked Device Graphic Raycaster"
   - If missing, add it (Fix 2, Step 3)

3. **Check Console for errors:**
   - Open Console (Window → General → Console)
   - Look for red errors
   - Fix any errors related to UI or XR

4. **Test in Play Mode:**
   - Press Play
   - Point ray at button
   - Ray should change color when hovering
   - Press Trigger - button should click

### Pink Line Still Shows:

1. **Assign material to remaining Line Visual:**
   - Select controller
   - Find "XR Interactor Line Visual" component
   - Check "Line Material" field
   - If empty, click circle icon
   - Search for: **"Default-Line"** or create a new material:
     - Right-click in Project → Create → Material
     - Shader: **"Unlit/Color"**
     - Color: White
     - Assign to Line Material

---

## Why This Happened

- **Double rays:** You likely added ray components manually AND used prefabs, or added components twice
- **Buttons not working:** Missing "XR UI Input Module" on EventSystem - this is required for XR button interaction

---

## Most Important Steps

1. ✅ **Remove duplicate ray visual components** (Fix 1)
2. ✅ **Add "XR UI Input Module" to EventSystem** (Fix 2, Step 2) ← CRITICAL!
3. ✅ **Add "Tracked Device Graphic Raycaster" to Canvas** (Fix 2, Step 3)
4. ✅ **Enable UI Interaction on controllers** (Fix 2, Step 4)

After these steps, buttons should work!
