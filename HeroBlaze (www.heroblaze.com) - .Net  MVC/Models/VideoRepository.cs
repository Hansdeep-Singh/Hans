using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class VideoRepository:Repository<VideoMedia>
    {
        public void AddRow(VideoMedia vm)
        {
            Add(vm); Save();
        }
        public VideoMedia OneRow(int id)
        {
            return DbSet.Where(d => d.VideoMediaID.Equals(id)).SingleOrDefault();
        }

        public IEnumerable<VideoMedia> VideoMediasIenum(int id)
        {
            return DbSet.Where(a => a.UserTalentID.Equals(id));
        }
        public void EditVideo(VideoMedia vm)
        {
            Entry(vm); Save();
        }
        public void DeleteVideo(VideoMedia v)
        {
            Delete(v);
            Save();
        }

        public List<VideoMedia> VideoMediasList(int id)
        {
            return DbSet.Where(a => a.UserTalentID.Equals(id)).ToList();
        }
    }
}