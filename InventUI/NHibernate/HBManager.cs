using System.Reflection;
using FirebirdSql.Data.FirebirdClient;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventUI.NHibernate
{
    public class HbManager
    {
        private ISessionFactory factory = null;
        //private ISession session = null;
      
        protected HbManager() { }

        /*
        public ISessionFactory Factory
        {
            get { return factory; }
        }
         * */
        public ISession NewSession { get { return factory.OpenSession(); } }

        public IStatelessSession StatelessSession { get { return factory.OpenStatelessSession(); } }

        public void SetConnectionString(string aConnectionString)
        {
            if (factory == null)
                factory = CreateSessionFactory(aConnectionString);
        }

        private ISessionFactory CreateSessionFactory(string aConnectionString)
        {
            var fbc = new FirebirdConfiguration();

            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(fbc.ConnectionString(aConnectionString).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<App>())
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.Load("Invent.Entities")))
                .Diagnostics(x => x.Enable())
                .BuildSessionFactory();

            return sessionFactory;
        }
    }
}
