using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthBridgeClinical.Common.Utils
{
    public class TemplatedStringGenerator
    {
        public const char DelimiterStartChar = '{';
        public const char DelimiterStopChar = '}';

        public virtual async Task<string> GenerateStringAsync(string template, Dictionary<string, object> values)
        {
            Validate.IfNullOrWhitespace(values).Throw(() => new ArgumentNullException(nameof(values)));
            Validate.IfNullOrWhitespace(template).Throw(() => new ArgumentNullException(nameof(template)));

            var renderer = new Template(template, DelimiterStartChar, DelimiterStopChar);

            values.Keys.ToList().ForEach(key =>
            {
                renderer.Add(key, values[key]);
            });

            var result = renderer.Render();

            return await Task.FromResult(result);
        }
    }

}
