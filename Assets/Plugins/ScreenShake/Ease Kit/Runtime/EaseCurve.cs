using System;
using UnityEngine;

/// <summary>
/// Represents a serializable easing curve that evaluates float values using a selected easing function.
/// Supports caching and editor-time hashing to improve performance in the Unity Editor.
/// </summary>
[Serializable]
public class EaseCurve
{
    [SerializeField] private Ease.Type m_Type;
    [NonSerialized] private Func<float, float> m_Func;
#if UNITY_EDITOR
    [SerializeField, HideInInspector] private int m_Hash;
#endif

    private Func<float, float> func
    {
        get
        {
#if UNITY_EDITOR
            int hash = m_Type.GetHashCode();
            if (m_Hash != hash || m_Func == null)
            {
                m_Func = Ease.Func(m_Type);
                m_Hash = hash;
            }
#else
            m_Func ??= Ease.Func(m_Type);
#endif
            return m_Func;
        }
    }

    /// <summary>
    /// Gets or sets the easing type used by this curve. 
    /// Changing the type will update the cached easing function.
    /// </summary>
    public Ease.Type type
    {
        get => m_Type; set
        {
            m_Type = value;
            m_Func = Ease.Func(m_Type);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EaseCurve"/> class with the specified easing type.
    /// </summary>
    /// <param name="type">The easing type to use for evaluating values.</param>
    public EaseCurve(Ease.Type type)
    {
        m_Type = type;
        m_Func = Ease.Func(type);
    }

    /// <summary>
    /// Evaluates the curve at the given input value using the selected easing function.
    /// </summary>
    /// <param name="x">The input value, typically between 0 and 1.</param>
    /// <returns>The result of applying the easing function to <paramref name="x"/>.</returns>
    public float Calc(float x)
    {
        return func.Invoke(x);
    }
    /// <summary>
    /// Returns a string that represents the current <see cref="EaseCurve"/>.
    /// </summary>
    /// <returns>A string describing the curve and its easing type.</returns>
    public override string ToString()
    {
        return $"EaseCurve ({m_Type})";
    }
}
