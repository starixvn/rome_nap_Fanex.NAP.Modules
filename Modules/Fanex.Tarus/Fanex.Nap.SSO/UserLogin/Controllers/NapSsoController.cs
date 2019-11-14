// --------------------------------------------------------------------------
// <copyright file="NapSsoController.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------

namespace Fanex.NAP.SSO
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Web.Mvc;
    using Core.Configuration;
    using Core.Context;
    using Core.Models.ParameterObject;
    using Fanex.Common;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web.Context;

    /// <summary>
    /// The Nap Single sign on controller.
    /// </summary>
    public class NapSsoController : Controller
    {
        private readonly ICurrentContext _currentContext;
        private readonly ISecurityService _securityService;

        public NapSsoController()
            : this(null, null)
        {
        }

        public NapSsoController(
            ICurrentContext currentContext,
            ISecurityService securityService)
        {
            this._currentContext = currentContext ?? new CurrentContext();
            this._securityService = securityService ?? new SecurityService();
        }

        [ValidateInput(false)]
        public ActionResult Index()
        {
            // check-in user
            int userId = Converter.ToInt(HttpContext.Session["AccId"], 0);
            if (userId != 0)
            {
                Uri uri = HttpContext.Request.Url;
                var onlineUserId = UserCheckin(NapSettings.Settings.SiteId, userId);
                var napHomeURL = string.Format(ConfigurationManager.AppSettings["NapHomeSSOUrl"], (uri.Host.Contains("oneops.net") ? "https" : uri.Scheme) + Uri.SchemeDelimiter + HttpContext.Request.Headers["Host"].ToString());
                return new RedirectResult(
                    string.Format(
                        napHomeURL + "?userId={0}&onuserId={1}&time={2}",
                        Cryptography.CfsEnCrypt(userId.ToString(CultureInfo.CurrentCulture)),
                        Cryptography.CfsEnCrypt(onlineUserId.ToString(CultureInfo.CurrentCulture)),
                        Cryptography.CfsEnCrypt(GetServerTime())));
            }
            else
            {
                return new RedirectResult("Index.aspx");
            }
        }

        private string GetServerTime()
            => _securityService.GetTime();

        private int UserCheckin(int siteId, int userId)
        {
            int onlineUserId = 0;

            try
            {
                onlineUserId = _securityService.Login(new LoginParameter
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
                });
            }
            catch (Exception)
            {
                if (onlineUserId == 0)
                {
                    throw;
                }
            }

            return onlineUserId;
        }
    }
}