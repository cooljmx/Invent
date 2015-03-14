using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace Invent.Entities
{
    public class Register : VirtualNotifyPropertyChanged, IModelItem
    {
        private int id;
        private string invName;
        private string invNum;
        private DateTime dateInput;
        private DateTime dateStatus;
        private string comment;
        private SpStatuses status;

        public virtual int Id { get { return id; } set { id = value; NotifyPropertyChanged("Id"); } }
        public virtual string InvName { get { return invName; } set { invName = value; NotifyPropertyChanged("InvName"); } }
        public virtual string InvNumber { get { return invNum; } set { invNum = value; NotifyPropertyChanged("InvNumber"); } }
        public virtual DateTime DateInput { get { return dateInput; } set { dateInput = value; NotifyPropertyChanged("DateInput"); } }
        public virtual DateTime DateStatus { get { return dateStatus; } set { dateStatus = value; NotifyPropertyChanged("DateStatus"); } }
        public virtual string Comment { get { return comment; } set { comment = value; NotifyPropertyChanged("Comment"); } }

        public virtual IList<RegisterDetails> Details { get; set; }

        public virtual SpUsers UserMaterial { get; set; }
        public virtual SpUsers UserOwner { get; set; }
        public virtual SpStatuses Status { get { return status; } set { status = value; NotifyPropertyChanged("Status"); } }

        public virtual Int64 RecordPrimaryKey { get; set; }
    }
    public class RegisterMap : ClassMap<Register>
    {
        public RegisterMap(){
            Table("REGISTER");
            Id(x => x.Id).Column("ID_REGISTER").GeneratedBy.TriggerIdentity();
            Map(x => x.RecordPrimaryKey).Column("ID_REGISTER").ReadOnly();  // Чтобы значение не было перезатерто
            Map(x => x.InvName).Column("INV_NAME");
            Map(x => x.InvNumber).Column("INV_NUMBER");
            Map(x => x.DateInput).Column("DATE_INPUT");
            References(x => x.UserMaterial).Column("ID_USER_MOL");
            References(x => x.UserOwner).Column("ID_USER_OWNER");
            References(x => x.Status).Column("ID_STATUS"); 
            Map(x => x.DateStatus).Column("DATE_STATUS");
            Map(x => x.Comment).Column("COMMENTARY");
            HasMany(x => x.Details).KeyColumn("ID_REGISTER").Not.LazyLoad().Cascade.AllDeleteOrphan().Inverse(); 
        }
    }

}
