﻿namespace CoreWCFRestContrib.ServiceModel.Configuration.WebDispatchFormatter
{
    //public class FormatterElement : ConfigurationElement
    //{
    //    private const string MimeTypesElement = "mimeTypes";
    //    private const string TypeElement = "type";

    //    private static readonly ConfigurationPropertyCollection _properties;
    //    private static readonly ConfigurationProperty _type;
    //    private static readonly ConfigurationProperty _mimeTypes;

    //    static FormatterElement()
    //    {
    //        _type = new ConfigurationProperty(TypeElement, typeof(string), null, null, new StringValidator(1), ConfigurationPropertyOptions.IsRequired);
    //        _mimeTypes = new ConfigurationProperty(MimeTypesElement, typeof(CommaDelimitedStringCollection), null, new CommaDelimitedStringCollectionConverter(), null, ConfigurationPropertyOptions.None);
    //        _properties = new ConfigurationPropertyCollection {_type, _mimeTypes};
    //    }

    //    public CommaDelimitedStringCollection MimeTypes
    //    {
    //        get { return (CommaDelimitedStringCollection)this[_mimeTypes]; }
    //    }

    //    public string Type
    //    {
    //        get { return (string)this[_type]; }
    //    }

    //    protected override ConfigurationPropertyCollection Properties
    //    {
    //        get
    //        {
    //            return _properties;
    //        }
    //    }
    //}
}
