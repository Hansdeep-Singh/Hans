using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class ImageRepository:Repository<ImageMedia>
    {

        public void AddRow(ImageMedia im)
        {
            Add(im); Save();
        }
        public ImageMedia OneRow(int id)
        {
            return DbSet.Where(d => d.ImageMediaID.Equals(id)).SingleOrDefault();
        }

        public IEnumerable<ImageMedia> ImageMediasIenum(int id)
        {
            return DbSet.Where(i => i.UserTalentID.Equals(id));
        }
        public void EditImage(ImageMedia im)
        {
            Entry(im); Save();
        }
        public void DeleteImage(ImageMedia i)
        {
            Delete(i);
            Save();
        }

        public List<ImageMedia> ImageMediasList(int id)
        {
            return DbSet.Where(i => i.UserTalentID.Equals(id)).ToList();
        }
    }
}