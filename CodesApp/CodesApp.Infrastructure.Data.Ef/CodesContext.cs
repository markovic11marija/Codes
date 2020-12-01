
using CodesApp.Infrastructure.Data.Ef.Configuration;
using CodesApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodesApp.Infrastructure.Data.Ef
{
    public class CodesContext : DbContext
    {
        public Action<ModelBuilder> ModelBuilderConfigurator { private get; set; }

        public CodesContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var a = typeof(CodeConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeConfiguration).Assembly);

            RegisterEntities(modelBuilder);

            ModelBuilderConfigurator?.Invoke(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void RegisterEntities(ModelBuilder modelBuilder)
        {
            MethodInfo entityMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == "Entity" && m.IsGenericMethod);

            IEnumerable<Type> entityTypes = Assembly.GetAssembly(typeof(Code)).GetTypes()
                .Where(x => (x.IsSubclassOf(typeof(Entity)) && !x.IsAbstract));

            foreach (Type type in entityTypes)
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }
    }
}
