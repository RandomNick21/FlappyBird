using UnityEngine;

public static class CameraExtensions
{
    public static bool IsObjectVisible(this Camera @this, GameObject gameObject)
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(@this),
            gameObject.GetComponent<Renderer>().bounds);
    }
    
    public static bool IsObjectVisible(this Camera @this, Renderer renderer)
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(@this), renderer.bounds);
    }
}
