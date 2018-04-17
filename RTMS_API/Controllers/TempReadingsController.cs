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
    public class TempReadingsController : ODataController
    {
        private OData_RTMSEntities db = new OData_RTMSEntities();

        // GET: /TempReadings
        [EnableQuery(PageSize = 100)]
        public IQueryable<TempReading> GetTempReadings()
        {
            return db.TempReadings;
        }

        // GET: /TempReadings(5)
        [EnableQuery]
        public SingleResult<TempReading> GetTempReading([FromODataUri] double key)
        {
            return SingleResult.Create(db.TempReadings.Where(tempReading => tempReading.id == key));
        }

        // PUT: /TempReadings(5)
        public async Task<IHttpActionResult> Put([FromODataUri] double key, Delta<TempReading> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TempReading tempReading = await db.TempReadings.FindAsync(key);
            if (tempReading == null)
            {
                return NotFound();
            }

            patch.Put(tempReading);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempReadingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tempReading);
        }

        // POST: /TempReadings
        public async Task<IHttpActionResult> Post(TempReading tempReading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TempReadings.Add(tempReading);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TempReadingExists(tempReading.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(tempReading);
        }

        // PATCH: /TempReadings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] double key, Delta<TempReading> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TempReading tempReading = await db.TempReadings.FindAsync(key);
            if (tempReading == null)
            {
                return NotFound();
            }

            patch.Patch(tempReading);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempReadingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tempReading);
        }

        // DELETE: /TempReadings(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] double key)
        {
            TempReading tempReading = await db.TempReadings.FindAsync(key);
            if (tempReading == null)
            {
                return NotFound();
            }

            db.TempReadings.Remove(tempReading);
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

        private bool TempReadingExists(double key)
        {
            return db.TempReadings.Count(e => e.id == key) > 0;
        }
    }
}
