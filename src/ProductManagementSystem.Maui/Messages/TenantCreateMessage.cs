using CommunityToolkit.Mvvm.Messaging.Messages;
using Volo.Saas.Host.Dtos;

namespace ProductManagementSystem.Maui.Messages;

public class TenantCreateMessage : ValueChangedMessage<SaasTenantCreateDto>
{
    public TenantCreateMessage(SaasTenantCreateDto value) : base(value)
    {
        
    }
}