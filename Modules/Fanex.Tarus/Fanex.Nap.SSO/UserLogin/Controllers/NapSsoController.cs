// --------------------------------------------------------------------------
// <copyright file="NapSsoController.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------

namespace Fanex.NAP.SSO
{
    using Fanex.Common;
    using Fanex.NAP.Core.Criteria.User;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web.Context;
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Web.Mvc;

    /// <summary>
    /// The Nap Single sign on controller.
    /// </summary>
    public class NapSsoController : Controller
    {
        private ICurrentContext _currentContext;
        private IUserService _userService;

        public NapSsoController()
            : this(null)
        {
        }

        public NapSsoController(IUserService userService)
        {
            this._userService = userService ?? new UserService();
            this._currentContext = new CurrentContext();
        }

        [ValidateInput(false)]
        public ActionResult Index()
        {
            // check-in user
            int userId = Converter.ToInt(HttpContext.Session["AccId"], 0);
            if (userId != 0)
            {
                Uri uri = HttpContext.Request.Url;
                var checkInUser = UserCheckin(Converter.ToInt(ConfigurationManager.AppSettings["NAPHomeSiteId"], 0), userId);
                var napHomeURL = string.Format(ConfigurationManager.AppSettings["NapHomeSSOUrl"], (uri.Host.Contains("oneops.net") ? "https" : uri.Scheme) + Uri.SchemeDelimiter + HttpContext.Request.Headers["Host"].ToString());
                return new RedirectResult(
                    string.Format(
                        napHomeURL + "?userId={0}&onuserId={1}&time={2}",
                        Cryptography.CfsEnCrypt(userId.ToString(CultureInfo.CurrentCulture)),
                        Cryptography.CfsEnCrypt(checkInUser.OnUserId.ToString(CultureInfo.CurrentCulture)),
                        Cryptography.CfsEnCrypt(GetServerTime())));
            }
            else
            {
                return new RedirectResult("Index.aspx");
            }
        }

        private string GetServerTime()
        {
            string time = this._userService.UserSSOGetTime();
            return time;
        }

        private UserCheckInUserCriteria UserCheckin(int siteId, int userId)
        {
            var criteria = new UserCheckInUserCriteria
            {
                UserId = userId,
                SiteId = siteId,
                NetSessionId = Session.SessionID,
                Ip = _currentContext.IP,
                LoginDate = DateTime.Now,
                BrowserName = _currentContext.BrowserName,
                CountryCode = _currentContext.CountryCode,
                Domain = _currentContext.DomainName,
                Platform = _currentContext.Platform
            };

            this._userService.UserCheckIn(ref criteria);
            return criteria;
        }
    }
}