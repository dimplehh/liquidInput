//#define DEBUG_RENDER

using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

namespace AkilliMum.Standard.D2WeatherEffects
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MinMaxSliderAttribute : Attribute
    {
        public float MinValue { get; private set; }
        public float MaxValue { get; private set; }

        public MinMaxSliderAttribute(float minValue, float maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }
    }
}