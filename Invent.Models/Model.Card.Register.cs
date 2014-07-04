using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using Common;
using Invent.Entities;
using InventUI.NHibernate;
using NHibernate;
using System.Collections.ObjectModel;
using NHibernate.Linq;

namespace InventUI.Models
{
    public class ModelCardRegister : VirtualNotifyPropertyChanged, IDisposable
    {
        private readonly ISession session = Singleton<HbManager>.Instance.NewSession;

        private Register registerItem = new Register();
        //private readonly ReferencesCollection references = Singleton<ReferencesCollection>.Instance;
        private readonly ObservableCollection<RegisterDetails> details = new ObservableCollection<RegisterDetails>();

        private IList<SpPlaces> placesList;
        private IList<SpStatuses> statusesList;
        private IList<SpUsers> usersList;
        private IList<SpDetailTypes> detailTypesList;
        private IList<WorkPlaces> workPlacesList;

        public Register RegisterItem { get { return registerItem; } }
        public ObservableCollection<RegisterDetails> Details { get { return details; } }
        //public ReferencesCollection References { get { return references; } }
        public RegisterDetails DetailsFocusedGridRow { get; set; }

        public IList<SpPlaces> PlacesList { get { return placesList ?? (placesList = session.QueryOver<SpPlaces>().List()); } }
        public IList<SpStatuses> StatusesList { get { return statusesList ?? (statusesList = session.QueryOver<SpStatuses>().List()); } }
        public IList<SpUsers> UsersList { get { return usersList ?? (usersList = session.QueryOver<SpUsers>().List()); } }
        public IList<SpDetailTypes> DetailTypesList { get { return detailTypesList ?? (detailTypesList = session.QueryOver<SpDetailTypes>().List()); } }
        public IList<WorkPlaces> WorkPlacesList { get { return workPlacesList ?? (workPlacesList = session.QueryOver<WorkPlaces>().OrderBy(x => x.Name).Asc.List()); } }

        /*
        public ModelCardRegister()
        {
            placesList = session.QueryOver<SpPlaces>().List();
            statusesList = session.QueryOver<SpStatuses>().List();
            usersList = session.QueryOver<SpUsers>().List();
            detailTypesList = session.QueryOver<SpDetailTypes>().List();
            workPlacesList = session.QueryOver<WorkPlaces>().OrderBy(x => x.Name).Asc.List();
        }
         * */

        public void Create()
        {
            registerItem.Details = new List<RegisterDetails>();
            registerItem.DateInput = DateTime.Now.Date;
            registerItem.DateStatus = DateTime.Now.Date;
            ApplyCollectionChanged();
        }

        public void Load(ItemCommon a)
        {
            //Singleton<ReferencesCollection>.Instance.Lock(session);
            registerItem = session.Get<Register>(a.RegisterId);
            //Singleton<ReferencesCollection>.Instance.Unlock(session);

            foreach (var item in registerItem.Details.OrderBy(x=>x.Npp)) details.Add(item);
            ApplyCollectionChanged();
        }

        public void Save()
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(registerItem);
                session.Flush();
                transaction.Commit();
            }
        }

        public void DeleteFocusedItem()
        {
            if (DetailsFocusedGridRow != null)
                Details.Remove(DetailsFocusedGridRow);
        }

        private void ApplyCollectionChanged()
        {
            details.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        registerItem.Details.Add((RegisterDetails)args.NewItems[0]);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        registerItem.Details.Remove((RegisterDetails)args.OldItems[0]);
                        break;
                }
            };
        }

        public IList<string> Validate()
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(registerItem.InvName))
                errors.Add("Необходимо указать наименование");
            if (registerItem.DateInput == DateTime.MinValue)
                errors.Add("Некорректная дата ввода в эксплуатацию");
            if (registerItem.UserMaterial == null)
                errors.Add("Необходимо выбрать материально ответственное лицо");
            if (registerItem.Status == null)
                errors.Add("Необходимо выбрать статус");
            if (registerItem.DateStatus == DateTime.MinValue)
                errors.Add("Некорректная дата состояния");
            //if (registerItem.UserOwner == null)
            //    errors.Add("Необходимо выбрать за кем закреплено оборудование");
            if (registerItem.Details.Any(x => string.IsNullOrEmpty(x.Name)))
                errors.Add("Необходимо заполнить описание каждого компонента из состава основного средства");
            if (registerItem.Details.Any(x => x.Npp <= 0 || x.Npp > 999999))
                errors.Add("Один из порядковых номеров некорректен");
            if (registerItem.Details.Count != registerItem.Details.Select(x=>x.Npp).Distinct().Count())
                errors.Add("Порядковые номера не должны повторяться");

            return
                errors;
        }

        public void Dispose()
        {
            session.Clear();
            session.Dispose();
        }
    }
}
