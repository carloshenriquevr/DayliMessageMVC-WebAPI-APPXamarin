using DailyMessageMVC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DailyMessageMVC.Data.EntitiesMapping
{
    public class MessageMapping : EntityTypeConfiguration<Messages>, IMapping
    {
        public MessageMapping()
        {
            // Table
            ToTable("dbo.Messages");

            // Primary Key
            HasKey(x => x.MessageId);

            // Column Mappings
            Property(x => x.MessageId).IsRequired();
            Property(x => x.MessageText).IsRequired();
            Property(x => x.MessageDate).IsRequired();
            

        }
    }
}
