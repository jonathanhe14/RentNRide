using DataAccess.Crud;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class UserProfileManager
    {
        private UserProfileCrudFactory crudUser;

        public UserProfileManager()
        {
            crudUser = new UserProfileCrudFactory();
        }


        public UserProfile ValidateUser(UserProfile user)
        {
            UserProfile u = null;
            u = crudUser.Retrieve<UserProfile>(user);
            /*try
            {

                u = crudUser.Retrieve<UserProfile>(user);
                if (u == null)
                {
                    throw new BussinessException(9);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }*/

            return u;
        }

    }
}
