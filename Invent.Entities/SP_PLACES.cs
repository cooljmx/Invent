using FluentNHibernate.Mapping;

namespace Invent.Entities
{
    public class SpPlaces : VirtualNotifyPropertyChanged
    {
        private int? id;
        private string name;
        private string shortName;
        private string description;
        //private int customKey;

        public virtual int? Id { get { return id; } set { id = value; NotifyPropertyChanged("Id"); } }
        public virtual string Name { get { return name; } set { name = value; NotifyPropertyChanged("InvName"); } }
        public virtual string ShortName { get { return shortName; } set { shortName = value; NotifyPropertyChanged("ShortName"); } }
        public virtual string Description { get { return description; } set { description = value; NotifyPropertyChanged("Description"); } }
        //public virtual int CustomKey { get { return customKey; } set { customKey = value; NotifyPropertyChanged("CustomKey"); } }
    }

    public class SpPlacesMap : ClassMap<SpPlaces>
    {
        public SpPlacesMap()
        {
            Table("SP_PLACES");
            Id(x => x.Id).Column("ID_PLACE").GeneratedBy.TriggerIdentity().UnsavedValue(null); 
            Map(x => x.Name).Column("NAME");
            Map(x => x.ShortName).Column("SHORT_NAME");
            Map(x => x.Description).Column("DESCRIPTION");
            //Map(x => x.CustomKey).Column("CUSTOM_KEY");
        }
    }
}
