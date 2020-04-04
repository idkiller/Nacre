using System;
using System.Collections.Generic;
using System.Text;

namespace Nacre
{
    public struct Nacre
    {
        public IEnumerable<IBackground> Background { get; set; }
        public Border Border { get; set; }
        public IEnumerable<Shadow> Shadow { get; set; }
    }
}
