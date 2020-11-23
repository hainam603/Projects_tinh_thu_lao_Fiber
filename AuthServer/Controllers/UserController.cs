using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Server.Users;
using ClassModel.Model.Fiber;
using ClassModel.Model.Respond;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser m_user;
        public UserController(IUser _user)
        {
            m_user = _user;
        }
        [HttpPost("hashpass")]
        public dynamic hashpass([FromBody] User user)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.message = "success";
                data.data = m_user.uHashPass(user.PassWord);
                data.success = true;
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [HttpGet("getById")]
        public dynamic getById(int id)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.message = "success";
                data.data = m_user.getById(id);
                data.success = true;
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        [HttpGet("getAllUser")]
        public dynamic getAllUser()
        {
            DataRespond data = new DataRespond();
            try
            {
                data.message = "success";
                data.data = m_user.getAll();
                data.success = true;
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [HttpPut("update")]
        public dynamic update(User user)
        {
            DataRespond data = new DataRespond();
            try
            {
                if (user.PassWord != null)
                {
                    if (user.PassWord != "")
                    {
                        user.PassWord = m_user.uHashPass(user.PassWord);
                        m_user.update(user);
                        data.message = "update user success";
                    }
                    else
                    {
                        m_user.updateUserNonePassword(user);
                        data.message = "update user none password success";
                    }
                }
                else {
                    m_user.updateUserNonePassword(user);
                    data.message = "update user none password success";
                }
                    
                data.success = true;
               
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [HttpPost("create")]
        public dynamic create(User user)
        {
            DataRespond data = new DataRespond();
            try
            {
                if (!m_user.checkUsername(user))
                {
                    user.PassWord = m_user.uHashPass(user.PassWord);
                    m_user.insert(user);
                    data.message = "create user success";
                    data.success = true;
                }
                else
                {
                    data.message = "Username already exists";
                    data.success = false;
                }
            

            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
    }
}
