namespace KeepAwakeTray6.Interfaces
{
    public interface IActivityMonitor
    {
        bool IsActive { get; }

        void Activate();
        void Deactivate();
    }
}
