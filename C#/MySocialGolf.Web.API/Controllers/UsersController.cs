using System.Collections.Generic;
using System.Web.Http;
using MySocialGolf.DtoManager;
using MySocialGolf.DtoModel;
using System;

namespace MySocialGolf.Web.API
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Get a list of Users
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var udm = new UserDtoManager();
            IEnumerable<UserDto> userList = null;
            try
            {
                userList = udm.ListUser(null); // All
                if (userList == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(userList);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Get a User
        /// </summary>
        /// <returns>UserDto</returns> 
        public IHttpActionResult Get(int id)
        {
            var udm = new UserDtoManager();
            UserDto user;
            try
            {
                user = udm.GetUser(id);
                if (user == null)
                {
                    return BadRequest($"User {id} does not exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(user);
        }

        // POST api/<controller>
        /// <summary>
        /// Add a User
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Post(UserDto userDto)
        {
            var udm = new UserDtoManager();
            try
            {
                udm.AddUser(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(userDto.SubmitMessage);

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Update a User
        /// </summary>
        /// <returns></returns>
        public void Put(UserDto userDto)
        {
            var udm = new UserDtoManager();
            udm.UpdateUser(userDto);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete a User
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var udm = new UserDtoManager();
            string result = "";
            try
            {
                result = udm.DeleteUser(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);

        }


    }
}