
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PMSClient.Helpers
{
    public enum TargetType
    {
        CIGS,
        CuGaSe,
        InSe,
        InS,
        Mo,
        WTi,
        Unknown
    }

    public static class CompositionHelper
    {
        public static string GetGa(string comp)
        {
            if (WhatItIs(comp) == TargetType.CIGS)
            {
                string pattern = @"Ga(\d+\.\d+|\d+)";
                var match = Regex.Match(comp, pattern);
                return match.Groups[0].Value;
            }
            return "无法判断";
        }
        public static TargetType WhatItIs(string comp)
        {
            if (comp.Contains("Mo"))
            {
                return TargetType.Mo;
            }

            if (comp.Contains("WTi"))
            {
                return TargetType.WTi;
            }

            if (comp.Contains("Cu"))
            {
                if (comp.Contains("In"))
                {
                    return TargetType.CIGS;
                }
                else if(comp.Contains("Ga"))
                {
                    return TargetType.CuGaSe;
                }
                else
                {
                    return TargetType.Unknown;
                }
            }
            else
            {
                if (comp.Contains("Se"))
                {
                   return TargetType.InSe;
                }
                else
                {
                    return TargetType.InS;
                }
            }
        }
    }
}
