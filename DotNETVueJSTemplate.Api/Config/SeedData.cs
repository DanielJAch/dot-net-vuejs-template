using System;
using System.Collections.Generic;
using System.Linq;
using DotNETVueJSTemplate.Data;
using DotNETVueJSTemplate.Model;

namespace DotNETVueJSTemplate.Api.Config
{
    public class SeedData
    {
        public static void SeedExampleEntities()
        {
            using (var db = new ExampleContext(Constants.ConnectionStringName))
            {
                if (!db.ExampleEntities.Any())
                {
                    var entities = new List<ExampleEntity>
                    {
                        new ExampleEntity{ ExampleProperty = "Example Property 1", CreateDate = DateTime.Now, CreatedBy = "SYSTEM" },
                        new ExampleEntity{ ExampleProperty = "Example Property 2", CreateDate = DateTime.Now, CreatedBy = "SYSTEM" },
                        new ExampleEntity{ ExampleProperty = "Example Property 3", CreateDate = DateTime.Now, CreatedBy = "SYSTEM" },
                        new ExampleEntity{ ExampleProperty = "Example Property 4", CreateDate = DateTime.Now, CreatedBy = "SYSTEM" },
                        new ExampleEntity{ ExampleProperty = "Example Property 5", CreateDate = DateTime.Now, CreatedBy = "SYSTEM" }
                    };

                    db.ExampleEntities.AddRange(entities);

                    db.SaveChanges();
                }
            }
        }
    }
}