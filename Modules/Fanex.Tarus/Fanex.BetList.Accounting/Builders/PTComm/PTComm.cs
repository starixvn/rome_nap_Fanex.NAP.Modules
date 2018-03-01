namespace Fanex.BetList.Accounting.Builders.PTComm
{
    using System.Collections.Generic;
    using BetList.Core.Entities;
    using BetList.Core.Utils;
    using Constants;

    public class PTComm
    {
        public PTComm(ITicket ticket, int roleId)
        {
            Ticket = ticket ?? new Ticket();
            RoleId = roleId;
        }

        public virtual bool VisiblePositionTaking => !BetStatusIsRunningOrWaiting();

        public virtual bool VisibleCommision => !BetStatusIsRunningOrWaiting();

        public virtual bool VisibilePositionTakingPercent => true;

        public virtual bool VisibileCommisionPercent => true;

        public virtual string PositionTaking => ConvertByBetType.PtValue(PositonTakingByRoleId[RoleId]);

        public virtual string PositionTakingPercent => ConvertByBetType.Pt(PTSettingByRoleId[RoleId]) + "%";

        public virtual string Commission => ConvertByBetType.Comm(CommByRoleId[RoleId]);

        public virtual string CommissionPercent => GetCommisionPercentText(ConvertByBetType.Comm_Percentage(CommSettingByRoleId[RoleId]));

        protected ITicket Ticket { get; }

        protected int RoleId { get; }

        protected virtual IDictionary<int, object> PositonTakingByRoleId
            => new Dictionary<int, object>
            {
                { RoleIdConstants.Super, Ticket.SuperWinlost },
                { RoleIdConstants.Master, Ticket.MasterWinlost },
                { RoleIdConstants.Agent, Ticket.AgentWinlost }
            };

        protected virtual IDictionary<int, object> PTSettingByRoleId
            => new Dictionary<int, object>
            {
                { RoleIdConstants.Super, Ticket.SuperPositionTaking },
                { RoleIdConstants.Master, Ticket.MasterPositionTaking },
                { RoleIdConstants.Agent, Ticket.AgentPositionTaking }
            };

        protected virtual IDictionary<int, object> CommByRoleId
            => new Dictionary<int, object> {
                { RoleIdConstants.Super, Ticket.SuperComm },
                { RoleIdConstants.Master, Ticket.AgentComm },
                { RoleIdConstants.Agent, Ticket.Comm }
            };

        protected virtual IDictionary<int, object> CommSettingByRoleId
            => new Dictionary<int, object> {
                { RoleIdConstants.Super, Ticket.SuperDiscount },
                { RoleIdConstants.Master, Ticket.MasterDiscount },
                { RoleIdConstants.Agent, Ticket.AgentDiscount }
            };

        private string GetCommisionPercentText(string commPercent)
            => Ticket.CommStatus == "1" ? $"2x {commPercent}%" : $"{commPercent}%";

        private bool BetStatusIsRunningOrWaiting()
        {
            var status = Ticket.Status.ToLowerInvariant();

            return status.Equals(BetStatus.Running) || status.Equals(BetStatus.Waiting);
        }
    }
}