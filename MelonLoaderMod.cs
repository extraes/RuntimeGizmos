using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private AssetBundle bundle;
        public static Material mat;

        public override void OnApplicationStart()
        {
            var category = MelonPreferences.CreateCategory("RuntimeGizmos");
            var entry = category.CreateEntry("patchRaycastsAndOverlaps", false, "patchRaycastsAndOverlaps");
            if (entry.Value) PatchRaycasts();

            MelonLogger.Msg("Loading AssetBundle...");


            using (Stream stream = Assembly.GetManifestResourceStream("RuntimeGizmos.Resources.materials"))
            {
                using (MemoryStream ms = new MemoryStream((int)stream.Length))
                {
                    stream.CopyTo(ms);
                    bundle = AssetBundle.LoadFromMemory(ms.ToArray());
                }
            }

            mat = bundle.LoadAsset<Material>("assets/bundledassets/wireframe.mat");
            mat.hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            lineRendererObject = new GameObject();
            lineRenderer = lineRendererObject.AddComponent<LineRenderer>();
            lineRenderer.material = Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(m => m.name == "ZPEFM_lighting");
        }

        private void PatchRaycasts()
        {
            HarmonyMethod postfix1 = new HarmonyMethod(typeof(RaycastPatches).GetMethod("Postfix"));
            HarmonyInstance.Patch(typeof(Physics).GetMethod("Raycast", new Type[] { typeof(Vector3), typeof(Vector3), typeof(RaycastHit), typeof(float), typeof(int) }),
                                  postfix: postfix1);
            HarmonyInstance.Patch(typeof(Physics).GetMethod("Raycast", new Type[] { typeof(Vector3), typeof(Vector3), typeof(float), typeof(int), typeof(QueryTriggerInteraction) }),
                                  postfix: postfix1);
            HarmonyInstance.Patch(typeof(Physics).GetMethod("Raycast", new Type[] { typeof(Vector3), typeof(Vector3), typeof(float), typeof(int) }),
                                  postfix: postfix1);
            HarmonyInstance.Patch(typeof(Physics).GetMethod("Raycast", new Type[] { typeof(Vector3), typeof(Vector3), typeof(float) }),
                                  postfix: postfix1);
            HarmonyInstance.Patch(typeof(Physics).GetMethod("Raycast", new Type[] { typeof(Vector3), typeof(Vector3), typeof(RaycastHit), typeof(float), typeof(int), typeof(QueryTriggerInteraction) }),
                                  postfix: postfix1);
            HarmonyInstance.Patch(typeof(Physics).GetMethod("Raycast", new Type[] { typeof(Vector3), typeof(Vector3), typeof(RaycastHit), typeof(float) }),
                                  postfix: postfix1);
        }
    }

    public static class RaycastPatches
    {
        public static void Postfix(Vector3 origin, Vector3 direction, float maxDistance)
        {
            MelonLogger.Msg("Postfix1 fired with values:");
            MelonLogger.Msg($"Origin: {origin.x}, {origin.y}, {origin.z}");
            MelonLogger.Msg($"Direction: {direction.x}, {direction.y}, {direction.z}");
            MelonLogger.Msg($"Distance: {maxDistance}");
            RuntimeGizmos.lineRenderer.SetPositions(new Vector3[]
            {
                origin,
                origin + direction.normalized * maxDistance
            });
            var cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube1.transform.position = origin;
            cube1.transform.localScale = Vector3.one / 10f;
            cube1.GetComponent<MeshRenderer>().material = RuntimeGizmos.mat;

            var cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube2.transform.position = origin + direction.normalized * maxDistance;
            cube2.transform.localScale = Vector3.one / 10f;
            cube2.GetComponent<MeshRenderer>().material = RuntimeGizmos.mat;
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
