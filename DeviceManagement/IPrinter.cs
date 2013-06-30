namespace HomeAutomation.DeviceManagement
{
    public interface IPrinter : IDevice
    {
        int BlackInkLevel { get; }
        int YellowInkLevel { get; }
        int CyanInkLevel { get; }
        int MagentaInkLevel { get; }

        int PagesPrinted { get; }
    }
}
