using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using MySocialGolf.DtoModel;
using MySocialGolf.DtoManager;

namespace MySocialGolf.Web.API.Controllers
{
    public class RoundsController : ApiController
    {
        // GET: api/users/5/rounds
        /// <summary>
        /// Get a list of GolfRounds for a User
        /// </summary>
        /// <returns>List of GolfRoundDtos</returns>
        [Route("api/users/{userid}/rounds")]
        public IHttpActionResult GetRoundsForUser(int userId)
        {
            var grMngr = new GolfRoundsDtoManager();
            IEnumerable<GolfRoundDto> roundsList;
            try
            {
                roundsList = grMngr.ListGolfRoundForUser(userId);// all for this user
                if (roundsList == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(roundsList);
        }

        // GET: api/Rounds/5
        /// <summary>
        /// Get a GolfRound
        /// </summary>
        /// <returns>GolfRoundDto</returns>
        public IHttpActionResult Get(int roundId = 0)
        {
            var grMngr = new GolfRoundsDtoManager();
            GolfRoundDto round;
            try
            {
                round = grMngr.GetGolfRound(roundId);
                if (round == null)
                {
                    return BadRequest($"Round {roundId} does not exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(round);
        }

        // POST: api/Rounds
        /// <summary>
        /// Add a GolfRound
        /// </summary>
        /// <returns>GolfRoundDto</returns>
        [Route("api/users/{userid}/rounds")]
        public IHttpActionResult Post(int userId, GolfRoundDto golfRound)
        {
            {
                var grMngr = new GolfRoundsDtoManager();
                try
                {
                    grMngr.AddGolfRound(golfRound);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(golfRound.SubmitMessage);
            }
        }

        // PUT: api/Rounds/5
        /// <summary>
        /// Update a GolfRound
        /// </summary>
        /// <returns></returns>
        public void Put(GolfRoundDto golfRound)
        {

        }

        // DELETE: api/Rounds/5
        /// <summary>
        /// Delete a GolfRound
        /// </summary>
        /// <returns></returns>
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
