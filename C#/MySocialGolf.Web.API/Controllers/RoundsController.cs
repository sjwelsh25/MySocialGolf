using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using MySocialGolf.DataModel;
using MySocialGolf.DataManager;

namespace MySocialGolf.Web.API.Controllers
{
    public class RoundsController : ApiController
    {
        // GET: api/users/5/rounds
        /// <summary>
        /// Get a list of GolfRounds for a User
        /// </summary>
        /// <returns>List of GolfRoundDataModels</returns>
        [Route("api/users/{userid}/rounds")]
        public IHttpActionResult GetRoundsForUser(int userId)
        {
            var grMngr = new GolfRoundsDataManager();
            IEnumerable<GolfRoundDataModel> roundsList;
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
        /// <returns>GolfRoundDataModel</returns>
        public IHttpActionResult Get(int roundId = 0)
        {
            var grMngr = new GolfRoundsDataManager();
            GolfRoundDataModel round;
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
        /// <returns>GolfRoundDataModel</returns>
        [Route("api/users/{userid}/rounds")]
        public IHttpActionResult Post(int userId, GolfRoundDataModel golfRound)
        {
            {
                var grMngr = new GolfRoundsDataManager();
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
        public void Put(GolfRoundDataModel golfRound)
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
