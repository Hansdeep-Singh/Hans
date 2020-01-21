using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class AudioRepository:Repository<AudioMedia>
    {
        public void AddRow(AudioMedia am)
        {
            Add(am); Save();
        }
        public AudioMedia OneRow(int id)
        {
            return DbSet.Where(a => a.AudioMediaID.Equals(id)).SingleOrDefault();
        }

        public IEnumerable<AudioMedia> AudioMediasIenum(int id)
        {
            return DbSet.Where(a => a.UserTalentID.Equals(id));
        }
        public void EditAudio(AudioMedia am)
        {
            Entry(am); Save();
        }
        public void DeleteAudio(AudioMedia a)
        {
            Delete(a);
            Save();
        }

        public List<AudioMedia> AudioMediasList(int id)
        {
            return DbSet.Where(a => a.UserTalentID.Equals(id)).ToList();
        }

        public void DeleteMultipleAudios(int UserTalentId)
        {
            Context.Audios.RemoveRange(Context.Audios.Where(c => c.UserTalentID == UserTalentId));
            Save();
        }

    }
}