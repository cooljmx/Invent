using FluentNHibernate.Mapping;

namespace Invent.Entities
{
    public class SpStatuses : VirtualNotifyPropertyChanged
    {
        private int id;
        private string name;
        //private int customKey;
        private string contentColor;

        public virtual int Id { get { return id; } set { id = value; NotifyPropertyChanged("Id"); } }
        public virtual string Name { get { return name; } set { name = value; NotifyPropertyChanged("InvName"); } }
        //public virtual int CustomKey { get { return customKey; } set { customKey = value; NotifyPropertyChanged("CustomKey"); } }
        public virtual string ContentColor { get { return contentColor; }set { contentColor = value; NotifyPropertyChanged("ContentColor"); }}
    }

    public class SpStatusesMap : ClassMap<SpStatuses>
    {
        public SpStatusesMap()
        {
            Table("SP_STATUSES");
            Id(x => x.Id).Column("ID_STATUS");
            Map(x => x.Name).Column("NAME");
            //Map(x => x.CustomKey).Column("CUSTOM_KEY");
            Map(x => x.ContentColor).Column("CONTENTCOLOR");
        }
    }
}
