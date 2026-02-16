using UnityEngine;

/// <summary>
/// ScriptableObject holding display and description data for a single brain region.
/// Create via Assets > Create > Brain Dissection > Region Data.
/// </summary>
[CreateAssetMenu(fileName = "NewRegion", menuName = "Brain Dissection/Region Data", order = 1)]
public class RegionData : ScriptableObject
{
    public string regionId = "";
    public string displayName = "";
    public Hemisphere hemisphere = Hemisphere.Left;
    [TextArea(2, 4)]
    public string shortDescription = "";
    [TextArea(4, 10)]
    public string detailedDescription = "";

    public enum Hemisphere
    {
        Left,
        Right
    }
}
