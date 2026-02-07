# How to Find Button OnClick() in Inspector

## The OnClick() Section Might Be Collapsed

The Button component has an **"OnClick ()"** section that might be collapsed. Here's how to find it:

---

## Step 1: Select PlayButton

1. In Hierarchy, find and select **"PlayButton"**

---

## Step 2: Find Button Component in Inspector

1. In Inspector, look for a component called **"Button"**
2. It should be near the top (after RectTransform)
3. If you don't see it, scroll down - it might be below other components

---

## Step 3: Expand the Button Component

1. Click on the **"Button"** component header to expand it
2. You should see sections like:
   - Navigation
   - Transition
   - Colors
   - **OnClick ()** ← This is what you need!

---

## Step 4: Check OnClick() Section

1. Scroll down in the Button component
2. Look for **"OnClick ()"** section
3. It should show:
   - A **+** button (to add more events)
   - A list showing: `SceneManager.OnPlayButtonClick`
   - Or it might be empty

---

## If OnClick() Section is Empty or Missing

### Option A: Add the Event Manually

1. In the **"OnClick ()"** section, click the **+** button
2. You'll see a new entry with:
   - **Runtime Only** dropdown
   - **No Function** dropdown
   - **Object** field (with a circle icon)
3. **Drag the SceneManager GameObject** from Hierarchy into the **Object** field
4. Click the **"No Function"** dropdown
5. Select: **SceneManager → OnPlayButtonClick()**

---

## Alternative: Verify Buttons Are Already Wired

The scene file shows buttons ARE already wired up! They might just be working. Let's verify:

### Quick Test:

1. **Add TrackedDeviceGraphicRaycaster to Canvas** (most important!)
   - Select Canvas → Add Component → "Tracked Device Graphic Raycaster"

2. **Verify Controllers Have UI Interaction:**
   - Select Left Controller → XRRayInteractor → "Enable UI Interaction" = ✅ Checked
   - Select Right Controller → XRRayInteractor → "Enable UI Interaction" = ✅ Checked

3. **Test in Play Mode:**
   - Press Play
   - Point controller ray at button
   - Press Trigger
   - Button should work!

---

## If You Still Can't See OnClick()

### Check These:

1. **Is the Button component enabled?**
   - Look for a checkbox next to "Button" component name
   - Make sure it's ✅ checked

2. **Is the GameObject active?**
   - Top-left of Inspector, make sure PlayButton is ✅ active

3. **Try selecting a different button:**
   - Select OptionsButton instead
   - See if you can see its OnClick() section

4. **Check if you're looking at the right GameObject:**
   - Make sure you selected "PlayButton" (the parent)
   - Not "PlayButton" → "Text" (the child)

---

## What the OnClick() Should Look Like

When you find it, you should see:

```
OnClick ()
  ┌─────────────────────────────────────┐
  │ Runtime Only  │  No Function       │
  │               │  SceneManager      │
  │               │  OnPlayButtonClick │
  └─────────────────────────────────────┘
  [+]
```

---

## Most Important: Make Buttons Work with XR

Even if you can't see OnClick() in Inspector, the buttons are already wired up in the scene file. The **critical step** is:

1. ✅ **Add TrackedDeviceGraphicRaycaster to Canvas**
2. ✅ **Enable UI Interaction on controllers**
3. ✅ **Test in Play Mode**

The buttons should work once you add TrackedDeviceGraphicRaycaster!

---

## Quick Checklist

- [ ] Selected PlayButton (parent GameObject, not child)
- [ ] Found "Button" component in Inspector
- [ ] Expanded Button component (clicked header)
- [ ] Scrolled down to find "OnClick ()" section
- [ ] Verified OnClick() has SceneManager.OnPlayButtonClick (or added it)
- [ ] Added TrackedDeviceGraphicRaycaster to Canvas
- [ ] Enabled UI Interaction on controllers
- [ ] Tested in Play Mode
