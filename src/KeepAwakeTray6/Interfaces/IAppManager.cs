using Microsoft.Toolkit.Mvvm.Input;

namespace KeepAwakeTray6.Interfaces
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
