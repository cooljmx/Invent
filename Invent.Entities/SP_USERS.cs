using FluentNHibernate.Mapping;

namespace Invent.Entities
{
    public class SpUsers : VirtualNotifyPropertyChanged
    {
        private int? id;
        private string name;
        private string job;
        private decimal disabled;

        public virtual int? Id { get { return id; } set { id = value; NotifyPropertyChanged("Id"); } }
        public virtual string Name { get { return name; } set { name = value; NotifyPropertyChanged("Name"); NotifyPropertyChanged("NameAndJob"); } }
        public virtual string Job { get { return job; } set { job = value; NotifyPropertyChanged("Job"); NotifyPropertyChanged("NameAndJob"); } }
        public virtual decimal Disabled { get { return disabled; } set { disabled = value; NotifyPropertyChanged("Disabled"); } }
        public virtual string NameAndJob { get { return string.Concat(Name, !string.IsNullOrEmpty(Job) ? string.Format(" - {0}", Job) : string.Empty); } }
    }

    public class SpUsersMap : ClassMap<SpUsers>
    {
        public SpUsersMap()
        {
            Table("SP_USERS");
            Id(x => x.Id).Column("ID_USER").UnsavedValue(null);
            Map(x => x.Name).Column("NAME");
            Map(x => x.Job).Column("JOB");
            Map(x => x.Disabled).Column("DISABLED");
        }
    }
}
