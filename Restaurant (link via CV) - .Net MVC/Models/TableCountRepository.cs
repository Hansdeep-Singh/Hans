using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{
    public class TableCountRepository:Repository<TableCount>
    {

        public void AddTableCount(TableCount tc)
        {
            Add(tc); Save();
        }

        public TableCount TableCount(string id)
        {
            return DbSet.Where(tc => tc.TableCountId.Equals(id)).SingleOrDefault();
        }
        public TableCount TableCountRow(string tablename)
        {
            return DbSet.Where(tc => tc.TableName.Equals(tablename)).SingleOrDefault();
        }

        public List<TableCount> GetAllTableCounts()
        {
            return DbSet.ToList();
        }

        public void EntryDetatch(TableCount tc)
        {
            EntryDetached(tc);
        }

        public void EntryAttach(TableCount tc)
        {
            EntryAttach(tc);
        }

        public List<TableCount> AllTableCounts()
        {
            return DbSet.ToList();
        }
        public List<TableCount> TableCounts(string id)
        {
            return DbSet.Where(i => i.TableCountId.Equals(id)).ToList();
        }


        public List<TableCount> TableCountsOnOrderId(string id)
        {
            return DbSet.Where(i => i.TableCountId.Equals(id)).ToList();
        }


        public void DeleteTableCount(TableCount tc)
        {
            Delete(tc); Save();
        }

        public void DeleteTableCounts(List<TableCount> tc_list)
        {
            DeleteRange(tc_list); Save();
        }

        public Boolean IfExists(string id)
        {
            if (DbSet.Any(ie => ie.TableCountId.Equals(id)))
                return true;
            else
                return false;
        }


        public void UpdateTableCount(TableCount tc)
        {
            Entry(tc); Save();
        }
    }
}
