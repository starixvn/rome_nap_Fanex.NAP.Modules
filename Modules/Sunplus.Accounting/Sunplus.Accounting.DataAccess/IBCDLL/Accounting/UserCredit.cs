using System;
using System.Collections;
using System.Data;
using System.Web;
using DNAide.Core;
using DNAide.Data;
using Fanex.Data;

namespace Sunplus.Accounting.DataAccess.IBCDLL.Accounting
{
    public sealed class Credit
    {
        public static void UpdateMemberCredit(int _accId, int _custId, decimal _newCredit, decimal totalAmt, decimal accumCredit)
        {
            var param = new Hashtable();
            param.Add("userid", _accId);
            param.Add("custid", _custId);
            param.Add("newcredit", _newCredit);
            param.Add("ip", HttpContext.Current.Request.UserHostAddress);
            param.Add("errcode", 0);
            param.Add("errmsg", string.Empty);
            param.Add("totalamt", totalAmt);
            param.Add("accumcreditallcashmember", accumCredit);

            CustomerUpdateCredit(param);
        }

        public static void UpdateAgentCredit(int _accId, int _custId, decimal _newCredit, decimal accumCredit)
        {
            var param = new Hashtable();
            param.Add("userid", _accId);
            param.Add("custid", _custId);
            param.Add("newcredit", _newCredit);
            param.Add("ip", HttpContext.Current.Request.UserHostAddress);
            param.Add("errcode", 0);
            param.Add("errmsg", string.Empty);
            param.Add("accumcreditallcashmember", accumCredit);

            CustomerUpdateCredit(param);
        }

        public static void UpdateSMACredit(int _accId, int _custId, decimal _newCredit)
        {
            var param = new Hashtable();
            param.Add("userid", _accId);
            param.Add("custid", _custId);
            param.Add("newcredit", _newCredit);
            param.Add("ip", HttpContext.Current.Request.UserHostAddress);
            param.Add("errcode", 0);
            param.Add("errmsg", string.Empty);

            CustomerUpdateCredit(param);
        }

        private static void CustomerUpdateCredit(Hashtable param)
        {
            try
            {
                using (ObjectDb objDb = new ObjectDb("Acc_CustomerUpdCredit", 600))
                {
                    objDb.AssignParamValues(param);

                    objDb.ExecuteNonQuery();

                    if (CastAide.AsInt32(objDb.GetParameterValue("errcode")) > 0)
                        throw new AccException(CastAide.AsString(objDb.GetParameterValue("errmsg")));
                }
            }
            catch (AccException accException)
            {
                throw accException;
            }
            catch (Exception ex)
            {
                Sunplus.Accounting.Core.Extensions.LogExtensions.ExceptionHandler(ex);
                throw ex;
            }
        }
    }

    public sealed class UserCredit
    {
        public static DataTable SelectAgentCredit(int _uplineId, int serviceType)
        {
            DataTable dt = new DataTable();
            try
            {
                using (ObjectDb objDB = new ObjectDb("Acc_CustomerSelUsedCredit_Ag", 600))
                {
                    var param = new Hashtable();
                    param.Add("uplineid", _uplineId);
                    param.Add("servicetype", serviceType);

                    objDB.AssignParamValues(param);

                    var ds = objDB.ExecuteDataSet();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }

                }
            }
            catch (Exception ex)
            {
                Sunplus.Accounting.Core.Extensions.LogExtensions.ExceptionHandler(ex);
                throw ex;
            }
            return dt;
        }

        /// <summary>
        /// Select Master Credit --
        /// Store Procedure: Acc_CustomerSelUsedCredit_MAg --
        /// @viet hoang
        /// </summary>
        /// <param name="_masterId"></param>
        /// <returns></returns>
        public static DataTable SelectMasterCredit(int _masterId)
        {
            DataTable dt = new DataTable();
            try
            {
               
                using (ObjectDb objDB = new ObjectDb("Acc_CustomerSelUsedCredit_MAg", 600))
                {
                    var param = new Hashtable();
                    param.Add("masterid", _masterId);

                    objDB.AssignParamValues(param);

                    var ds = objDB.ExecuteDataSet();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Sunplus.Accounting.Core.Extensions.LogExtensions.ExceptionHandler(ex);
                throw ex;
            }
            return dt;
        }

        /// <summary>
        /// Select Super Credit Details --
        /// Store Procedure: Acc_CustomerSelUsedCredit_SAg --
        /// @viet hoang
        /// </summary>
        /// <param name="_superId"></param>
        /// <returns></returns>
        public static DataTable SelectSuperCredit(int _superId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (ObjectDb objDB = new ObjectDb("Acc_CustomerSelUsedCredit_SAg", 600))
                {
                    var param = new Hashtable();
                    param.Add("superid", _superId);

                    objDB.AssignParamValues(param);
                    var ds = objDB.ExecuteDataSet();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Sunplus.Accounting.Core.Extensions.LogExtensions.ExceptionHandler(ex);
                throw ex;
            }
            return dt;
        }

        /// <summary>
        /// Agent Reset Member Credit
        /// SP: Acc_CustomerUpdResetMemberCredit
        /// @viet hoang
        /// </summary>
        /// <param name="_actorId"></param>
        /// <param name="_uplineId"></param>
        /// <param name="closedOnly"></param>
        /// <returns></returns>
        public static void ResetMemberCredit(int _actorId, int _uplineId, bool closedOnly)
        {
            try
            {
                using (ObjectDb objDb = new ObjectDb("Acc_CustomerUpdResetMemberCredit", 600))
                {
                    var param = new Hashtable();
                    param.Add("userid", _actorId);
                    param.Add("uplineid", _uplineId);
                    param.Add("closedonly", closedOnly);
                    param.Add("ip", HttpContext.Current.Request.UserHostAddress);
                    param.Add("errcode", 0);
                    param.Add("errmsg", string.Empty);

                    objDb.AssignParamValues(param);

                    objDb.ExecuteNonQuery();
                    if (CastAide.AsInt32(objDb.GetParameterValue("errcode")) > 0)
                        throw new AccException(string.Format("{0}", objDb.GetParameterValue("errmsg")));
                }
            }
            catch (AccException accException)
            {
                throw accException;
            }
            catch (Exception ex)
            {
                Sunplus.Accounting.Core.Extensions.LogExtensions.ExceptionHandler(ex);
                throw ex;
            }
        }
    }
}
