﻿using System;
using CoreWCFRestContrib.Reflection;
using CoreWCFRestContrib.ServiceModel.Description;
using CoreWCFRestContrib.ServiceModel.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Configuration.WebAuthentication
{
    public class BehaviorElement : BehaviorExtensionElement
    {
        private const string AuthHandlerTypeElement = "authenticationHandlerType";
        private const string UsernamePasswordValidatorTypeElement = "usernamePasswordValidatorType";
        private const string RequireSecureTransportElement = "requireSecureTransport";
        private const string SourceElement = "source";

        public override Type BehaviorType
        {
            get { return typeof(WebAuthenticationConfigurationBehavior); }
        }

        protected override object CreateBehavior()
        {
            Type operationAuthenticationHandler = null;
            if (!string.IsNullOrEmpty(OperationAuthenticationHandlerTypeName))
                try
                {
                    operationAuthenticationHandler = OperationAuthenticationHandlerTypeName.GetType<IWebAuthenticationHandler>();
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Invalid authenticationHandlerType specified in webAuthentication behavior element. {0}", e));
                }

            Type usernamePasswordValidator = null;
            if (!string.IsNullOrEmpty(UsernamePasswordValidatorTypeName))
                try
                {
                    usernamePasswordValidator = UsernamePasswordValidatorTypeName.GetType<UserNamePasswordValidator>();
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Invalid usernamePasswordValidatorType specified in webAuthentication behavior element. {0}", e));
                }
         
            return new WebAuthenticationConfigurationBehavior(
                operationAuthenticationHandler,
                usernamePasswordValidator,
                RequireSecureTransport,
                Source);
        }

        [ConfigurationProperty(AuthHandlerTypeElement, IsRequired = false, DefaultValue = null)]
        public string OperationAuthenticationHandlerTypeName
        {
            get
            { return (string)base[AuthHandlerTypeElement]; }
            set
            { base[AuthHandlerTypeElement] = value; }
        }

        [ConfigurationProperty(UsernamePasswordValidatorTypeElement, IsRequired = false, DefaultValue = null)]
        public string UsernamePasswordValidatorTypeName
        {
            get
            { return (string)base[UsernamePasswordValidatorTypeElement]; }
            set
            { base[UsernamePasswordValidatorTypeElement] = value; }
        }

        [ConfigurationProperty(RequireSecureTransportElement, IsRequired = false, DefaultValue = true)]
        public bool RequireSecureTransport
        {
            get
            { return (bool)base[RequireSecureTransportElement]; }
            set
            { base[RequireSecureTransportElement] = value; }
        }

        [ConfigurationProperty(SourceElement, IsRequired = true)]
        public string Source
        {
            get
            { return (string)base[SourceElement]; }
            set
            { base[SourceElement] = value; }
        }
    }
}
