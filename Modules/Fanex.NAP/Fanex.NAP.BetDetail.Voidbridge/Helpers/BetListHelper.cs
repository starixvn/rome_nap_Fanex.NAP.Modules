namespace Fanex.Voidbridge.BetDetail.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using BetList.Core.Builder;
    using BetList.Core.Entities;
    using Resources;

    public class BetListHelper
    {
        public static readonly Regex IsNumericRegex = new Regex("^(" +
            /*Hex*/ @"0x[0-9a-f]+" + "|" +
            /*Bin*/ @"0b[01]+" + "|" +
            /*Oct*/ @"0[0-7]+" + "|" +
            /*Dec*/ @"((?!0)|[-+])(\d*\.)?\d+(e\d+)?" +
                                                    ")$");

        public static bool IsNumeric(string value)
        {
            return IsNumericRegex.IsMatch(value);
        }

        public static string GetCommonHeaderRows(int siteId)
        {
            var result = new StringBuilder();
            result.Append("<tr>");
            result.Append("<th><div class=\"width30 height34 table-cell\">" + BetListResources.No + "</div></th>");
            result.Append("<th><div class=\"width120 height34 table-cell\">" + BetListResources.Trans_Time + "</div></th>");
            result.Append("<th><div class=\"column-choice height34 table-cell\">" + BetListResources.Choice + "</div></th>");
            result.Append("<th><div class=\"width60 height34 table-cell\">" + BetListResources.Odds + "</div></th>");
            result.Append("<th><div class=\"width50 height34 table-cell\">" + BetListResources.Stake + "</div></th>");
            result.Append("<th><div class=\"column-winloss height34 table-cell\">" + BetListResources.Win_Loss + "</div></th>");
            result.Append("<th><div class=\"width100 height34 table-cell\">" + BetListResources.Status + "</div></th>");
            result.Append(
                "<th><div class=\"width60 height34 table-cell\">" + BetListResources.Super + "<br />" + BetListResources.PT + "/" + BetListResources.Comm + "</div></th>");
            result.Append(
                "<th><div class=\"width60 height34 table-cell\">" + BetListResources.Master + "<br />" + BetListResources.PT + "/" + BetListResources.Comm + "</div></th>");
            result.Append(
                "<th><div class=\"width60 height34 table-cell\">" + BetListResources.Agent + "<br />" + BetListResources.PT + "/" + BetListResources.Comm + "</div></th>");
            result.Append("</tr>");
            return result.ToString();
        }

        public string IsNegative(decimal input)
        {
            return input < 0 ? "negative" : "positive";
        }

        public string PercentFormat(decimal input)
        {
            var isInputInterger = (input * 100) % 1 == 0;
            return isInputInterger ?
                Math.Round(input * 100, 0).ToString() + BetListResources.Symbol_Percent :
                Math.Round(input * 100, 2).ToString() + BetListResources.Symbol_Percent;
        }

        public string BuildStatus(BetListHTMLBuilder builder, ITicketHelper ticketHelper, ITicket ticket, List<ITicketData> ticketData)
        {
            var statusBuilder = builder.CreateStatusBuilder(ticket.BetTypeId);
            var statusBlock = statusBuilder.Render(ticket, ticketHelper, ticketData, true);
            statusBlock.ShowIP = null;

            return statusBlock.ToString();
        }

        public bool IsShowOddsWithChoice(int betTypeId)
        {
            return betTypeId == 9 || betTypeId == 29;
        }
    }
}
