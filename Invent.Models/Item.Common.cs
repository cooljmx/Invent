using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invent.Entities;
using InventUI.NHibernate;

namespace InventUI.Models
{
    public class ItemCommon : VirtualNotifyPropertyChanged, IModelItem
    {
        private DateTime dateInput;
        private int registerId;
        private int detailId;
        private string invName;
        private string invNumber;
        private DateTime dateStatus;
        private int npp;
        private string detailName;
        private string place;
        private int? placeCustomKey;
        private string workPlace;
        private string userMol;
        private string userOwner;
        private string workPlaceUser;
        private string statusName;
        //private int statusCustomKey;
        private string statusContentColor;
        private string detailTypeName;
        private int? detailCount;
        private int placeId;
        private int userMolId;
        private int userOwnerId;
        private int questions;

        public virtual int RegisterId { get { return registerId; } set { registerId = value; OverridedPropertyChanged("RegisterId"); } }
        public virtual int DetailId { get { return detailId; } set { detailId = value; NotifyPropertyChanged("DetailId"); } }
        /// <summary>
        /// Инвентарное имя
        /// </summary>
        public virtual string InvName { get { return invName; } set { invName = value; OverridedPropertyChanged("InvName"); } }
        /// <summary>
        /// Инвентарный номер
        /// </summary>
        public virtual string InvNumber { get { return invNumber; } set { invNumber = value; OverridedPropertyChanged("InvNumber"); } }
        /// <summary>
        /// Дата внесения в БД
        /// </summary>
        public virtual DateTime DateInput { get { return dateInput; } set { dateInput = value; OverridedPropertyChanged("DateInput"); } }
        /// <summary>
        /// Дата статуса
        /// </summary>
        public virtual DateTime DateStatus { get { return dateStatus; } set { dateStatus = value; OverridedPropertyChanged("DateStatus"); } }
        /// <summary>
        /// Порядковый номер
        /// </summary>
        public virtual int Npp { get { return npp; } set { npp = value; NotifyPropertyChanged("Npp"); } }
        /// <summary>
        /// Имя компонента
        /// </summary>
        public virtual string DetailName { get { return string.IsNullOrEmpty(detailName) ? InvName : string.Format("{0} - {1}", detailName, InvName); } set { detailName = value; NotifyPropertyChanged("DetailName"); } }
        public virtual int PlaceId { get { return placeId; } set { placeId = value; OverridedPropertyChanged("PlaceId"); } }
        /// <summary>
        /// Кабинет
        /// </summary>
        public virtual string Place { get { return place; } set { place = value; OverridedPropertyChanged("Place"); } }
        public virtual int? PlaceCustomKey { get { return placeCustomKey; } set { placeCustomKey = value; OverridedPropertyChanged("PlaceCustomKey"); } }
        /// <summary>
        /// Рабочее место
        /// </summary>
        public virtual string WorkPlace { get { return workPlace; } set { workPlace = value; OverridedPropertyChanged("WorkPlace"); } }
        public virtual int UserMolId { get { return userMolId; } set { userMolId = value; OverridedPropertyChanged("UserMolId"); } }
        /// <summary>
        /// МОЛ
        /// </summary>
        public virtual string UserMol { get { return userMol; } set { userMol = value; OverridedPropertyChanged("UserMol"); } }
        /// <summary>
        /// Кому передано
        /// </summary>
        public virtual int UserOwnerId { get { return userOwnerId; } set { userOwnerId = value; OverridedPropertyChanged("UserOwnerId"); } }
        public virtual string UserOwner { get { return userOwner; } set { userOwner = value; OverridedPropertyChanged("UserOwner"); } }
        /// <summary>
        /// Кому установлено
        /// </summary>
        public virtual string WorkPlaceUser { get { return workPlaceUser; } set { workPlaceUser = value; OverridedPropertyChanged("WorkPlaceUser"); } }
        public virtual string StatusName { get { return statusName; } set { statusName = value; OverridedPropertyChanged("StatusName"); } }
        //public virtual int StatusCustomKey { get { return statusCustomKey; } set { statusCustomKey = value; OverridedPropertyChanged("StatusCustomKey"); } }
        public virtual string StatusContentColor { get { return statusContentColor; } set { statusContentColor = value; OverridedPropertyChanged("StatusContentColor"); } }
        public virtual string DetailTypeName { get { return detailTypeName; } set { detailTypeName = value; OverridedPropertyChanged("DetailTypeName"); } }
        public virtual int? DetailCount { get { return detailCount; } set { detailCount = value; OverridedPropertyChanged("DetailCount"); } }
        public virtual int Questions { get { return questions; } set { questions = value; OverridedPropertyChanged("Questions"); OverridedPropertyChanged("HasQuestions"); } }
        public virtual bool HasQuestions { get { return Questions == 1; } }
        public virtual Int64 RecordPrimaryKey { get { return Convert.ToInt64(RegisterId); } }

        public virtual void OverridedPropertyChanged(string aName)
        {
            NotifyPropertyChanged(aName);
            switch (aName)
            {
                case "RegisterId":
                    NotifyPropertyChanged("RecordPrimaryKey");
                    break;
            }

            /*switch (aName)
            {
                case "SimpleName":
                    NotifyPropertyChanged("InvName");
                    break;
                case "PlaceSimpleName":
                    NotifyPropertyChanged("PlaceName");
                    break;
            }*/
        }
    }
}
