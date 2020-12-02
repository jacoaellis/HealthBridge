using Newtonsoft.Json.Linq;
using System;

namespace HealthBridgeClinical.Common.Extensions
{
    public static class JTokenExtensions
    {
        public static void Visit(this JToken node, Action<JToken> action)
        {
            if (node == null || action == null)
            {
                return;
            }

            action(node);

            foreach (var child in node.Children())
            {
                Visit(child, action);
            }
        }
    }
}
