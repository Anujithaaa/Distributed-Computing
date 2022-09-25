using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

//Anujitha Fernando (20903448) & Senara Mallikahewa (20903435) - Assignment 1

namespace Authenticator
{
    [ServiceContract]
    public interface AuthenticatorInterface
    {
        [OperationContract]
        String Register(String name, String Password);

        [OperationContract]
        int Login(String name, String Password);

        [OperationContract]
        String validate(int token);
    }
}
