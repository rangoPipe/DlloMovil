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
using Taller6.Data;
using Taller6.Models;

namespace Taller6.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Taller6.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Entity>("Entity");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EntityController : ODataController
    {
        private Taller6Context db = new Taller6Context();

        // GET: odata/Entity
        [EnableQuery]
        public IQueryable<Entity> GetEntity()
        {
            return db.Entities;
        }

        // GET: odata/Entity(5)
        [EnableQuery]
        public SingleResult<Entity> GetEntity([FromODataUri] int key)
        {
            return SingleResult.Create(db.Entities.Where(entity => entity.Id == key));
        }

        // PUT: odata/Entity(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Entity> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Entity entity = await db.Entities.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            patch.Put(entity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(entity);
        }

        // POST: odata/Entity
        public async Task<IHttpActionResult> Post(Entity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entities.Add(entity);
            await db.SaveChangesAsync();

            return Created(entity);
        }

        // PATCH: odata/Entity(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Entity> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Entity entity = await db.Entities.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            patch.Patch(entity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(entity);
        }

        // DELETE: odata/Entity(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Entity entity = await db.Entities.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            db.Entities.Remove(entity);
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

        private bool EntityExists(int key)
        {
            return db.Entities.Count(e => e.Id == key) > 0;
        }
    }
}
