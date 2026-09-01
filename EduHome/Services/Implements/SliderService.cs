using EduHome.Contexts;
using EduHome.Enums;
using EduHome.Extensions;
using EduHome.Models;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.Slider;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EduHome.Services.Implements
{
    public class SliderService : ISliderService
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public SliderService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public void Create(SliderCreateVM vm)
        {
            if (! vm.Image.IsSizeValid(2, FileSize.MB)) throw new Exception("Size is not valid!");
            if (!vm.Image.IsFormatValid()) throw new Exception("Format is not valid");
            var slider = new Slider
            {
                Title = vm.Title,
                Text = vm.Text,
                Image = vm.Image.UploadFile( _env.WebRootPath, "images/slider"),
                CreatedAt = DateTime.UtcNow.AddHours(3),
            };

            var entry = _context.sliders.Add(slider);
            if (entry.State != EntityState.Added) throw new Exception("Add failed!");
            var count = _context.SaveChanges();
            if (count <= 0) throw new Exception("Save Slider failed!");

        }

        public List<SliderGetVM> GetAll()
        {
            var sliders = _context.sliders.AsNoTracking().ToList();
            var vms = sliders.Select(slider => new SliderGetVM
            {
                Id = slider.Id,
                Text = slider.Text,
                CreatedAt= slider.CreatedAt,
                Title = slider.Title,
                Image = slider.Image,  
                UpdatedAt = slider.UpdatedAt,
            }).ToList();
            return vms;
        }

        public SliderGetVM GetSingle(int id)
        {
            var slider = _context.sliders.AsNoTracking().FirstOrDefault(slider => slider.Id == id);
            if (slider == null) throw new Exception("Slider not found!");
            var vm = new SliderGetVM
            {
                Id = slider.Id,
                Text = slider.Text,
                CreatedAt = slider.CreatedAt,
                Title = slider.Title,
                Image = slider.Image,
                UpdatedAt = slider.UpdatedAt,
            };
            return vm;
        }

        public void Remove(int id)
        {
            var slider = _context.sliders.Find(id);
            if (slider == null) throw new Exception("Slider not found!");
            var path = $"{_env.WebRootPath}/images/slider/{slider.Image}";
            if (File.Exists(path)) File.Delete(path);


            var entry = _context.Remove(slider);
            if (entry.State != EntityState.Deleted) throw new Exception("Remove failed");
            var count = _context.SaveChanges();
            if (count <= 0) throw new Exception("Save failed!");

        }

        public void Update(int id, SliderUpdateVM vm)
        {
            var slider = _context.sliders.Find();
            if (slider == null) throw new Exception("Slider not found!");
            slider.Text = vm.Text;
            slider.Title = vm.Title;
            if (vm.Image != null)
            {
                if (!vm.Image.IsSizeValid(2, FileSize.MB)) throw new Exception("Size is not valid!");
                if (!vm.Image.IsFormatValid()) throw new Exception("Format is not valid");
                var path = $"{_env.WebRootPath}/images/slider/{slider.Image}";
                if (File.Exists(path)) File.Delete(path);
                slider.Image = vm.Image.UploadFile(_env.WebRootPath, "images/slider");
            }
            slider.UpdatedAt = DateTime.UtcNow.AddHours(3);
            var entry = _context.sliders.Update(slider);
            if (entry.State != EntityState.Modified) throw new Exception("Update failed");
            var count = _context.SaveChanges();
            if (count <= 0) throw new Exception("Save failed");
        }
    }
}
