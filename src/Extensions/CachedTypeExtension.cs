﻿using Soenneker.Reflection.Cache.Types;

namespace Soenneker.Reflection.Cache.Extensions;

public static class CachedTypeExtension
{
    public static bool IsDerivedFromType(this CachedType sourceCachedType, params CachedType[] targetCachedTypes)
    {
        CachedType? tempCachedType = sourceCachedType;

        while (tempCachedType != null)
        {
            foreach (CachedType targetCachedType in targetCachedTypes)
            {
                // Check if it's non-generic
                if (targetCachedType.IsAssignableFrom(sourceCachedType))
                    return true;

                // Check if it's generic type (collection<T>)
                if (sourceCachedType.IsGenericType && sourceCachedType.GetCachedGenericTypeDefinition() == targetCachedType)
                    return true;
            }

            tempCachedType = sourceCachedType.CachedBaseType;

            if (tempCachedType.Type == typeof(object))
                return false;
        }

        return false;
    }
}