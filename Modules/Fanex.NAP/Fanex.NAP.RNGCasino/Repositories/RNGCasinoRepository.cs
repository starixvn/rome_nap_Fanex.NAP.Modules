namespace Fanex.Module.RNGCasino.Repositories
{
    using System;
    using System.Collections.Generic;
    using BetList.Core.Entities;
    using Common;
    using Criteria;
    using Data;

    [Obsolete]
    public class RNGCasinoRepository : IRNGCasinoRepository
    {
        public IEnumerable<Ticket> BetDetails(GetRNGCasinoBetDetailsCriteria criteria)
        {
            var result = new List<Ticket>();

            using (ObjectDb objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(new Ticket
                        {
                            TransId = Converter.To<long>(reader["transid"]),
                            SportTypeId = Converter.ToInt(reader["SportType"]),
                            BetTypeId = Converter.ToInt(reader["BetType"]),
                            TransDate = Converter.ToDateTime(reader["transdate"]),
                            WinlostDate = Converter.ToDateTime(reader["winlostdate"]),

                            Status = Converter.ToString2(reader["status"]),
                            StatusId = Converter.ToInt(reader["statusid"]),

                            Odds = Converter.ToDecimal(reader["odds"]),
                            Currency = Converter.ToString2(reader["currency"]),
                            Stake = Converter.ToDecimal(reader["stake"]),
                            ActualStake = Converter.ToDecimal(reader["actual_stake"]),
                            Winlost = Converter.ToDecimal(reader["WinLost"]),
                            IP = Converter.ToString2(reader["ip"]),
                            TransDesc = Converter.ToString2(reader["transdesc"]),

                            PlayerComm = Converter.ToDecimal(reader["commission"]),
                            AgentWinlost = Converter.ToDecimal(reader["awinlost"]),
                            AgentPositionTaking = Converter.ToDecimal(reader["apositiontaking"]),
                            Comm = Converter.ToDecimal(reader["comm"]),
                            AgentDiscount = Converter.ToDecimal(reader["discount"]),

                            MasterWinlost = Converter.ToDecimal(reader["mwinlost"]),
                            MasterPositionTaking = Converter.ToDecimal(reader["mpositiontaking"]),
                            AgentComm = Converter.ToDecimal(reader["acomm"]),
                            MasterDiscount = Converter.ToDecimal(reader["adiscount"]),

                            SuperWinlost = Converter.ToDecimal(reader["swinlost"]),
                            SuperPositionTaking = Converter.ToDecimal(reader["spositiontaking"]),
                            SuperComm = Converter.ToDecimal(reader["scomm"]),
                            SuperDiscount = Converter.ToDecimal(reader["sdiscount"]),
                            BetCheck = Converter.ToString2(reader["BetCheck"]),

                            MatchCode = Converter.ToString2(reader["matchcode"]),
                            OddsSpread = Converter.ToDecimal(reader["OddsSpread"]),
                            BetdaqId = Converter.To<long>(reader["BetdaqId"]),
                            ExtraInfo = Converter.ToString2(reader["ExtraInfo"]),
                            EventDate = Converter.ToDateTime(reader["eventdate"])
                        });
                    }
                }

                criteria.TotalRows = Converter.ToInt(objDb.GetOuputParams()["totalrows"]);
            }

            return result;
        }

        public IEnumerable<Ticket> JackpotDetails(GetJackpotDetailsCriteria criteria)
        {
            var result = new List<Ticket>();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());

                while (reader.Read())
                {
                    result.Add(new Ticket
                    {
                        TransId = Converter.To<long>(reader["TransId"]),
                        RefNo = Converter.ToString2(reader["RefNo"]),
                        TransDate = Converter.ToDateTime(reader["TransDate"]),
                        WinlostDate = Converter.ToDateTime(reader["WinLostDate"]),
                        SportTypeId = Converter.ToInt(reader["SportType"]),
                        BetTypeId = Converter.ToInt(reader["BetType"]),
                        Odds = Converter.ToDecimal(reader["Odds"]),

                        Status = Converter.ToString2(reader["Status"]),
                        StatusId = Converter.ToInt(reader["StatusId"]),

                        Stake = Converter.ToDecimal(reader["Stake"]),
                        ActualStake = Converter.ToDecimal(reader["Actual_Stake"]),

                        Winlost = Converter.ToDecimal(reader["WinLost"]),
                        AgentWinlost = Converter.ToDecimal(reader["AWinLost"]),
                        MasterWinlost = Converter.ToDecimal(reader["MWinLost"]),
                        SuperWinlost = Converter.ToDecimal(reader["SWinLost"]),

                        PlayerComm = Converter.ToDecimal(reader["Commission"]),
                        Comm = Converter.ToDecimal(reader["Comm"]),
                        AgentComm = Converter.ToDecimal(reader["AComm"]),
                        SuperComm = Converter.ToDecimal(reader["SComm"]),

                        AgentPositionTaking = Converter.ToDecimal(reader["APositionTaking"]),
                        MasterPositionTaking = Converter.ToDecimal(reader["MPositionTaking"]),
                        SuperPositionTaking = Converter.ToDecimal(reader["SPositionTaking"]),

                        AgentDiscount = Converter.ToDecimal(reader["Discount"]),
                        MasterDiscount = Converter.ToDecimal(reader["ADiscount"]),
                        SuperDiscount = Converter.ToDecimal(reader["SDiscount"]),

                        IP = Converter.ToString2(reader["Ip"]),
                        BetCheck = Converter.ToString2(reader["BetCheck"]),
                        TransDesc = Converter.ToString2(reader["TransDesc"]),
                        BetdaqId = Converter.To<long>(reader["BetdaqId"])
                    });
                }
            }

            return result;
        }
    }
}