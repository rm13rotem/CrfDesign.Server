using System;

namespace CrfDesign.Server.WebAPI.Models
{
    public interface IPersistantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}