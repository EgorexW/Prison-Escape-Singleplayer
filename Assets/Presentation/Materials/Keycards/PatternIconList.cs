// AI GENERATED

// A list of icons for the Pattern Tiler shader, and the Unity-side bake that
// turns it into the Texture2DArray the material wants.
//
// Drop this file anywhere in the project. Create > Pattern Tiler > Icon List,
// drag icons into the list, press Build. The array asset appears next to the
// list and is assigned to the material if you gave it one.
//
// Why an array and not one texture per icon: a Texture2DArray is a single
// sampler and a single bind however many icons there are, so the count has no
// practical ceiling -- sixteen separate texture properties would have run the
// shader out of samplers against URP's own. Each icon is still its own image,
// so neighbours cannot bleed into each other in the lower mips.

using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "PatternIcons", menuName = "Others/Pattern Tiler Icon List")]
public class PatternIconList : ScriptableObject
{
    [Tooltip("Icons, in any order. Cells cycle through them: two alternate " +
             "like a checkerboard, three means every third differs, and so on.")]
    public Texture2D[] icons = new Texture2D[0];

    [Tooltip("Longest side of one slice, in pixels. Every icon is fitted into " +
             "a slice this big; they all share one size, as an array requires.")]
    [Range(32, 1024)]
    public int resolution = 256;

    [Tooltip("Clear space around an icon inside its slice, as a fraction of " +
             "the slice. This is the icon's own breathing room; the spacing " +
             "between icons is Gap X / Gap Y on the material.")]
    [Range(0f, 0.3f)]
    public float margin = 0.06f;

    [Tooltip("Optional. Built arrays are assigned to this material's Icons slot.")]
    public Material material;

    [Tooltip("The array this list was baked into. Filled in by Build.")]
    public Texture2DArray built;

#if UNITY_EDITOR
    // A pixel counts as part of the icon above this. Not zero: a compressed
    // source has a little noise in the empty corners, and cropping to that
    // noise would quietly change every icon's spacing.
    const byte CoverageFloor = 4;

    /// <summary>Bake the list into a Texture2DArray asset. Editor only.</summary>
    public string Build()
    {
        var used = new System.Collections.Generic.List<Texture2D>();
        foreach (var t in icons)
            if (t != null)
                used.Add(t);
        if (used.Count == 0)
            return "No icons in the list.";

        // Read each icon's coverage and crop it to what it actually draws. The
        // shader spaces cells by the icon, so leftover transparent padding in
        // the source would silently widen the gaps.
        var masks = new Mask[used.Count];
        for (int i = 0; i < used.Count; i++)
        {
            masks[i] = ReadMask(used[i]);
            if (masks[i].width == 0)
                return string.Format("'{0}' looks empty -- nothing to draw.", used[i].name);
        }

        int maxW = 0, maxH = 0;
        foreach (var m in masks)
        {
            maxW = Mathf.Max(maxW, m.width);
            maxH = Mathf.Max(maxH, m.height);
        }

        // One cell, sized to the largest icon plus its margin. Every icon is
        // scaled by the same factor, so a tall chevron and a square cog keep
        // their real relative sizes instead of each being blown up to fill.
        int pad = Mathf.RoundToInt(Mathf.Max(maxW, maxH) * margin);
        float cellW = maxW + pad * 2f;
        float cellH = maxH + pad * 2f;
        float fit = Mathf.Min(1f, resolution / Mathf.Max(cellW, cellH));
        int sliceW = Mathf.Max(1, Mathf.RoundToInt(cellW * fit));
        int sliceH = Mathf.Max(1, Mathf.RoundToInt(cellH * fit));

        var array = new Texture2DArray(sliceW, sliceH, masks.Length,
                                       TextureFormat.RGBA32, true, false);
        array.wrapMode = TextureWrapMode.Clamp;
        array.filterMode = FilterMode.Bilinear;
        array.anisoLevel = 4;

        for (int i = 0; i < masks.Length; i++)
        {
            int w = Mathf.Max(1, Mathf.RoundToInt(masks[i].width * fit));
            int h = Mathf.Max(1, Mathf.RoundToInt(masks[i].height * fit));
            array.SetPixels32(Compose(masks[i], w, h, sliceW, sliceH), i);
        }
        array.Apply(true, false);

        var saved = Save(array);
        built = saved;
        if (material != null && material.HasProperty("_Icons"))
            material.SetTexture("_Icons", saved);

        EditorUtility.SetDirty(this);

        var report = new StringBuilder();
        report.AppendFormat("{0} icon{1}, {2}x{3} per slice",
                            masks.Length, masks.Length == 1 ? "" : "s", sliceW, sliceH);
        if (fit < 1f)
            report.AppendFormat(" (scaled to {0:0}% to fit {1}px)", fit * 100f, resolution);
        if (material != null && !material.HasProperty("_Icons"))
            report.Append("  -- the material has no Icons slot, so nothing was assigned");
        return report.ToString();
    }

    struct Mask
    {
        public byte[] coverage;      // one byte per pixel, row 0 at the bottom
        public int width, height;
    }

    /// <summary>Icon -> coverage, cropped to what it draws.</summary>
    static Mask ReadMask(Texture2D source)
    {
        // Blit through a render texture rather than requiring the source be
        // marked readable: the icons are the user's assets and their import
        // settings are none of our business.
        var rt = RenderTexture.GetTemporary(source.width, source.height, 0,
                                            RenderTextureFormat.ARGB32,
                                            RenderTextureReadWrite.Linear);
        var previous = RenderTexture.active;
        Graphics.Blit(source, rt);
        RenderTexture.active = rt;
        var flat = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false, true);
        flat.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
        flat.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        var pixels = flat.GetPixels32();
        DestroyImmediate(flat);

        // Use alpha when there is any to use; an icon that is opaque all over
        // is dark-on-light art, so read its darkness as coverage instead.
        bool alphaVaries = false;
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].a < 255)
            {
                alphaVaries = true;
                break;
            }
        }

        int w = source.width, h = source.height;
        var full = new byte[pixels.Length];
        for (int i = 0; i < pixels.Length; i++)
        {
            if (alphaVaries)
            {
                full[i] = pixels[i].a;
            }
            else
            {
                int lum = (pixels[i].r * 77 + pixels[i].g * 150 + pixels[i].b * 29) >> 8;
                full[i] = (byte)(255 - Mathf.Clamp(lum, 0, 255));
            }
        }

        int minX = w, minY = h, maxX = -1, maxY = -1;
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                if (full[y * w + x] <= CoverageFloor)
                    continue;
                if (x < minX) minX = x;
                if (x > maxX) maxX = x;
                if (y < minY) minY = y;
                if (y > maxY) maxY = y;
            }
        }
        if (maxX < 0)
            return new Mask { coverage = new byte[0], width = 0, height = 0 };

        int cw = maxX - minX + 1, ch = maxY - minY + 1;
        var cropped = new byte[cw * ch];
        for (int y = 0; y < ch; y++)
            for (int x = 0; x < cw; x++)
                cropped[y * cw + x] = full[(minY + y) * w + minX + x];
        return new Mask { coverage = cropped, width = cw, height = ch };
    }

    /// <summary>Resize the mask to w x h and centre it in a slice.</summary>
    static Color32[] Compose(Mask mask, int w, int h, int sliceW, int sliceH)
    {
        var slice = new Color32[sliceW * sliceH];
        int offsetX = (sliceW - w) / 2;
        int offsetY = (sliceH - h) / 2;

        for (int y = 0; y < h; y++)
        {
            // Average the source pixels this one covers, rather than picking
            // one of them: point sampling a downscale drops thin strokes.
            int sy0 = Mathf.Clamp(Mathf.FloorToInt(y * (float)mask.height / h), 0, mask.height - 1);
            int sy1 = Mathf.Clamp(Mathf.CeilToInt((y + 1) * (float)mask.height / h), sy0 + 1, mask.height);
            for (int x = 0; x < w; x++)
            {
                int sx0 = Mathf.Clamp(Mathf.FloorToInt(x * (float)mask.width / w), 0, mask.width - 1);
                int sx1 = Mathf.Clamp(Mathf.CeilToInt((x + 1) * (float)mask.width / w), sx0 + 1, mask.width);

                int total = 0, n = 0;
                for (int sy = sy0; sy < sy1; sy++)
                {
                    for (int sx = sx0; sx < sx1; sx++)
                    {
                        total += mask.coverage[sy * mask.width + sx];
                        n++;
                    }
                }
                byte a = (byte)(n > 0 ? total / n : 0);

                int tx = offsetX + x, ty = offsetY + y;
                if (tx < 0 || ty < 0 || tx >= sliceW || ty >= sliceH)
                    continue;
                // White, with the shape in alpha: the material's Tint decides
                // the colour, so one array serves every colour of card.
                slice[ty * sliceW + tx] = new Color32(255, 255, 255, a);
            }
        }
        return slice;
    }

    /// <summary>Write the array beside this asset, keeping its GUID.</summary>
    Texture2DArray Save(Texture2DArray array)
    {
        var listPath = AssetDatabase.GetAssetPath(this);
        var path = string.IsNullOrEmpty(listPath)
            ? "Assets/" + name + " Array.asset"
            : Path.Combine(Path.GetDirectoryName(listPath),
                           Path.GetFileNameWithoutExtension(listPath) + " Array.asset");
        path = path.Replace('\\', '/');

        // Overwrite in place when it already exists, so materials pointing at
        // the array keep pointing at it instead of going pink on every rebuild.
        var existing = AssetDatabase.LoadAssetAtPath<Texture2DArray>(path);
        if (existing != null)
        {
            EditorUtility.CopySerialized(array, existing);
            AssetDatabase.SaveAssets();
            return existing;
        }
        AssetDatabase.CreateAsset(array, path);
        AssetDatabase.SaveAssets();
        return array;
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(PatternIconList))]
public class PatternIconListEditor : Editor
{
    string report;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var list = (PatternIconList)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("Build Icon Array", GUILayout.Height(28)))
            report = list.Build();

        if (!string.IsNullOrEmpty(report))
            EditorGUILayout.HelpBox(report, MessageType.Info);

        EditorGUILayout.HelpBox(
            "Assign the built array to the material's Icons slot. Icon Count on " +
            "the material can stay 0: the shader reads how many icons there are " +
            "from the array itself.", MessageType.None);
    }
}
#endif
