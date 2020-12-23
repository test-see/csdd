using System;
using System.Collections.Generic;
using System.Linq;

namespace domain.sys.entities
{
    public class MenuEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime CreateTime { get; set; }
        public int PatientId { get; set; }
        public IList<MenuEntity> Children { get; set; }

        public void FindChildren(IList<MenuEntity> menus)
        {
            this.Children = menus.Where(x => x.PatientId == Id).ToList();
            foreach (var m in this.Children)
            {
                m.FindChildren(menus);
            }
        }
    }
}
