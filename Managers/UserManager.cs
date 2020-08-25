using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waka.Brokers;
using Waka.Models;
using Waka.ViewModels;

namespace Waka.Managers
{
    public class UserManager
    {
        private readonly IStorageBroker<User> storage;

        public UserManager(IStorageBroker<User> storage)
        {
            this.storage = storage;
        }

        public bool SignIn(SignInViewModel model)
        {
            try
            {

                var users = storage.GetAll();
                var compareUser = storage.GetAll().Where(c => c.Email == model.Email).FirstOrDefault();
                var userToUpdate = users.Where(d => d.Email == model.Email).FirstOrDefault();

                if (userToUpdate == null)
                {
                    return false;
                }

                userToUpdate.IsSignedIn = true;
                storage.Update(userToUpdate, compareUser);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void Logout(Guid id)
        {
            try
            {

                var users = storage.GetAll();
                var compareUser = storage.GetAll().Where(c => c.UserId == id).FirstOrDefault();
                var userToUpdate = users.Where(d => d.UserId == id).FirstOrDefault();

                userToUpdate.IsSignedIn = false;
                storage.Update(userToUpdate, compareUser);
                PersistUser.userId = default;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public User CurrentSignedInUser(Guid id)
        {
            try
            {

                var users = storage.GetAll();
                var signedInUsers = storage.GetAll().Where(c => c.UserId == id).FirstOrDefault();
                return signedInUsers;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public bool DisableUser(Guid id)
        {
            try
            {

                var users = storage.GetAll();
                var compareUser = storage.GetAll().Where(c => c.UserId == id).FirstOrDefault();
                var userToUpdate = users.Where(d => d.UserId == id).FirstOrDefault();

                userToUpdate.IsEnabled = false;
                storage.Update(userToUpdate, compareUser);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public bool EnableUser(Guid id)
        {
            try
            {

                var users = storage.GetAll();
                var compareUser = storage.GetAll().Where(c => c.UserId == id).FirstOrDefault();
                var userToUpdate = users.Where(d => d.UserId == id).FirstOrDefault();

                userToUpdate.IsEnabled = true;
                storage.Update(userToUpdate, compareUser);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public bool UserIsSignedIn(Guid userId)
        {
            var user = storage.GetAll().Where(u => u.UserId == userId).FirstOrDefault();
            if (user == null)
            {
                return false;
            }

            if (user.IsSignedIn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
