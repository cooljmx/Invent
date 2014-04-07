using FluentNHibernate.Mapping;

namespace Invent.Entities
{
    public class SpDetailTypes : VirtualNotifyPropertyChanged
    {
        private int? id;
        private string name;
        public virtual int? Id { get { return id; } set { id = value; NotifyPropertyChanged("Id"); } }
        public virtual string Name { get { return name; } set { name = value; NotifyPropertyChanged("Name"); } }
    }
    public class SpDetailTypesMap : ClassMap<SpDetailTypes>
    {
        public SpDetailTypesMap()
        {
            Table("SP_DETAIL_TYPES");
            Id(x => x.Id).Column("ID_DETAIL_TYPE").UnsavedValue(null);
            Map(x => x.Name).Column("NAME");
        }
    }
}
