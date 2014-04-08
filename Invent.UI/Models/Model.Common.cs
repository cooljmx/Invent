using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Common;
using DevExpress.Xpf.Editors.Helpers;
using Invent.Entities;
using InventUI.NHibernate;
using NHibernate.Transform;
using NHibernate;

namespace InventUI.Models
{
    public class ModelCommon<TGridItem, TEditableItem> : VirtualNotifyPropertyChanged
        where TGridItem : IModelItem
        where TEditableItem : IModelItem
    {
        private readonly ObservableCollection<TGridItem> data = new ObservableCollection<TGridItem>();
        public ObservableCollection<TGridItem> Data { get { return data; } }

        public TGridItem FocusedGridRow { get; set; }

        protected virtual IQueryOver<TEditableItem, TEditableItem> BuildQuery(ISession session)
        {
            return null;
        }

        public virtual void ReloadModel()
        {
            using (var session = Singleton<HbManager>.Instance.NewSession)
            {
                var listQuery = BuildQuery(session)
                    .TransformUsing(Transformers.AliasToBean<TGridItem>())
                    .List<TGridItem>();

                Data.Clear();
                foreach (var listItem in listQuery)
                    Data.Add(listItem);
            }
        }

        public virtual void ReloadItem(TGridItem a)
        {
            using (var session = Singleton<HbManager>.Instance.NewSession)
            {
                var refreshedItem = BuildQuery(session)
                    .Where(x => (x.RecordPrimaryKey == a.RecordPrimaryKey) /* a.RegisterId*/)
                    .TransformUsing(Transformers.AliasToBean<TGridItem>())
                    .Take(1).List<TGridItem>()
                    .FirstOrDefault();

                if (refreshedItem != null)
                {
                    var item = Data.FirstOrDefault(x => x.RecordPrimaryKey == refreshedItem.RecordPrimaryKey);
                    if (item != null)
                    {
                        var index = Data.IndexOf(item);
                        if (index > -1)
                            Data[index] = refreshedItem;
                    }
                    /*
                    var index =
                        Data.IndexOf(Data.FirstOrDefault(x => x.Id == refreshedItem.Id) ??
                                     new TGridItem());
                    if (index > -1)
                        Data[index] = refreshedItem;
                     * */
                }
            }
        }

        public void DeleteFocusedItem<T>()
        {
            if (FocusedGridRow != null)
            {
                using (var session = Singleton<HbManager>.Instance.NewSession)
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(session.Load(typeof(TEditableItem), Convert.ChangeType(FocusedGridRow.RecordPrimaryKey, typeof(T))));
                        transaction.Commit();
                    }
                    Data.Remove(FocusedGridRow);
                }
            }
        }


    }
}
