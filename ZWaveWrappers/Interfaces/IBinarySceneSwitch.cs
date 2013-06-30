namespace ZWaveWrappers.Interfaces
{
    public interface IBinarySceneSwitch : IZWaveDevice
    {
        byte Level { get; }

        void Ping();

        void PowerOn();

        void PowerOff();
    }
} 
