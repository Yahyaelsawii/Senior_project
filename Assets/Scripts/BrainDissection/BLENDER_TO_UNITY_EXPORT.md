# Export Blender Scene to Unity – Step by Step (Beginner)

This guide walks you through exporting your Blender operating room (with its look and lighting) into Unity.

---

## Part 1: Prepare Your Scene in Blender

### 1.1 Check Your Scale
- In Blender, 1 unit = 1 meter by default. Unity also uses 1 unit = 1 meter.
- If your room is huge or tiny in Blender, scale the whole scene so the room is roughly 4–6 meters across before exporting (Select All → Scale with S).

### 1.2 Apply All Transforms (Important!)
- Select everything: **A** (Select All).
- **Object → Apply → All Transforms** (or **Ctrl+A → All Transforms**).
- This bakes scale/rotation into the mesh so Unity gets the right size.

### 1.3 (Optional) Bake Lighting for “Blender Look”
Blender’s lights don’t become real lights in Unity. To keep that “amazing” lit look, you can bake lighting onto textures in Blender and use them in Unity.

- Switch to **Shading** workspace (top tabs).
- In the **Outliner**, make sure your room objects are in a collection.
- Add an **Image Texture** node to the material that will receive the bake (e.g. floor/walls).
- Create a **New Image** (e.g. 2048×2048), name it like `Room_Bake`.
- In **Render Properties** (camera icon):
  - **Engine**: Cycles.
  - **Bake** section: **Bake Type** = **Diffuse**, check **Direct + Indirect**, uncheck **Color** if you only want light/shadow.
- Select the object(s) to bake, then **Render → Bake**.
- **Image → Save As** and save the baked image (e.g. PNG) next to your project.

You can skip baking and light the scene in Unity instead (see Part 3).

---

## Part 2: Export from Blender as FBX

### 2.1 Export
- **File → Export → FBX (.fbx)**.

### 2.2 Export Settings (use these so Unity gets it right)

| Setting | Value | Why |
|--------|--------|-----|
| **Path** | Choose a folder (e.g. your Unity project `Assets` or `Assets/Models`) | So the file lands where Unity can see it. |
| **Include** | **Selected Objects** only if you want just the room; otherwise **All** | Keeps export clean. |
| **Transform** | | |
| → **Scale** | **1.00** | Unity and Blender both use meters. |
| → **Apply Scalings** | **FBX All** | Keeps scale consistent. |
| → **Forward** | **-Z Forward** | Matches Unity’s convention. |
| → **Up** | **Y Up** | Matches Unity. |
| **Geometry** | | |
| → **Apply Modifiers** | ✓ On | So subdivided/corrected meshes export. |
| → **Smoothing** | **Face** or **Normals** | Keeps smooth shading. |
| **Armature** | (leave default if no characters) | Not needed for a static room. |

### 2.3 Export
- Click **Export FBX**. You’ll get one `.fbx` file (e.g. `operating_room.fbx`).

### 2.4 Textures
- Copy all texture images (e.g. from the `.fbm` folder or your Blender project) into the same Unity folder as the FBX, or into `Assets/Textures`. Unity will use them when you assign materials (Part 3).

---

## Part 3: Import into Unity

### 3.1 Bring the FBX into Unity
- In **Unity**, in the **Project** window, go to the folder where you want the room (e.g. `Assets` or `Assets/Models`).
- Copy the `operating_room.fbx` file into that folder (or drag it from Windows Explorer into the Project window).
- Unity will import it automatically. Wait for the spinner to finish.

### 3.2 Fix Scale (if the room is huge or tiny)
- Click the FBX in the Project window.
- In the **Inspector**, open **Model**.
- Set **Scale Factor** to **1** (or 0.01 if the room came in 100× too big).
- Click **Apply**.

### 3.3 Put the Room in the Scene
- Drag `operating_room` from the Project window into the **Hierarchy** (or into the Scene view).
- Use the **Transform** tool (W = move) to position it where you want (e.g. around the brain and tools).

### 3.4 Materials and Textures (if they look gray/wrong)
- Expand the FBX in the Project window (arrow next to it) and open **Materials**.
- For each material that looks wrong:
  - Click the material.
  - In Inspector, set **Shader** to **Universal Render Pipeline → Lit** (if you use URP) or **Standard**.
  - Assign **Albedo** (base color) texture, and **Normal Map** if you have one.
  - If you baked lighting in Blender, use the baked texture as **Albedo** or in a second material slot so the lit look appears.

### 3.5 Lighting in Unity (so it looks “amazing” like Blender)
Blender’s actual lights don’t come into Unity. You have two options:

**Option A – Use Unity’s lighting (easiest)**  
- In Unity menu: **Tools → Brain Dissection → Fix Room Lighting**.
- This sets bright ambient + fill lights so the room is evenly lit (similar to your Blender look). No extra steps.

**Option B – Baked lighting in Unity (closest to Blender)**  
- **Window → Rendering → Lighting**.
- In **Environment**, set **Skybox** and **Sun Source** (your Directional Light).
- Under **Lightmapping**, set **Lightmapper** to **Progressive CPU** (or **GPU** if available).
- Check **Baked Global Illumination**.
- Select your **Directional Light**, set **Mode** to **Mixed** or **Baked**.
- Click **Generate Lighting** and wait. Unity will bake shadows and GI into lightmaps so the room looks much closer to Blender.

---

## Part 4: Quick Checklist

- [ ] Blender: Apply All Transforms (Ctrl+A → All Transforms).
- [ ] Blender: Export FBX with **Y Up**, **-Z Forward**, **Scale 1**, **Apply Modifiers**.
- [ ] Copy FBX + textures into Unity’s `Assets` (or subfolder).
- [ ] Unity: Adjust FBX **Scale Factor** in Model import if needed.
- [ ] Unity: Drag FBX into scene and position it.
- [ ] Unity: Fix materials (URP Lit, assign textures).
- [ ] Unity: **Tools → Brain Dissection → Fix Room Lighting** (or bake lighting for best quality).

If you do these steps, you’ll have your Blender scene and its “amazing” look in Unity. For VR, using **Fix Room Lighting** plus optional **Baked Global Illumination** gives the best balance of look and performance.
