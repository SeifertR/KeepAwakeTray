using GalaSoft.MvvmLight.CommandWpf;

namespace KeepAwakeTray.Interfaces
{
    public interface IAppManager
    {
        RelayCommand ActivateCommand { get; }
        RelayCommand DeactivateCommand { get; }
        RelayCommand ShowSettingsCommand { get; }
        RelayCommand ExitCommand { get; }
    }
}
