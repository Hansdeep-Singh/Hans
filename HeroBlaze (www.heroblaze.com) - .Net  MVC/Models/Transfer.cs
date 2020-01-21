using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Logic;
using NAudio;

namespace HeroBlaze.Models
{
    public class Transfer
    {
        private UserInitialInfoRepository uiir = new UserInitialInfoRepository();
        PhotoString ps = new PhotoString();
        public UserDetailViewModel UdToUdvm(UserDetail ud)
        {
            UserDetailViewModel udvm = new UserDetailViewModel();
            udvm.UserID = ud.UserID;
            udvm.UserAbout = ud.UserAbout;
            udvm.FirstName = ud.FirstName;
            udvm.LastName = ud.LastName;
            udvm.DobYear = ud.DobYear;
            udvm.DobMonth = ud.DobMonth;
            udvm.DobDay = ud.DobDay;
            udvm.UserDetailID = ud.UserDetailID;
            udvm.PhotoString = FileString(ud.ProfilePhoto);
            udvm.PhotoStringThumb = FileString(ud.ProfilePhotoThumb);
            return udvm;
        }

        public UserDetail UdvmToUd(UserDetailViewModel udvm, UserDetail ud)
        {
            
            ud.UserDetailID = udvm.UserDetailID;
            ud.UserID = udvm.UserID;
            ud.UserAbout = udvm.UserAbout;
            ud.FirstName = udvm.FirstName;
            ud.LastName = udvm.LastName;
            ud.DobYear = udvm.DobYear;
            ud.DobMonth = udvm.DobMonth;
            ud.DobDay = udvm.DobDay;
            return ud;
        }
       

        public UserTalent UtvmToUt(UserTalentViewModel utvm, UserTalent ut)
        {
            ut.UserID = utvm.UserID;
            ut.TalentDescription = utvm.TalentDescription;
            ut.TalentName = utvm.TalentName;
            ut.UserTalentID = utvm.UserTalentID;
            return ut;
        }

        public UserTalentViewModel UtToUtvm(UserTalentViewModel utvm, UserTalent ut)
        {
            utvm.UserID = ut.UserID;
            utvm.TalentDescription = ut.TalentDescription;
            utvm.TalentName = ut.TalentName;
            utvm.UserTalentID = ut.UserTalentID;
            return utvm;
        }

        public AudioMedia AmvmToAm(AudioMedia am, AudioMediaViewModel amvm)
        {
            am.AudioByte = amvm.AudioByte;
            am.AudioMediaID = amvm.AudioMediaID;
            am.AudioTitle = amvm.AudioTitle;
            am.Description= amvm.Description;
            am.UserTalentID = amvm.UserTalentID;
            return am;
        }

        public AudioMediaViewModel AmToAmvm(AudioMedia am, AudioMediaViewModel amvm)
        {
            amvm.AudioByte = am.AudioByte;
            amvm.AudioMediaID = am.AudioMediaID;
            amvm.AudioTitle = am.AudioTitle;
            amvm.Description= am.Description;
            amvm.UserTalentID = am.UserTalentID;
            amvm.AudioFile = ps.AudioByteToFile(am.AudioByte);
            amvm.SongIndex = amvm.SongIndex + 1;
          //  amvm.Duration = amvm.AudioByte;
            return amvm;
        }

        public DocumentMedia DmvmToDm(DocumentMedia dm, DocumentMediaViewModel dmvm)
        {
            dm.DocumentMediaID = dmvm.DocumentMediaID;
            dm.DocumentName = dmvm.DocumentName;
            dm.UserTalentID = dmvm.UserTalentID;
            return dm;
        }


        public DocumentMediaViewModel DmToDmvm(DocumentMedia dm, DocumentMediaViewModel dmvm)
        {
            dmvm.DocumentMediaID = dm.DocumentMediaID;
            dmvm.DocumentString = FileString(dm.DocumentByte);
            dmvm.DocumentName = dm.DocumentName;
            dmvm.UserTalentID = dm.UserTalentID;
            return dmvm;
        }
         public VideoMediaViewModel VmToVmvm(VideoMedia vm, VideoMediaViewModel vmvm)
        {
            vmvm.UserTalentID = vm.UserTalentID;
            vmvm.VideoLink = vm.VideoLink;
            vmvm.VideoMediaID = vm.VideoMediaID;
            vmvm.VideoTitle = vm.VideoTitle;
            return vmvm;
        }

        public VideoMedia VmvmToVm(VideoMedia vm, VideoMediaViewModel vmvm)
        {
            vm.UserTalentID = vmvm.UserTalentID;
            vm.VideoLink = vmvm.VideoLink;
            vm.VideoMediaID = vmvm.VideoMediaID;
            vm.VideoTitle = vmvm.VideoTitle;
            return vm;
        }

        public ImageMedia ImtoImvm(ImageMedia im, ImageMediaViewModel imvm)
        {
            imvm.ImageMediaID = im.ImageMediaID;
            imvm.ImageName = im.ImageName;
            imvm.PhotoString = FileString(im.ImageByte);
            imvm.PhotoStringThumb = FileString(im.ImageByteThumb);
            imvm.UserTalentID = im.UserTalentID;
            return im;
        }

        public ImageMediaViewModel ImvmtoIm(ImageMedia im, ImageMediaViewModel imvm)
        {
            im.ImageMediaID = imvm.ImageMediaID;
            im.ImageName = imvm.ImageName;
            im.UserTalentID = imvm.UserTalentID;
            return imvm;
        }

        public string FileString(Byte[] FileByte)
        {
            string File;
            if (FileByte == null) { File = ps.thumb; }
            else { File = ps.PhotoByteToString(FileByte); }
            return File;
        }
    }
}