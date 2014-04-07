using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invent.Entities;
using InventUI.NHibernate;
using Common;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Hql.Ast.ANTLR.Tree;
using NHibernate.Util;

namespace InventUI.Models.References
{
    public delegate void ChooseWorkItemEventHandler(object sender, ChooseWorkItemEventArgs e);
    public class ChooseWorkItemEventArgs : EventArgs
    {
        public WorkPlaces Item { get; set; }
    }

    public class ModelReferenceWorkPlaces : VirtualNotifyPropertyChanged, IDisposable
    {
        private readonly ISession session = Singleton<HbManager>.Instance.NewSession;

        private IList<WorkPlaces> workPlaces;
        private IList<SpPlaces> placesList;
        private IList<SpUsers> usersList; 
        private readonly ObservableCollection<WorkPlaces> workPlacesCollection = new ObservableCollection<WorkPlaces>();

        public ObservableCollection<WorkPlaces> WorkPlacesCollection { get { return workPlacesCollection; } }
        public IList<SpPlaces> PlacesList { get { return placesList ?? (placesList = session.QueryOver<SpPlaces>().ReadOnly().List()); } }
        public IList<SpUsers> UsersList { get { return usersList ?? (usersList = session.QueryOver<SpUsers>().ReadOnly().List()); } }
        public WorkPlaces FocusedGridRow { get; set; }

        public event ChooseWorkItemEventHandler ChooseItem = null;

        public void Load()
        {
            workPlaces = session.QueryOver<WorkPlaces>().List<WorkPlaces>();

            foreach (var item in workPlaces) workPlacesCollection.Add(item);
            workPlacesCollection.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        workPlaces.Add((WorkPlaces)args.NewItems[0]);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        workPlaces.Remove((WorkPlaces)args.OldItems[0]);
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
            var item = new WorkPlaces();
            WorkPlacesCollection.Add(item);
        }

        public void DeleteFocusedItem()
        {
            if (FocusedGridRow != null)
                WorkPlacesCollection.Remove(FocusedGridRow);            
        }

        public bool InvokeFocusedItem()
        {
            if (ChooseItem == null || FocusedGridRow == null) return false;
            ChooseItem(this, new ChooseWorkItemEventArgs() {Item = FocusedGridRow});
            return true;
        }

        public void Dispose()
        {
            session.Dispose();
        }
    }
}
