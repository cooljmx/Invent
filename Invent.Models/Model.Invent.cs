using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
//using System.Windows.Documents;
//using System.Windows.Media;
using Common;
//using DevExpress.XtraPrinting.Native;
using FluentNHibernate.Conventions;
using Invent.Entities;
using InventUI.NHibernate;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Transform;

namespace InventUI.Models
{
    public class ModelInvent : VirtualNotifyPropertyChanged
    {
        private readonly ObservableCollection<ItemInvent> data = new ObservableCollection<ItemInvent>();
        private readonly ObservableCollection<ItemInventFilter> places = new ObservableCollection<ItemInventFilter>();
        private readonly ObservableCollection<ItemInventFilter> ownerUsers = new ObservableCollection<ItemInventFilter>();
        private readonly ObservableCollection<ItemInventFilter> molUsers = new ObservableCollection<ItemInventFilter>();
        public ObservableCollection<ItemInvent> Data { get { return data; } }
        public ObservableCollection<ItemInventFilter> Places { get { return places; } }
        public ObservableCollection<ItemInventFilter> OwnerUsers { get { return ownerUsers; } }
        public ObservableCollection<ItemInventFilter> MolUsers { get { return molUsers; } }

        public void ReloadFilters()
        {
            using (var session = Singleton<HbManager>.Instance.NewSession)
            {
                Register registers = null;
                RegisterDetails registerDetails = null;
                SpUsers ownerUserItem = null;
                SpUsers molUserItem = null;
                WorkPlaces workPlaces = null;
                SpPlaces placeItem = null;

                SpPlaces place = null;
                SpUsers user = null;

                var placeList = session.QueryOver<RegisterDetails>(() => registerDetails)
                    .JoinAlias(() => registerDetails.WorkPlace, () => workPlaces)
                    .JoinAlias(() => workPlaces.Place, () => placeItem)
                    .SelectList(list => list
                        .SelectGroup(c => placeItem.Id).WithAlias(() => place.Id)
                        .SelectGroup(c => placeItem.Name).WithAlias(() => place.Name))
                    .TransformUsing(Transformers.AliasToBean<ItemInventFilter>())
                    .List<ItemInventFilter>();
                places.Clear();
                foreach (var item in placeList)
                {
                    places.Add(item);
                }

                var userList = session.QueryOver<Register>(() => registers)
                    .JoinAlias(() => registers.UserOwner, () => ownerUserItem)
                    .SelectList(list => list
                        .SelectGroup(c => ownerUserItem.Id).WithAlias(() => user.Id)
                        .SelectGroup(c => ownerUserItem.Name).WithAlias(() => user.Name))
                    .TransformUsing(Transformers.AliasToBean<ItemInventFilter>())
                    .List<ItemInventFilter>();
                ownerUsers.Clear();
                foreach (var item in userList)
                {
                    ownerUsers.Add(item);
                }

                userList = session.QueryOver<Register>(() => registers)
                    .JoinAlias(() => registers.UserMaterial, () => molUserItem)
                    .SelectList(list => list
                        .SelectGroup(c => molUserItem.Id).WithAlias(() => user.Id)
                        .SelectGroup(c => molUserItem.Name).WithAlias(() => user.Name))
                    .TransformUsing(Transformers.AliasToBean<ItemInventFilter>())
                    .List<ItemInventFilter>();
                molUsers.Clear();
                foreach (var item in userList)
                {
                    molUsers.Add(item);
                }
            }
        }
        public void ReloadModel()
        {
            using (var session = Singleton<HbManager>.Instance.NewSession)
            {
                Register register = null;
                RegisterDetails registerDetail = null;
                WorkPlaces workPlace = null;
                SpPlaces place = null;
                SpDetailTypes detailType = null;
                SpStatuses status = null;
                SpUsers molUser = null;
                SpUsers owner = null;
                SpUsers wpUser = null;
                ItemInvent item = null;

                var listQuery = session.QueryOver<Register>(() => register)
                    .JoinAlias(() => register.Status, () => status)
                    .JoinAlias(() => register.UserMaterial, () => molUser)
                    .JoinAlias(() => register.UserOwner, () => owner)
                    .Left.JoinAlias(() => register.Details, () => registerDetail)
                    .Left.JoinAlias(() => registerDetail.DetailType, () => detailType)
                    .Left.JoinAlias(() => registerDetail.WorkPlace, () => workPlace)
                    .Left.JoinAlias(() => workPlace.Place, () => place)
                    .Left.JoinAlias(() => workPlace.User, () => wpUser)
                    .SelectList(list => list
                        .SelectGroup(c => c.Id).WithAlias(() => item.RegisterId)
                        .SelectGroup(c => registerDetail.Id).WithAlias(() => item.DetailId)
                        .SelectGroup(c => c.DateInput).WithAlias(() => item.DateInput)
                        .SelectGroup(c => c.DateStatus).WithAlias(() => item.DateStatus)
                        .SelectCount(c => registerDetail.Id).WithAlias(() => item.DetailCount)
                        .SelectGroup(c => registerDetail.Name).WithAlias(() => item.DetailName)
                        .SelectGroup(c => detailType.Name).WithAlias(() => item.DetailTypeName)
                        .SelectGroup(c => c.InvName).WithAlias(() => item.InvName)
                        .SelectGroup(c => c.InvNumber).WithAlias(() => item.InvNumber)
                        .SelectGroup(c => registerDetail.Npp).WithAlias(() => item.Npp)
                        .SelectGroup(c => place.Id).WithAlias(() => item.PlaceId)
                        .SelectGroup(c => place.Name).WithAlias(() => item.Place)
                        .SelectGroup(c => workPlace.CustomKey).WithAlias(() => item.PlaceCustomKey)
                        .SelectGroup(c => status.Name).WithAlias(() => item.StatusName)
                        //.SelectGroup(c => status.CustomKey).WithAlias(() => item.StatusCustomKey)
                        .SelectGroup(c => status.ContentColor).WithAlias(() => item.StatusContentColor)
                        .SelectGroup(c => molUser.Id).WithAlias(() => item.UserMolId)
                        .SelectGroup(c => molUser.Name).WithAlias(() => item.UserMol)
                        .SelectGroup(c => owner.Id).WithAlias(() => item.UserOwnerId)
                        .SelectGroup(c => owner.Name).WithAlias(() => item.UserOwner)
                        .SelectGroup(c => workPlace.Name).WithAlias(() => item.WorkPlace)
                        .SelectGroup(c => wpUser.Name).WithAlias(() => item.WorkPlaceUser))
                        .TransformUsing(Transformers.AliasToBean<ItemInvent>())
                    .List<ItemInvent>();

                listQuery = listQuery
                    .Where(x => !places.Any(a => a.Checked) || places.Where(a => a.Checked).Any(y => y.Id == x.PlaceId))
                    .Where(x => !molUsers.Any(a => a.Checked) || molUsers.Where(a => a.Checked).Any(y => y.Id == x.UserMolId))
                    .Where(x => !ownerUsers.Any(a => a.Checked) || ownerUsers.Where(a => a.Checked).Any(y => y.Id == x.UserOwnerId))
                    .ToList();
                
                Data.Clear();
                foreach (var listItem in listQuery) Data.Add(listItem);}
        }
    }
}
