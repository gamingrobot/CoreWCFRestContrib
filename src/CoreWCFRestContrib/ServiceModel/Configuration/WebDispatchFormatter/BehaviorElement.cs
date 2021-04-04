using System;
using System.Collections.Generic;
using CoreWCFRestContrib.Reflection;
using CoreWCFRestContrib.ServiceModel.Description;
using CoreWCFRestContrib.ServiceModel.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Configuration.WebDispatchFormatter
{
    //public class BehaviorElement : BehaviorExtensionElement
    //{
    //    private const string FormattersElement = "formatters";

    //    public override Type BehaviorType
    //    {
    //        get { return typeof(WebDispatchFormatterConfigurationBehavior); }
    //    }

    //    protected override object CreateBehavior()
    //    {
    //        var formatters = new Dictionary<string, Type>();

    //        foreach (FormatterElement element in Formatters)
    //            foreach (var mimeType in element.MimeTypes)
    //            {
    //                Type formatter;
    //                try
    //                {
    //                    formatter = element.Type.GetType<IWebFormatter>();
    //                }
    //                catch (Exception e)
    //                {
    //                    throw new Exception(string.Format("Invalid type specified in formatter behavior element. {0}", e));
    //                }

    //                formatters.Add(
    //                    mimeType,
    //                    formatter);
    //            }

    //        return new WebDispatchFormatterConfigurationBehavior(
    //            formatters, Formatters.DefaultMimeType);
    //    }

    //    [ConfigurationProperty(FormattersElement, IsRequired = true)]
    //    public FormattersElement Formatters
    //    {
    //        get
    //        { return (FormattersElement)base[FormattersElement]; }
    //        set
    //        { base[FormattersElement] = value; }
    //    }
    //}
}
