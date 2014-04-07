using FluentNHibernate.Mapping;

namespace Invent.Entities
{
    public class RegisterDetails : VirtualNotifyPropertyChanged
    {
        private int id;
        //private int idRegister;
        private int npp;
        private string name;
        private string comment;
        private int questions;

        public virtual int Id { get { return id; } set { id = value; NotifyPropertyChanged("Id"); } }
        public virtual int Npp { get { return npp; } set { npp = value; NotifyPropertyChanged("Npp"); } }
        public virtual string Name { get { return name; } set { name = value; NotifyPropertyChanged("InvName"); } }
        public virtual string Comment { get { return comment; } set { comment = value; NotifyPropertyChanged("Comment"); } }
        public virtual int Questions { get { return questions; } set { questions = value; NotifyPropertyChanged("Questions"); NotifyPropertyChanged("HasQuestions"); } }
        public virtual Register Parent { get; set; }
        public virtual WorkPlaces WorkPlace { get; set; }
        public virtual SpDetailTypes DetailType { get; set; }
    }

    public class RegisterDetailsMap : ClassMap<RegisterDetails>
    {
        public RegisterDetailsMap()
        {
            Table("REGISTER_DETAILS");
            Id(x => x.Id).Column("ID_REGISTER_DETAILS").GeneratedBy.TriggerIdentity();
            References(x => x.Parent).Column("ID_REGISTER");
            Map(x => x.Npp).Column("NPP");
            Map(x => x.Name).Column("NAME");
            References(x => x.WorkPlace).Column("ID_WORK_PLACE");
            References(x=>x.DetailType).Column("ID_DETAIL_TYPE");
            Map(x => x.Comment).Column("COMMENTARY");
            Map(x => x.Questions).Column("QUESTIONS");
        }
    }
}
