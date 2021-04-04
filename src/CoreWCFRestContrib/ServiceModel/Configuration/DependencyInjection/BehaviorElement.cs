using System;
using CoreWCFRestContrib.DependencyInjection;
using CoreWCFRestContrib.Reflection;
using CoreWCFRestContrib.ServiceModel.Description;

namespace CoreWCFRestContrib.ServiceModel.Configuration.DependencyInjection
{
    //public class BehaviorElement : BehaviorExtensionElement
    //{
    //    private const string ObjectFactoryTypeElement = "objectFactoryType";

    //    public override Type BehaviorType
    //    {
    //        get { return typeof(DependencyInjectionBehavior); }
    //    }

    //    protected override object CreateBehavior()
    //    {
    //        Type objectFactory;
    //        try
    //        {
    //            objectFactory = ObjectFactoryType.GetType<IDependencyResolver>();
    //        }
    //        catch (Exception e)
    //        {
    //            throw new Exception(string.Format("Invalid objectFactoryType specified in dependencyInjection behavior element. {0}", e));
    //        }

    //        return new DependencyInjectionBehavior(objectFactory);
    //    }

    //    [ConfigurationProperty(ObjectFactoryTypeElement, IsRequired = true)]
    //    public string ObjectFactoryType
    //    {
    //        get { return (string) base[ObjectFactoryTypeElement]; }
    //        set { base[ObjectFactoryTypeElement] = value; }
    //    }
    //}
}