using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.DTO
{
    public class ImageDTO : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; set; }
        private string path;
        public string Path
        {
            get { return path; }
            set
            {
                if (value != path)
                {
                    path = value;
                    OnPropertyChanged(nameof(Path));
                }
            }
        }
        private int entityId;
        public int EntityId 
        {
            get { return entityId; }
            set
            {
                if (value != entityId)
                {
                    entityId = value;
                    OnPropertyChanged(nameof(EntityId));
                }
            }
        }
        private Enumeration.EntityType type;
        public Enumeration.EntityType Type
        {
            get { return type; }
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }
        public ImageDTO()
        {
            
        }
        public ImageDTO(Image image)
        {
            Id = image.Id;
            Path = image.Path;
            EntityId = image.EntityId;
            Type = image.Type;
        }
        public Image ToImage() 
        {
            return new Image(Id,path,entityId,type);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
