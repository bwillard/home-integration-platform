namespace ZWaveWrappers.Interfaces
{
    public interface IMultilevelSceneSwitch : IBinarySceneSwitch
    {
        byte Level { get; set; }
    }
} 
