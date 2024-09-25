using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ProductManagementSystem.Maui.Messages;

public class LanguageChangedMessage : ValueChangedMessage<string?>
{
    public LanguageChangedMessage(string? value) : base(value)
    {
    }
}
