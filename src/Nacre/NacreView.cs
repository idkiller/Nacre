using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Nacre
{
    public class NacreView : ContentView
    {
        public IEnumerable<IBackground> Background { get; set; }
        public Border Border { get; set; }
        public IEnumerable<Shadow> Shadow { get; set; }
    }
}
