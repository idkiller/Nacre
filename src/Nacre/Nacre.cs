using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nacre
{
    public struct Nacre
    {
        public IEnumerable<IBackground> Background { get; set; }
        public Border Border { get; set; }
        public IEnumerable<Shadow> Shadow { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Background.Aggregate(new StringBuilder(), (c, n) => c.Append(c.Length == 0 ? "" : "\n").Append(n.ToString())));
            if (sb.Length != 0) sb.Append("\n");
            sb.Append(Border.ToString());
            if (sb.Length != 0) sb.Append("\n");
            sb.Append(Shadow.Aggregate(new StringBuilder(), (c, n) => c.Append(c.Length == 0 ? "" : "\n").Append(n.ToString())));
            return sb.ToString();
        }
    }
}
