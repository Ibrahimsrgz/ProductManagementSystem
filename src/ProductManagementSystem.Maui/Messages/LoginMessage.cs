using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ProductManagementSystem.Maui.Messages;
public class LoginMessage : ValueChangedMessage<bool?>
{
    public LoginMessage(bool? value = null) : base(value)
    {
    }
}
