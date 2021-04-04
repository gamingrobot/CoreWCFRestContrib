using System;
using CoreWCF.IdentityModel.Selectors;
using CoreWCFRestContrib.DependencyInjection;

namespace CoreWCFRestContrib.IdentityModel.Selectors
{
    public class UserNamePasswordValidatorProxy : UserNamePasswordValidator
    {
        private readonly Lazy<UserNamePasswordValidator> _validator;

        public UserNamePasswordValidatorProxy()
        {
            _validator = new Lazy<UserNamePasswordValidator>(GetValidator);
        }

        public override void Validate(string userName, string password)
        {
            _validator.Value.Validate(userName, password);
        }

        public virtual UserNamePasswordValidator GetValidator()
        {
            return DependencyResolver.Current.GetInfrastructureService<UserNamePasswordValidator>();
        }
    }

    public class UserNamePasswordValidatorProxy<T> : UserNamePasswordValidatorProxy where T : UserNamePasswordValidator
    {
        public override UserNamePasswordValidator GetValidator()
        {
            return DependencyResolver.Current.GetInfrastructureService<UserNamePasswordValidator>(typeof(T));
        }
    }
}
