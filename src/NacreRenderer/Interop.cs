using System;
using System.Runtime.InteropServices;

internal static class Interop
{
    const string Evas = "libevas.so.1";
    [DllImport(Evas)]
    internal static extern IntPtr evas_object_clip_get(IntPtr obj);

    [DllImport(Evas)]
    internal static extern void evas_object_clip_unset(IntPtr obj);
}