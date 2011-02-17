using System;
using System.Web;
using System.Web.Mvc;
using Lib.Web.Mvc.Properties;

namespace Lib.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {

        private string _salt;
        private AntiForgeryDataSerializer _serializer;

        public string Salt
        {
            get
            {
                return _salt ?? String.Empty;
            }
            set
            {
                _salt = value;
            }
        }

        internal AntiForgeryDataSerializer Serializer
        {
            get
            {
                if (_serializer == null)
                {
                    _serializer = new AntiForgeryDataSerializer();
                }
                return _serializer;
            }
            set
            {
                _serializer = value;
            }
        }

        private bool ValidateFormToken(AntiForgeryData token)
        {
            return (String.Equals(Salt, token.Salt, StringComparison.Ordinal));
        }

        private static HttpAntiForgeryException CreateValidationException()
        {
            return new HttpAntiForgeryException(Resources.AntiForgeryToken_ValidationFailed);
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            string fieldName = AntiForgeryData.GetAntiForgeryTokenName(null);
            string cookieName = AntiForgeryData.GetAntiForgeryTokenName(filterContext.HttpContext.Request.ApplicationPath);

            HttpCookie cookie = filterContext.HttpContext.Request.Cookies[cookieName];
            if (cookie == null || String.IsNullOrEmpty(cookie.Value))
            {
                // error: cookie token is missing
                throw CreateValidationException();
            }
            AntiForgeryData cookieToken = Serializer.Deserialize(cookie.Value);

            HttpVerbs verb;
            //Parse HttpVerb from Request.GetHttpMethodOverride()
            if (!Enum.TryParse<HttpVerbs>(filterContext.HttpContext.Request.GetHttpMethodOverride(), true, out verb))
                //If method couldn't be parsed, assume POST
                verb = HttpVerbs.Post;

            //Extract token based on HttpVerb
            string formValue = String.Empty;
            if (verb == HttpVerbs.Post || verb == HttpVerbs.Put)
                formValue = filterContext.HttpContext.Request.Form[fieldName];
            else
                formValue = filterContext.HttpContext.Request.Params[fieldName];

            if (String.IsNullOrEmpty(formValue))
            {
                // error: form token is missing
                throw CreateValidationException();
            }
            AntiForgeryData formToken = Serializer.Deserialize(formValue);

            if (!String.Equals(cookieToken.Value, formToken.Value, StringComparison.Ordinal))
            {
                // error: form token does not match cookie token
                throw CreateValidationException();
            }

            string currentUsername = AntiForgeryData.GetUsername(filterContext.HttpContext.User);
            if (!String.Equals(formToken.Username, currentUsername, StringComparison.OrdinalIgnoreCase))
            {
                // error: form token is not valid for this user
                // (don't care about cookie token)
                throw CreateValidationException();
            }

            if (!ValidateFormToken(formToken))
            {
                // error: custom validation failed
                throw CreateValidationException();
            }
        }
    }
}
