# Brain Dissection VR – Setup Guide

Follow these steps in order. Use **BrainDissectionScene** as your working scene.

## 1. Open the scene and confirm the brain

1. Open **Assets/Scenes/BrainDissectionScene.unity**.
2. In the Hierarchy you should see **BrainRoot** with **Allen_brain_final** (the brain model) as a child.
3. With **Preserve Hierarchy** enabled on the FBX (already set in the importer), the brain should show **Allen_brain_Hemisphere_L** and **Allen_brain_Hemisphere_R** under the root. If not, select `Allen_brain_final.fbx` in the Project window, open the Inspector, and enable **Model > Preserve Hierarchy**.
4. Position **BrainRoot** (e.g. Transform position 0, 1, 2) so the brain is in front of the XR Origin.

## 2. Run scene setup (UI and BrainManager)

1. Make sure **BrainDissectionScene** is the active scene (double‑click it in the Project window if needed).
2. In the **top menu bar** click **Tools**, then **Brain Dissection**, then **Setup Scene**.
   - If you don’t see **Brain Dissection**: check the **Console** (Window > General > Console) for red errors. Fix any script errors so the menu can appear.
   - If the dialog says **Brain Root was created**: drag **Allen_brain_final** from the Project (under Assets) onto **BrainRoot** in the Hierarchy so the brain is a child of BrainRoot, then run **Tools > Brain Dissection > Setup Scene** again.
3. After it runs, the setup creates:
   - **BrainSystem** with **BrainManager** and **RegionUIController**
   - **RegionFocusPivot** (where a selected region is shown)
   - **BrainDissectionCanvas** (world-space UI) with:
     - **Left Hemisphere** / **Right Hemisphere** / **Reset Brain** buttons
     - Hover name text and Region details panel
3. In the Hierarchy, select **BrainSystem**. In the Inspector, confirm **Brain Manager** has:
   - **Left Hemisphere** = Allen_brain_Hemisphere_L
   - **Right Hemisphere** = Allen_brain_Hemisphere_R
   - **Region Focus Pivot** and **Region UI Controller** assigned
   If any are missing, drag the corresponding objects from the Hierarchy.

## 3. Create region data (sample or all 132)

- **Sample (5 regions):** **Tools > Brain Dissection > Create Sample Region Data**  
  Creates 5 example RegionData assets in **Assets/Data/BrainRegions** with descriptions.
- **All 132 regions:** **Tools > Brain Dissection > Create All 132 Region Data Assets**  
  Creates one RegionData asset per brain region with placeholder text (you can edit later).

## 4. Add interactivity to regions

1. With **BrainDissectionScene** open, run **Tools > Brain Dissection > Add Region Components To Brain**.
2. This finds each RegionData asset whose **regionId** matches a child of BrainRoot (e.g. `Allen_angular_gyrus_L`) and adds:
   - **BrainRegion** (with that RegionData assigned)
   - **XR Simple Interactable** (for ray hover/select)
   - **Mesh Collider** (if the object has a MeshFilter and no collider)
3. Only regions that have a matching RegionData asset get these components. After creating “Create All 132”, run this again to wire all 132.

## 5. Test in Play mode

1. Enter **Play**.
2. Use the XR ray (or XR Device Simulator) to point at the brain.
3. **Left Hemisphere** / **Right Hemisphere** should dim the other side; **Reset Brain** should show both again.
4. Hover over a configured region: its name should appear in the hover text.
5. Select a region (trigger): it should move to the focus pivot and the details panel should show that region’s text. **Reset Brain** should return the brain to the full view.

## Troubleshooting

- **Buttons do nothing:** Ensure **BrainDissectionCanvas** has the **BrainDissectionUI** component and its **Brain Manager** field points to **BrainSystem**.
- **No hover/select on regions:** Ensure each region has **BrainRegion**, **XR Simple Interactable**, and a **Collider**. Re-run **Add Region Components To Brain**.
- **Hemisphere references missing:** After Setup Scene, assign **Left Hemisphere** and **Right Hemisphere** on **BrainManager** to the L and R hemisphere GameObjects under the brain model.
- **Script errors:** Confirm the project uses the **XR Interaction Toolkit** package and that all scripts in **Assets/Scripts/BrainDissection** compile (check the Console).
