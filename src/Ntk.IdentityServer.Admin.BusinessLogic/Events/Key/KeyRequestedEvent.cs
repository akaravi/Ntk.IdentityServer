using Skoruba.AuditLogging.Events;
using Ntk.IdentityServer.Admin.BusinessLogic.Dtos.Grant;
using Ntk.IdentityServer.Admin.BusinessLogic.Dtos.Key;

namespace Ntk.IdentityServer.Admin.BusinessLogic.Events.Key
{
    public class KeyRequestedEvent : AuditEvent
    {
        public KeyDto Key { get; set; }

        public KeyRequestedEvent(KeyDto key)
        {
            Key = key;
        }
    }
}