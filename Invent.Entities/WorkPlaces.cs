using FluentNHibernate.Mapping;

namespace Invent.Entities
{
    public class WorkPlaces : VirtualNotifyPropertyChanged
    {
        private int? id;
        private string name;
        private int? customKey;

        public virtual int? Id { get { return id; } set { id = value; NotifyPropertyChanged("Id"); } }
        public virtual string Name { get { return name; } set { name = value; NotifyPropertyChanged("Name"); } }
        public virtual int? CustomKey { get { return customKey; } set { customKey = value; NotifyPropertyChanged("CustomKey"); } }
        public virtual SpPlaces Place { get; set; }
        public virtual SpUsers User { get; set; }
    }
    public class WorkPlacesMap : ClassMap<WorkPlaces>
    {
        public WorkPlacesMap()
        {
            Table("WORK_PLACES");
            Id(x => x.Id).Column("ID_WORK_PLACE").UnsavedValue(null);
            References(x => x.User).Column("ID_USER");
            References(x => x.Place).Column("ID_PLACE");
            Map(x => x.Name).Column("NAME");
            Map(x => x.CustomKey).Column("CUSTOM_KEY");
        }
    }
}
