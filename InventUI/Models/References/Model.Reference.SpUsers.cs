using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Common;
using Invent.Entities;
using InventUI.NHibernate;
using NHibernate;

namespace InventUI.Models.References
{
    public class ModelReferenceSpUsers : VirtualNotifyPropertyChanged, IDisposable
    {
        private readonly ISession session = Singleton<HbManager>.Instance.NewSession;
        private IList<SpUsers> usersList; 
        private readonly ObservableCollection<SpUsers> spUserCollection = new ObservableCollection<SpUsers>();
        public ObservableCollection<SpUsers> SpUserCollection { get { return spUserCollection; } }
        public SpUsers FocusedGridRow { get; set; }
        public void Load()
        {
            usersList = session.QueryOver<SpUsers>().List<SpUsers>();

            foreach (var item in usersList) spUserCollection.Add(item);
            spUserCollection.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        usersList.Add((SpUsers)args.NewItems[0]);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        usersList.Remove((SpUsers)args.OldItems[0]);
                        break;
                }
            };
        }
        public void Save()
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Flush();
                transaction.Commit();
            }
        }
        public void AddItem()
        {
            var item = new SpUsers();
            spUserCollection.Add(item);
        }
        public void DeleteFocusedItem()
        {
            if (FocusedGridRow != null)
                spUserCollection.Remove(FocusedGridRow);
        }
        public void Dispose()
        {
            session.Dispose();
        }
    }
}
