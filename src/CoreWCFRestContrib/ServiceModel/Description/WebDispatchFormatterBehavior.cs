using System;
using System.Collections.Generic;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.ServiceModel.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class WebDispatchFormatterBehavior : IOperationBehavior
    {
        private readonly WebDispatchFormatter.FormatterDirection _direction;

        public WebDispatchFormatterBehavior(WebDispatchFormatter.FormatterDirection direction)
        {
            _direction = direction;
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            var behavior =
                operationDescription.DeclaringContract.ContractBehaviors[typeof(WebDispatchFormatterConfigurationBehavior)];

            if (behavior == null)
                behavior = dispatchOperation.Parent.EndpointDispatcher.ChannelDispatcher.Host.Description.Behaviors.Find
                        <WebDispatchFormatterConfigurationBehavior>();

            if (behavior == null)
            {
                var configurationAttribute = 
                    operationDescription.DeclaringContract.GetAttribute<WebDispatchFormatterConfigurationAttribute>();

                if (configurationAttribute == null)
                    configurationAttribute = dispatchOperation.Parent.EndpointDispatcher.ChannelDispatcher.Host.Description.
                        GetAttribute<WebDispatchFormatterConfigurationAttribute>();

                string defaultMimeType = null;

                if (configurationAttribute != null)
                    defaultMimeType = configurationAttribute.DefaultMimeType;

                var mimeTypeAttributes = 
                    operationDescription.DeclaringContract.GetAttributes<WebDispatchFormatterMimeTypeAttribute>();

                if (mimeTypeAttributes == null || mimeTypeAttributes.Count == 0)
                    mimeTypeAttributes = dispatchOperation.Parent.EndpointDispatcher.ChannelDispatcher.Host.Description.
                        GetAttributes<WebDispatchFormatterMimeTypeAttribute>();

                var formatters = new Dictionary<string, Type>();

                if (mimeTypeAttributes != null && mimeTypeAttributes.Count > 0)
                {

                    foreach (var mimeTypeAttribute in mimeTypeAttributes)
                        foreach (var mimeType in mimeTypeAttribute.MimeTypes)
                        {
                            if (defaultMimeType == null) defaultMimeType = mimeType;
                            formatters.Add(mimeType, mimeTypeAttribute.Type);
                        }
                }

                if (formatters.Count > 0)
                    behavior = new WebDispatchFormatterConfigurationBehavior(
                        formatters, defaultMimeType);
            }

            if (behavior == null)
                throw new InvalidOperationException(
                    "WebDispatchFormatterConfigurationBehavior or WebDispatchFormatterMimeTypeAttribute's not applied to contract or service. This behavior or attributes are required to configure web dispatch formatting.");

            dispatchOperation.Formatter = 
                new WebDispatchFormatter(
                    behavior.FormatterFactory,
                    operationDescription,
                    _direction != WebDispatchFormatter.FormatterDirection.Both ? dispatchOperation.Formatter : null,
                    _direction);
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation) { }
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters) { }
        public void Validate(OperationDescription operationDescription) { }
    }
}
