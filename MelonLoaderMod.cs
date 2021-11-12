using HarmonyLib;
using MelonLoader;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Gizmos
{
    public static class BuildInfo
    {
        public const string Name = "RuntimeGizmos"; // Name of the Mod.  (MUST BE SET)
        public const string Author = null; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class RuntimeGizmos : MelonMod
    {
        private GameObject lineRendererObject;
        public static LineRenderer lineRenderer;

        public override void OnApplicationStart()
        {
            //MelonLogger.Msg("Loading AssetBundle...");
            var category = MelonPreferences.CreateCategory("RuntimeGizmos");
            var entry = category.CreateEntry("patchRaycastsAndOverlaps", false, "patchRaycastsAndOverlaps");
            if (entry.Value) HarmonyInstance.Patch(
                                typeof(Physics).GetMethod("Raycast",
                                    new Type[] { typeof(Vector3), typeof(Vector3), typeof(float) }),
                                postfix: new HarmonyMethod(typeof(RaycastPatch).GetMethod("Postfix"))
                                );
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            lineRendererObject = new GameObject();
            lineRenderer = lineRendererObject.AddComponent<LineRenderer>();
            lineRenderer.material = Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.name == "ZPEFM_lighting");
        }
    }

    public static class RaycastPatch
    {
        public static void Postfix(Vector3 origin, Vector3 direction, float maxDistance)
        {
            MelonLogger.Msg("Postfix fired with values:");
            MelonLogger.Msg($"Origin: {origin.x}, {origin.y}, {origin.z}");
            MelonLogger.Msg($"Direction: {direction.x}, {direction.y}, {direction.z}");
            MelonLogger.Msg($"Distance: {maxDistance}");
            RuntimeGizmos.lineRenderer.SetPositions(new Vector3[]
            {
                origin,
                origin + direction.normalized * maxDistance
            });
        }

    }

    public static class Draw
    {
        public static void Ray(Ray ray, Color color)
        {

        }

        public static void Ray(Ray ray, Color color, float distance)
        {

        }

        public static void Ray(Vector3 start, Vector3 end, Color color)
        {

        }

    }

    public class GizmoDrawer
    {
        static List<Sphere> spheres = new List<Sphere>();
    }

    struct Sphere
    {

    }

    struct Line
    {

    }

}
