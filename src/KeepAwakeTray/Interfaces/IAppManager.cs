using GalaSoft.MvvmLight.CommandWpf;

namespace KeepAwakeTray.Interfaces
{
    public interface IAppManager
    {
        bool IsActive { get; }

        RelayCommand ActivateCommand { get; }
        RelayCommand DeactivateCommand { get; }
        RelayCommand ShowSettingsCommand { get; }
        RelayCommand ExitCommand { get; }
    }
}
