using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Invent.Entities;
using InventUI.NHibernate;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Util;
using NHibernate.Criterion;

namespace InventUI.Models
{
    public class ModelRegister<TGridItem, TEditableItem> : ModelCommon<TGridItem, TEditableItem>
        where TGridItem : IModelItem
        where TEditableItem : IModelItem
    {
        //public TGridItem RegisterFocusedGridRow { get; set; }

        protected override IQueryOver<TEditableItem, TEditableItem> BuildQuery(ISession session)
        {
            Register register = null;
            RegisterDetails registerDetail = null;
            SpStatuses status = null;
            SpUsers molUser = null;
            SpUsers owner = null;
            ItemCommon item = null;

            return (IQueryOver<TEditableItem, TEditableItem>)session.QueryOver(() => register)
                .JoinAlias(() => register.Status, () => status)
                .JoinAlias(() => register.UserMaterial, () => molUser)
                .JoinAlias(() => register.UserOwner, () => owner)
                .Left.JoinAlias(() => register.Details, () => registerDetail)
                .SelectList(list => list
                    .SelectGroup(c => c.Id).WithAlias(() => item.RegisterId)
                    .SelectGroup(c => c.DateInput).WithAlias(() => item.DateInput)
                    .SelectGroup(c => c.DateStatus).WithAlias(() => item.DateStatus)
                    .SelectGroup(c => c.InvName).WithAlias(() => item.InvName)
                    .SelectGroup(c => c.InvNumber).WithAlias(() => item.InvNumber)
                    .SelectGroup(c => status.Name).WithAlias(() => item.StatusName)
                    //.SelectGroup(c => status.CustomKey)
                    .SelectGroup(c => status.ContentColor).WithAlias(() => item.StatusContentColor)
                    .SelectGroup(c => molUser.Name).WithAlias(() => item.UserMol)
                    .SelectGroup(c => owner.Name).WithAlias(() => item.UserOwner)
                    .SelectCount(c => registerDetail.Id).WithAlias(() => item.DetailCount));
        }

        /*
        public override void ReloadModel()
        {
            using (var session = Singleton<HbManager>.Instance.NewSession)
            {
                var listQuery = BuildQuery(session)
                    .TransformUsing(Transformers.AliasToBean<ItemCommon>())
                    .List<ItemCommon>();

                Data.Clear();
                foreach (var listItem in listQuery)
                    Data.Add(listItem);
            }
        }
         * */

        /*
        public override void ReloadItem(ItemCommon a)
        {
            using (var session = Singleton<HbManager>.Instance.NewSession)
            {
                var refreshedItem = BuildQuery(session)
                    .Where(x => x.Id == a.RegisterId)
                    .TransformUsing(Transformers.AliasToBean<ItemCommon>())
                    .Take(1).List<ItemCommon>()
                    .FirstOrDefault();

                if (refreshedItem != null)
                {
                    var index =
                        Data.IndexOf(Data.FirstOrDefault(x => x.RegisterId == refreshedItem.RegisterId) ??
                                     new ItemCommon());
                    if (index > -1)
                        Data[index] = refreshedItem;
                }
            }
        }
         * */
/*
        public void DeleteFocusedItem()
        {
            if (RegisterFocusedGridRow != null)
            {
                using (var session = Singleton<HbManager>.Instance.NewSession)
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(session.Load(typeof(TEditableItem), RegisterFocusedGridRow.RecordPrimaryKey));
                        transaction.Commit();
                    }
                    Data.Remove(RegisterFocusedGridRow);
                }
            }
        }
 * */
    }
}
