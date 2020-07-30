using Fanex.Data;
using System;
using System.Collections;
using System.Web;

namespace Sunplus.Accounting.DataAccess.IBCDLL.Accounting
{
    public static class CrmUpdateHeadlineInfo
    {
        public static void Acc_CRM_HeadlineUpd(string userName, int custId, int Case, int userId, int oldHeadline, int newHeadline)
        {
            try
            {
                using (ObjectDb objdb = new ObjectDb("Acc_CRM_HeadlineUpd", 600))
                {
                    var hash = new Hashtable();
                    hash.Add("username", userName);
                    hash.Add("custid", custId);
                    hash.Add("userid", userId);
                    hash.Add("case", Case);
                    hash.Add("old_headline", oldHeadline);
                    hash.Add("new_headline", newHeadline);
                    hash.Add("errcode", 0);
                    hash.Add("errmsg", "");
                    hash.Add("ip", HttpContext.Current.Request.UserHostAddress);
                    objdb.AssignParamValues(hash);
                    objdb.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Core.Extensions.LogExtensions.ExceptionHandler(ex);
            }
        }
    }
}