using CommunityToolkit.Mvvm.ComponentModel;

namespace ProductManagementSystem.Maui.ViewModels;
public partial class SelectionViewModel : ObservableObject
{
    [ObservableProperty]
    private string displayName = string.Empty;

    [ObservableProperty]
    private bool isSelected = false;

    [ObservableProperty]
    private object key = null!;
}
