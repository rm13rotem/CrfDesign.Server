using BuisnessLogic.DataContext;
using BuisnessLogic.Models;
using BuisnessLogic.Models.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.Managers
{
    public class CrfPageManager : Manager<CrfPage>
    {
        public CrfPageManager(CrfDesignContext context) : base(context)
        { }

        public override CrfPage Duplicate(CrfPage entity)
        {
            int originalId = entity.Id;
            var newEntity = base.Duplicate(entity);

            var componentManager = new Manager<CrfPageComponent>(_context);
            var crfComponentIds = _context.CrfPageComponents.Where(x => x.CRFPageId == originalId)
                .Select(x => x.Id).ToList().Distinct();
            foreach (var crfComponentId in crfComponentIds)
            {
                var crfPageComponent = _context.CrfPageComponents.Find(crfComponentId);
                var newCrfComponent = componentManager.Duplicate(crfPageComponent);
                newCrfComponent.CRFPageId = newEntity.Id;// belongs to new CRFPage
                newCrfComponent.CrfPage = newEntity;
                newCrfComponent.ModifiedDateTime = DateTime.Now;
                _context.SaveChanges(); 
            }
            return newEntity;
        }
    }
}

