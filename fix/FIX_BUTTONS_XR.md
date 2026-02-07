# Make Buttons Work with XR Controllers

## Your Buttons
- **PlayButton** → Calls `OnPlayButtonClick()` (teleports camera to lab)
- **OptionsButton** → Calls `OnOptionsButtonClick()` (opens brightness panel)
- **TutorialButton** → (needs to be wired up)

---

## Step 1: Add TrackedDeviceGraphicRaycaster to Canvas

This is **CRITICAL** - without this, controllers can't interact with UI buttons.

1. **Select "Canvas"** in Hierarchy
2. In Inspector, click **"Add Component"**
3. Search for: **"Tracked Device Graphic Raycaster"**
4. Add it
5. Leave all settings as default

**This allows XR controllers to detect and click UI buttons.**

---

## Step 2: Verify Controllers Have UI Interaction Enabled

### Left Controller:
1. Select **"Left Controller"** (under XR Origin → Camera Offset)
2. Find **"XRRayInteractor"** component
3. Check: **"Enable UI Interaction"** = ✅ **Checked**
4. If unchecked, check it

### Right Controller:
1. Select **"Right Controller"**
2. Find **"XRRayInteractor"** component
3. Check: **"Enable UI Interaction"** = ✅ **Checked**
4. If unchecked, check it

---

## Step 3: Verify Button Setup

### Check PlayButton:
1. Select **"PlayButton"** in Hierarchy
2. In Inspector, find **"Button"** component
3. Check **"OnClick ()"** section:
   - Should show: `SceneManager.OnPlayButtonClick`
   - If empty, add it:
     - Click **+** button
     - Drag **SceneManager** GameObject into the object field
     - Select: **SceneManager → OnPlayButtonClick()**

### Check OptionsButton:
1. Select **"OptionsButton"**
2. In Inspector, find **"Button"** component
3. Check **"OnClick ()"** section:
   - Should show: `SceneManager.OnOptionsButtonClick`
   - If empty, wire it up (same as PlayButton)

### Check TutorialButton:
1. Select **"TutorialButton"** (or "TutuorialButton" if that's the name)
2. In Inspector, find **"Button"** component
3. Check **"OnClick ()"** section:
   - If empty, add a method:
     - Click **+** button
     - Drag **SceneManager** GameObject into the object field
     - You'll need to add a method in SceneManager.cs (see Step 4)

---

## Step 4: Add Tutorial Button Method (If Needed)

If TutorialButton doesn't have a method yet, add one to SceneManager.cs:

1. Open **`Assets/SceneManager.cs`**
2. Add this method (after `CloseBrightnessPanel()`):

```csharp
public void OnTutorialButtonClick()
{
    // Add your tutorial logic here
    // For example:
    Debug.Log("Tutorial button clicked!");
    // Or load a tutorial scene, show tutorial UI, etc.
}
```

3. Save the file
4. Wire it up in TutorialButton's OnClick event (Step 3)

---

## Step 5: Verify Canvas Settings

1. Select **"Canvas"**
2. In Inspector, check **"Canvas"** component:
   - **Render Mode**: Should be **"World Space"** or **"Screen Space - Overlay"**
   - **World Space** is better for VR (buttons appear in 3D space)
   - **Screen Space - Overlay** works but may not be ideal for VR

3. If using **World Space**:
   - Position Canvas where you want buttons to appear
   - Scale it appropriately (e.g., Scale: 0.001, 0.001, 0.001)
   - Make sure it's visible from where the player starts

---

## Step 6: Test in Play Mode

1. **Save Scene** (Ctrl+S / Cmd+S)
2. Press **Play**
3. Point controller ray at buttons
4. Press **Trigger** to click
5. Buttons should respond!

---

## Troubleshooting

### Buttons Don't Respond:

1. **Check Canvas has TrackedDeviceGraphicRaycaster:**
   - Select Canvas → Inspector → Should see "Tracked Device Graphic Raycaster" component
   - If missing, add it (Step 1)

2. **Check Controllers Enable UI Interaction:**
   - Select Left/Right Controller
   - XRRayInteractor → "Enable UI Interaction" = ✅ Checked

3. **Check Ray is Hitting Buttons:**
   - In Play mode, point controller at button
   - Ray should turn green/change color when hovering over button
   - If ray doesn't change, button might be behind something or Canvas settings wrong

4. **Check Button Interactable:**
   - Select button → Inspector → Button component
   - **"Interactable"** = ✅ Checked
   - If unchecked, check it

5. **Check Button Layer:**
   - Select button → Inspector → Layer should be "UI" (Layer 5)
   - Canvas should also be Layer "UI"

6. **Check OnClick Event:**
   - Select button → Inspector → Button → OnClick()
   - Should have SceneManager method assigned
   - If empty, wire it up (Step 3)

### Ray Doesn't Show:

- Follow the controller setup guides (FIX_LEFT_CONTROLLER_AND_PINK.md)
- Make sure XR Interactor Line Visual component exists on controllers

### Buttons Work But Nothing Happens:

- Check Console for errors
- Verify SceneManager GameObject has all public fields assigned:
  - `cameraTransform` → Assign Main Camera
  - `labPoint` → Assign lab point GameObject
  - `brightnessSliderPanel` → Assign brightness panel GameObject
  - `pLight` → Assign your light
  - `slider` → Assign brightness slider
  - `contrastSlider` → Assign contrast slider
  - `meshRenderers` → Add your mesh renderers to the list

---

## Quick Checklist

- [ ] Canvas has "Tracked Device Graphic Raycaster" component
- [ ] Left Controller → XRRayInteractor → "Enable UI Interaction" = ✅ Checked
- [ ] Right Controller → XRRayInteractor → "Enable UI Interaction" = ✅ Checked
- [ ] PlayButton → OnClick → SceneManager.OnPlayButtonClick wired up
- [ ] OptionsButton → OnClick → SceneManager.OnOptionsButtonClick wired up
- [ ] TutorialButton → OnClick → SceneManager.OnTutorialButtonClick wired up (if method exists)
- [ ] All buttons have "Interactable" = ✅ Checked
- [ ] SceneManager GameObject has all public fields assigned

---

## Expected Result

After setup:
- ✅ Point controller ray at button → Ray changes color/highlights button
- ✅ Press Trigger → Button clicks
- ✅ PlayButton → Camera teleports to lab point
- ✅ OptionsButton → Brightness panel opens
- ✅ TutorialButton → Does whatever you programmed it to do

---

## Controls

- **Point Controller**: Aim ray at button
- **Trigger Button**: Click/select button
- **Ray Color Change**: Indicates button is hovered/selected
