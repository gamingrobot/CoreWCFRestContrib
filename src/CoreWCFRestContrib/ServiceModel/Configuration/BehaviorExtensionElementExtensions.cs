using System.Reflection;

namespace CoreWCFRestContrib.ServiceModel.Configuration
{
    public static class BehaviorExtensionElementExtensions
    {
        public static object CreateBehavior(this BehaviorExtensionElement extensionElement)
        {
            try
            {
                return extensionElement.GetType().
                    GetMethod(
                        "CreateBehavior",
                        BindingFlags.Instance |
                        BindingFlags.NonPublic).
                        Invoke(extensionElement, new object[] { });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;                
            }
        }
    }
}
