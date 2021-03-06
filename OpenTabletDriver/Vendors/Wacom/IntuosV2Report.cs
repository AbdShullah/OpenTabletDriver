using System.Numerics;
using OpenTabletDriver.Plugin.Tablet;

namespace OpenTabletDriver.Vendors.Wacom
{
    public struct IntuosV2TabletReport : ITabletReport
    {
        public IntuosV2TabletReport(byte[] report)
        {
            Raw = report;

            ReportID = (uint)report[9] >> 2;
            var x = (report[3] | report[2] << 8) << 1 | ((report[9] >> 1) & 1);
            var y = (report[5] | report[4] << 8) << 1 | (report[9] & 1);
            Position = new Vector2(x, y);
            Pressure = (uint)((report[6] << 3) | ((report[7] & 0xC0) >> 5) | (report[1] & 1));

            PenButtons = new bool[]
            {
                (report[1] & (1 << 1)) != 0,
                (report[1] & (1 << 2)) != 0
            };
        }

        public byte[] Raw { private set; get; }
        public uint ReportID { private set; get; }
        public Vector2 Position { private set; get; }
        public uint Pressure { private set; get; }
        public bool[] PenButtons { private set; get; }
    }
}