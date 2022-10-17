using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReimbursementSystemAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Base
{
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = repository.Get();
            if (result.Count() != 0)
            {
                return Ok(result);

            }
            return NotFound();
        }


        [HttpGet("{Key}")]
        public ActionResult Get(Key Key)
        {
            var result = repository.Get(Key);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpDelete("{Key}")]
        public ActionResult Delete(Key key)
        {
            var result = repository.Delete(key);
            try
            {
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public ActionResult Update(Entity entity, Key key)
        {
            var result = repository.Update(entity, key);
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try {
                var result = repository.Insert(entity);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
