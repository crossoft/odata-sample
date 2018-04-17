using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using RTMS_API;

namespace RTMS_API.Controllers
{
    public class WifiActivitiesController : ODataController
    {
        private OData_RTMSEntities db = new OData_RTMSEntities();

        // GET: /WifiActivities
        [EnableQuery]
        public IQueryable<WifiActivity> GetWifiActivities()
        {
            return db.WifiActivities;
        }

        // GET: /WifiActivities(5)
        [EnableQuery]
        public SingleResult<WifiActivity> GetWifiActivity([FromODataUri] double key)
        {
            return SingleResult.Create(db.WifiActivities.Where(wifiActivity => wifiActivity.id == key));
        }

        // PUT: /WifiActivities(5)
        public async Task<IHttpActionResult> Put([FromODataUri] double key, Delta<WifiActivity> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WifiActivity wifiActivity = await db.WifiActivities.FindAsync(key);
            if (wifiActivity == null)
            {
                return NotFound();
            }

            patch.Put(wifiActivity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WifiActivityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(wifiActivity);
        }

        // POST: /WifiActivities
        public async Task<IHttpActionResult> Post(WifiActivity wifiActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WifiActivities.Add(wifiActivity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WifiActivityExists(wifiActivity.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(wifiActivity);
        }

        // PATCH: /WifiActivities(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] double key, Delta<WifiActivity> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WifiActivity wifiActivity = await db.WifiActivities.FindAsync(key);
            if (wifiActivity == null)
            {
                return NotFound();
            }

            patch.Patch(wifiActivity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WifiActivityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(wifiActivity);
        }

        // DELETE: /WifiActivities(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] double key)
        {
            WifiActivity wifiActivity = await db.WifiActivities.FindAsync(key);
            if (wifiActivity == null)
            {
                return NotFound();
            }

            db.WifiActivities.Remove(wifiActivity);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WifiActivityExists(double key)
        {
            return db.WifiActivities.Count(e => e.id == key) > 0;
        }
    }
}
