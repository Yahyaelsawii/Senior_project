# Crash Fix Instructions

## Critical Issues Found

1. **Missing Scripts** - XR components have broken script references
2. **Input Handling** - Set to "Both" which crashes on Android
3. **Plugin Alignment** - OpenXR plugins not aligned (minor, but can cause issues)

## 🚨 CRITICAL FIX - Scene Corruption Detected 🚨

**Root Cause Found:** Your scene file (`level0`) is corrupted due to missing scripts!

### Immediate Fix (Do This First!)

1. **SAVE YOUR SCENE** (Ctrl+S / Cmd+S)
2. Go to **XR → Fix Corrupted Scene (CRITICAL)**
3. This will:
   - Remove ALL broken XR components
   - Recreate them from clean prefabs
   - Fix missing scripts
   - Clean up corrupted scene data
4. **SAVE THE SCENE AGAIN** after the fix
5. **Clean your build folder** (delete old build)
6. Build again

### Why This Happened

The crash logs show:
- `The file '.../level0' is corrupted!`
- `[Position out of bounds!]`
- Multiple missing scripts on XR components

Missing scripts cause Unity to write invalid data to the scene file, corrupting it. The corrupted scene then crashes when loaded on the device.

## Quick Fix Steps (If Still Crashing After Critical Fix)

### Step 1: Run Diagnostics
1. Go to **XR → Check XR Setup (Diagnostics)**
2. This will show you exactly what's broken
3. Follow the recommended actions

### Step 2: Aggressive Fix (If Critical Fix Didn't Work)
1. **SAVE YOUR SCENE FIRST!** (Ctrl+S / Cmd+S)
2. Go to **XR → Replace Broken XR Components (Aggressive)**
3. This will DELETE and RECREATE XR Origin from the prefab
4. Save the scene again

### Step 3: Remove Missing Scripts
1. Go to **XR → Remove All Missing Scripts**
2. This cleans up any remaining broken references
3. Save the scene

### Step 4: Setup XR Components
1. Go to **XR → Setup XR Components**
2. This adds Ray Interactors and Locomotion
3. Save the scene

### Step 5: Verify Input Handling
✅ **Already Fixed** - Changed from "Both" to "Input System Package (New)"
- Double-check: Edit → Project Settings → Player → Other Settings → Active Input Handling = "Input System Package (New)"

### Step 3: Verify Setup

After fixing, check:
- ✅ XR Origin has all components (no missing script warnings)
- ✅ XR Interaction Manager exists in scene
- ✅ Left/Right Controllers have XR Controller components
- ✅ Canvas has TrackedDeviceGraphicRaycaster

### Step 4: Re-run XR Setup

1. Go to **XR → Setup XR Components**
2. This will add Ray Interactors and Locomotion

## Why It Crashed

The app crashed because:
1. **Missing Scripts** - Critical XR components couldn't initialize
2. **Input Handler Conflict** - "Both" mode causes crashes on Android/Quest
3. **Broken References** - Script GUIDs point to missing or moved scripts

## Prevention

- Always use prefabs from the XR Interaction Toolkit samples
- Don't manually delete components from XR Origin
- Use "Input System Package (New)" for Android builds
- Save scene after making XR changes

## If Still Crashing After All Fixes

### 1. Get Crash Logs (CRITICAL)
Connect your Quest via USB and run:
```bash
adb logcat -c  # Clear logs
adb logcat | grep -i "unity\|fatal\|crash\|exception"
```
This will show the actual crash reason. Common issues:
- Missing native libraries
- Permission denied
- Null reference exceptions
- XR initialization failures

### 2. Check Device Compatibility
- Quest 3S should work, but verify:
  - Developer Mode is enabled on Quest
  - USB debugging is enabled
  - Quest is connected and recognized by `adb devices`

### 3. Minimal Test Scene
Create a new scene and test:
1. File → New Scene
2. Add XR Origin prefab: `Assets/Samples/XR Interaction Toolkit/2.6.5/Starter Assets/Prefabs/XR Origin (XR Rig).prefab`
3. Build and test
4. If this works → issue is scene-specific
5. If this crashes → issue is project-wide

### 4. Re-import XR Packages
1. Window → Package Manager
2. Find "XR Interaction Toolkit"
3. Click "Reimport" (three dots menu)
4. Find "OpenXR Plugin"
5. Click "Reimport"

### 5. Check Build Settings
Verify these settings:
- **Build Target**: Android
- **Minimum API Level**: 29
- **Target API Level**: 33 or higher
- **Graphics API**: Vulkan (recommended for Quest)
- **Multithreaded Rendering**: Enabled
- **OpenXR**: Enabled in XR Plug-in Management → Android

### 6. Check Android Manifest Permissions
The app might be missing required permissions. Check:
- `AndroidManifest.xml` in `Assets/Plugins/Android/`
- Should have: `android.permission.VIBRATE`, `android.permission.INTERNET` (if needed)

## Build Settings Check

Before building, verify:
- ✅ Build Target: Android
- ✅ Minimum API Level: 29 (Android 10)
- ✅ Target API Level: 33+ (recommended)
- ✅ Input System: "Input System Package (New)" ✅ FIXED
- ✅ XR Plug-in Management: OpenXR enabled for Android
