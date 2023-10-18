#region ENBREA - Copyright (c) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA 
 *    
 *    Copyright (c) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System.Text.RegularExpressions;

namespace Enbrea.MessageBroker
{
    /// <summary>
    /// Routing key helpers
    /// </summary>
    public static class RoutingKeys
    {
        static public bool Match(string pattern, string key)
        {
            if (key == pattern)
            {
                return true;
            }

            var normalizedPattern = pattern.
                Replace(".", "\\.").
                Replace("*", "\\w+").
                Replace("#", "((\\w+)|(\\w+\\.\\w+))+");

            return Regex.IsMatch(key, $"^{normalizedPattern}$");
        }
    }
}
