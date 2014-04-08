using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Common;
using DevExpress.Xpf.Layout.Core;
using InventUI.NHibernate;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Transform;
using System.Collections.ObjectModel;

namespace InventUI.Models
{
    /*
    public class ReferencesCollection
    {
        private IList<SpPlaces> placesList = null;
        private IList<SpStatuses> statusesList = null;
        private IList<SpUsers> usersList = null;
        private IList<SpDetailTypes> detailTypesList = null;
        private IList<WorkPlaces> workPlaces = null;

        private readonly ObservableCollection<SpPlaces> spPlacesCollection = new ObservableCollection<SpPlaces>();
        private readonly ObservableCollection<SpStatuses> spStatusesCollection = new ObservableCollection<SpStatuses>();
        private readonly ObservableCollection<SpUsers> spUsersCollection = new ObservableCollection<SpUsers>();
        private readonly ObservableCollection<SpDetailTypes> spDetailTypesCollection = new ObservableCollection<SpDetailTypes>();
        private readonly ObservableCollection<WorkPlaces> workPlacesCollection = new ObservableCollection<WorkPlaces>();

        public IList<SpPlaces> PlacesList { get { return placesList; } }
        public IList<SpStatuses> StatusesList { get { return statusesList; } }
        public IList<SpUsers> UsersList { get { return usersList; } }
        public IList<SpDetailTypes> DetailTypesList { get { return detailTypesList; } }
        public IList<WorkPlaces> WorkPlaces { get { return workPlaces; } }

        public ObservableCollection<SpPlaces> SpPlacesCollection { get { return spPlacesCollection; } }
        public ObservableCollection<SpStatuses> SpStatusesCollection { get { return spStatusesCollection; } }
        public ObservableCollection<SpUsers> SpUsersCollection { get { return spUsersCollection; } }
        public ObservableCollection<SpDetailTypes> SpDetailTypesCollection { get { return spDetailTypesCollection; } }
        public ObservableCollection<WorkPlaces> WorkPlacesCollection { get { return workPlacesCollection; } }

        protected ReferencesCollection()
        {
        }


        public void Lock(ISession aSession)
        {
            foreach (var item in placesList) aSession.Lock(item, LockMode.None);
            foreach (var item in statusesList) aSession.Lock(item, LockMode.None);
            foreach (var item in usersList) aSession.Lock(item, LockMode.None);
            foreach (var item in detailTypesList) aSession.Lock(item, LockMode.None);
            foreach (var item in workPlaces) aSession.Lock(item, LockMode.None);
        }

        public void Unlock(ISession aSession)
        {
            foreach (var item in placesList) aSession.Evict(item);
            foreach (var item in statusesList) aSession.Evict(item);
            foreach (var item in usersList) aSession.Evict(item);
            foreach (var item in detailTypesList) aSession.Evict(item);
            foreach (var item in workPlaces) aSession.Evict(item);
        }

        public void Reload()
        {
            using (var session = Singleton<HbManager>.Instance.NewSession)
            {
                workPlaces = session.QueryOver<WorkPlaces>().ReadOnly().List<WorkPlaces>();
                placesList = session.QueryOver<SpPlaces>().ReadOnly().List<SpPlaces>();
                statusesList = session.QueryOver<SpStatuses>().ReadOnly().List<SpStatuses>();
                usersList = session.QueryOver<SpUsers>().ReadOnly().List<SpUsers>();
                detailTypesList = session.QueryOver<SpDetailTypes>().ReadOnly().List<SpDetailTypes>();
            }

            spPlacesCollection.Clear();
            spStatusesCollection.Clear();
            spUsersCollection.Clear();
            spDetailTypesCollection.Clear();
            workPlacesCollection.Clear();
            foreach (var item in placesList) spPlacesCollection.Add(item);
            foreach (var item in statusesList) spStatusesCollection.Add(item);
            foreach (var item in usersList) spUsersCollection.Add(item);
            foreach (var item in detailTypesList) spDetailTypesCollection.Add(item);
            foreach (var item in workPlaces) workPlacesCollection.Add(item);
        }
    }
     * */
}
