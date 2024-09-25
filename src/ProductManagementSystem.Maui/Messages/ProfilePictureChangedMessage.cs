using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ProductManagementSystem.Maui.Messages;

public class ProfilePictureChangedMessage : ValueChangedMessage<string>
{
    public ProfilePictureChangedMessage(string value) : base(value)
    {
    }
}
